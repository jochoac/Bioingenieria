using System.IO.Compression;
using System.Xml.Linq;
using Bioingenieria.Models;
using ClosedXML.Excel;

namespace Bioingenieria.Services;

public static class CronogramaExcelReader
{
    private const string InventoryTagHeader = "PLACA DE INVENTARIO";
    private const int GridStartColumn = 6; // column F
    private const int MonthCount = 12;
    private const int WeeksPerMonth = 4;
    private const int ColumnsPerMonth = WeeksPerMonth * 2; // P and E column per week

    public static List<ScheduleEntry> Read(string filePath)
    {
        // Los cronogramas institucionales suelen traer imágenes/logos incrustados (exportados desde
        // Google Sheets) que hacen fallar a ClosedXML al cargar los "drawings". No se necesitan para
        // leer datos de celdas, así que se eliminan de una copia temporal antes de abrir el archivo.
        var sanitizedPath = StripEmbeddedImages(filePath);

        try
        {
            using var workbook = new XLWorkbook(sanitizedPath);
            var worksheet = workbook.Worksheets.First();

            return ReadWorksheet(worksheet);
        }
        finally
        {
            File.Delete(sanitizedPath);
        }
    }

    private static List<ScheduleEntry> ReadWorksheet(IXLWorksheet worksheet)
    {

        var headerRow = FindHeaderRow(worksheet);
        if (headerRow is null)
        {
            throw new InvalidOperationException(
                $"No se encontró la columna '{InventoryTagHeader}' en el archivo. Verifica que sea un cronograma con el formato esperado.");
        }

        var entries = new List<ScheduleEntry>();
        var currentArea = string.Empty;
        var row = headerRow.Value + 3; // header ocupa 3 filas fusionadas (etiqueta, mes, semana)

        while (true)
        {
            var cellA = worksheet.Cell(row, 1);
            var cellB = worksheet.Cell(row, 2);

            if (cellA.IsEmpty() && cellB.IsEmpty())
            {
                break;
            }

            if (cellB.IsEmpty())
            {
                currentArea = cellA.GetString().Trim();
                row++;
                continue;
            }

            entries.Add(ReadEntry(worksheet, row, currentArea));
            row++;
        }

        return entries;
    }

    private static ScheduleEntry ReadEntry(IXLWorksheet worksheet, int row, string area)
    {
        var entry = new ScheduleEntry
        {
            Area = area,
            InventoryTag = worksheet.Cell(row, 1).GetString().Trim(),
            EquipmentName = worksheet.Cell(row, 2).GetString().Trim(),
            Provider = worksheet.Cell(row, 3).GetString().Trim(),
            Periodicity = worksheet.Cell(row, 4).GetString().Trim(),
            WarrantyExpiration = worksheet.Cell(row, 5).GetString().Trim()
        };

        for (var month = 1; month <= MonthCount; month++)
        {
            var monthStartColumn = GridStartColumn + (month - 1) * ColumnsPerMonth;

            for (var week = 1; week <= WeeksPerMonth; week++)
            {
                var scheduledColumn = monthStartColumn + (week - 1) * 2;
                var executedColumn = scheduledColumn + 1;
                var weekIndex = (month - 1) * WeeksPerMonth + week;

                if (worksheet.Cell(row, scheduledColumn).GetString().Trim().Equals("P", StringComparison.OrdinalIgnoreCase))
                {
                    entry.ScheduledWeeks.Add(weekIndex);
                }

                if (worksheet.Cell(row, executedColumn).GetString().Trim().Equals("E", StringComparison.OrdinalIgnoreCase))
                {
                    entry.ExecutedWeeks.Add(weekIndex);
                }
            }
        }

        return entry;
    }

    private static int? FindHeaderRow(IXLWorksheet worksheet)
    {
        var usedRange = worksheet.RangeUsed();
        if (usedRange is null)
        {
            return null;
        }

        foreach (var row in usedRange.Rows())
        {
            var text = row.Cell(1).GetString().Trim();
            if (string.Equals(text, InventoryTagHeader, StringComparison.OrdinalIgnoreCase))
            {
                return row.RowNumber();
            }
        }

        return null;
    }

    private static string StripEmbeddedImages(string filePath)
    {
        var sanitizedPath = Path.Combine(Path.GetTempPath(), $"cronograma_{Guid.NewGuid():N}.xlsx");
        File.Copy(filePath, sanitizedPath);

        using var archive = ZipFile.Open(sanitizedPath, ZipArchiveMode.Update);

        foreach (var entry in archive.Entries.ToList())
        {
            if (entry.FullName.StartsWith("xl/drawings/", StringComparison.OrdinalIgnoreCase)
                || entry.FullName.StartsWith("xl/media/", StringComparison.OrdinalIgnoreCase))
            {
                entry.Delete();
            }
        }

        foreach (var entry in archive.Entries.ToList())
        {
            if (entry.FullName.Contains("worksheets/_rels/", StringComparison.OrdinalIgnoreCase))
            {
                RemoveDrawingRelationships(entry);
            }
            else if (entry.FullName.StartsWith("xl/worksheets/", StringComparison.OrdinalIgnoreCase)
                     && entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                RemoveDrawingElement(entry);
            }
            else if (string.Equals(entry.FullName, "[Content_Types].xml", StringComparison.OrdinalIgnoreCase))
            {
                RemoveDrawingContentTypeOverrides(entry);
            }
        }

        return sanitizedPath;
    }

    private static void RemoveDrawingRelationships(ZipArchiveEntry entry)
    {
        XNamespace rel = "http://schemas.openxmlformats.org/package/2006/relationships";

        RewriteXmlEntry(entry, doc => doc.Root?
            .Elements(rel + "Relationship")
            .Where(e => ((string?)e.Attribute("Target"))?.Contains("drawings", StringComparison.OrdinalIgnoreCase) == true)
            .Remove());
    }

    private static void RemoveDrawingElement(ZipArchiveEntry entry)
    {
        XNamespace main = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        RewriteXmlEntry(entry, doc => doc.Root?.Elements(main + "drawing").Remove());
    }

    private static void RemoveDrawingContentTypeOverrides(ZipArchiveEntry entry)
    {
        XNamespace ct = "http://schemas.openxmlformats.org/package/2006/content-types";

        RewriteXmlEntry(entry, doc => doc.Root?
            .Elements(ct + "Override")
            .Where(e => ((string?)e.Attribute("PartName"))?.Contains("/drawings/", StringComparison.OrdinalIgnoreCase) == true)
            .Remove());
    }

    private static void RewriteXmlEntry(ZipArchiveEntry entry, Action<XDocument> mutate)
    {
        XDocument document;
        using (var stream = entry.Open())
        {
            document = XDocument.Load(stream);
        }

        mutate(document);

        var archive = entry.Archive;
        var fullName = entry.FullName;
        entry.Delete();

        var newEntry = archive.CreateEntry(fullName);
        using var writer = newEntry.Open();
        document.Save(writer);
    }
}
