using Bioingenieria.Models;

namespace Bioingenieria.Services;

public class EquipmentService
{
    private const string SystemFolderName = "_system";
    private const string MetadataFileName = "metadata.json";

    private readonly string _rootPath;
    private List<Equipment> _cache = new();

    public EquipmentService(string rootPath)
    {
        _rootPath = rootPath;
        RefreshCache();
    }

    public void RefreshCache()
    {
        var equipments = new List<Equipment>();

        if (!Directory.Exists(_rootPath))
        {
            _cache = equipments;
            return;
        }

        foreach (var folder in Directory.GetDirectories(_rootPath))
        {
            var folderName = Path.GetFileName(folder);
            if (string.Equals(folderName, SystemFolderName, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            equipments.Add(LoadEquipment(folder, folderName));
        }

        _cache = equipments;
    }

    private static Equipment LoadEquipment(string folder, string serialNumber)
    {
        var metadataPath = Path.Combine(folder, MetadataFileName);
        var metadata = JsonFileStore.Read<EquipmentMetadata>(metadataPath) ?? new EquipmentMetadata { Name = serialNumber };

        var equipment = new Equipment
        {
            SerialNumber = serialNumber,
            Name = metadata.Name,
            FolderPath = folder
        };

        foreach (var categoryFolder in Directory.GetDirectories(folder))
        {
            var files = Directory.GetFiles(categoryFolder);
            if (files.Length == 0)
            {
                continue;
            }

            var folderKey = Path.GetFileName(categoryFolder);
            equipment.Categories.Add(new DocumentCategory
            {
                FolderKey = folderKey,
                DisplayName = CategoryCatalog.GetDisplayName(folderKey),
                FilePaths = files.ToList()
            });
        }

        return equipment;
    }

    public IReadOnlyList<Equipment> GetAll()
    {
        return _cache;
    }

    public IReadOnlyList<Equipment> SearchBySerial(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return _cache;
        }

        return _cache
            .Where(e => e.SerialNumber.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public bool SerialExists(string serialNumber)
    {
        return _cache.Any(e => string.Equals(e.SerialNumber, serialNumber, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsValidFolderName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return name.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;
    }

    public Equipment CreateEquipment(string serialNumber, string name)
    {
        if (!IsValidFolderName(serialNumber))
        {
            throw new ArgumentException("El número de serie contiene caracteres no válidos o está vacío.");
        }

        if (SerialExists(serialNumber))
        {
            throw new InvalidOperationException($"Ya existe un equipo con la serie '{serialNumber}'.");
        }

        var folder = Path.Combine(_rootPath, serialNumber);
        Directory.CreateDirectory(folder);
        JsonFileStore.Write(Path.Combine(folder, MetadataFileName), new EquipmentMetadata { Name = name });

        RefreshCache();
        return _cache.First(e => string.Equals(e.SerialNumber, serialNumber, StringComparison.OrdinalIgnoreCase));
    }

    public string SaveFileToCategory(string serialNumber, string categoryKey, string sourceFilePath, bool overwrite)
    {
        var categoryFolder = Path.Combine(_rootPath, serialNumber, categoryKey);
        Directory.CreateDirectory(categoryFolder);

        var destinationPath = Path.Combine(categoryFolder, Path.GetFileName(sourceFilePath));
        if (File.Exists(destinationPath) && !overwrite)
        {
            throw new IOException($"El archivo '{Path.GetFileName(destinationPath)}' ya existe en esta categoría.");
        }

        File.Copy(sourceFilePath, destinationPath, overwrite: true);
        RefreshCache();
        return destinationPath;
    }
}
