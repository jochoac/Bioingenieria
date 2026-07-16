using Bioingenieria.Models;

namespace Bioingenieria.Services;

public class ScheduleService
{
    private readonly string _rootPath;
    private readonly Dictionary<ScheduleType, ScheduleImport?> _cache = new();

    public ScheduleService(string rootPath)
    {
        _rootPath = rootPath;
        RefreshCache();
    }

    public void RefreshCache()
    {
        foreach (var type in Enum.GetValues<ScheduleType>())
        {
            _cache[type] = JsonFileStore.Read<ScheduleImport>(ConfigService.SchedulePath(_rootPath, type));
        }
    }

    public ScheduleImport? GetImport(ScheduleType type)
    {
        return _cache.TryGetValue(type, out var import) ? import : null;
    }

    public ScheduleImport ImportFromExcel(string filePath, ScheduleType type, int year)
    {
        var import = new ScheduleImport
        {
            Type = type,
            Year = year,
            ImportedAtUtc = DateTime.UtcNow,
            SourceFileName = Path.GetFileName(filePath),
            SourceFilePath = filePath,
            Entries = CronogramaExcelReader.Read(filePath)
        };

        JsonFileStore.Write(ConfigService.SchedulePath(_rootPath, type), import);
        _cache[type] = import;

        return import;
    }

    public ComplianceIndicators ComputeIndicators(ScheduleType type)
    {
        var import = GetImport(type);
        var indicators = new ComplianceIndicators();

        if (import is null)
        {
            return indicators;
        }

        var currentWeek = CurrentAbsoluteWeek(import.Year);
        var areaMap = new Dictionary<string, AreaCompliance>(StringComparer.OrdinalIgnoreCase);
        var lateDeltas = new List<int>();

        foreach (var entry in import.Entries)
        {
            var area = GetOrAddArea(areaMap, entry.Area);
            var equipment = new EquipmentCompliance
            {
                InventoryTag = entry.InventoryTag,
                EquipmentName = entry.EquipmentName
            };
            area.ByEquipment.Add(equipment);

            var scheduled = entry.ScheduledWeeks.OrderBy(w => w).ToList();
            var executed = entry.ExecutedWeeks.OrderBy(w => w).ToList();
            var pairCount = Math.Min(scheduled.Count, executed.Count);

            for (var i = 0; i < pairCount; i++)
            {
                var delta = executed[i] - scheduled[i];

                indicators.Scheduled++;
                indicators.Executed++;
                area.Scheduled++;
                area.Executed++;
                equipment.Scheduled++;
                equipment.Executed++;

                if (delta == 0)
                {
                    indicators.OnTime++;
                    area.OnTime++;
                    equipment.OnTime++;
                }
                else if (delta < 0)
                {
                    indicators.Early++;
                    area.Early++;
                    equipment.Early++;
                }
                else
                {
                    indicators.Late++;
                    area.Late++;
                    equipment.Late++;
                    lateDeltas.Add(delta);
                }

                indicators.DelayHistogram[delta] = indicators.DelayHistogram.GetValueOrDefault(delta) + 1;
            }

            foreach (var week in GetLeftoverScheduledWeeks(entry))
            {
                indicators.Scheduled++;
                area.Scheduled++;
                equipment.Scheduled++;

                if (week < currentWeek)
                {
                    indicators.Overdue++;
                    area.Overdue++;
                    equipment.Overdue++;
                }
            }

            for (var i = pairCount; i < executed.Count; i++)
            {
                indicators.Executed++;
                indicators.UnscheduledExecutions++;
                area.Executed++;
                equipment.Executed++;
            }

            equipment.MaxDelayWeeks = GetDelayWeeks(entry, currentWeek).DefaultIfEmpty(0).Max();
        }

        indicators.AverageLateWeeks = lateDeltas.Count == 0 ? 0 : lateDeltas.Average();
        indicators.ByArea = areaMap.Values.OrderBy(a => a.Area, StringComparer.OrdinalIgnoreCase).ToList();

        foreach (var area in indicators.ByArea)
        {
            area.ByEquipment = area.ByEquipment
                .OrderBy(eq => eq.CompliancePercentage)
                .ThenBy(eq => eq.EquipmentName, StringComparer.OrdinalIgnoreCase)
                .ToList();
            area.MaxDelayWeeks = area.ByEquipment.Select(eq => eq.MaxDelayWeeks).DefaultIfEmpty(0).Max();
        }

        return indicators;
    }

    public SemesterSummary ComputeSemesterSummary(ScheduleType type, Semester semester)
    {
        var import = GetImport(type);

        if (import is null)
        {
            return new SemesterSummary();
        }

        var (start, end) = type == ScheduleType.Maintenance ? semester.GetWeekRange() : (1, 48);

        return new SemesterSummary
        {
            Scheduled = import.Entries.Sum(e => e.ScheduledWeeks.Count(w => w >= start && w <= end)),
            Executed = import.Entries.Sum(e => e.ExecutedWeeks.Count(w => w >= start && w <= end))
        };
    }

    public List<UpcomingAlert> GetUpcomingAlerts(int withinDays = 30)
    {
        var today = DateTime.Today;

        return Enum.GetValues<ScheduleType>()
            .SelectMany(type => GetImport(type) is { } import
                ? import.Entries.Select(entry => (Type: type, Year: import.Year, Entry: entry))
                : Enumerable.Empty<(ScheduleType Type, int Year, ScheduleEntry Entry)>())
            .Select(x => (x.Type, x.Entry, x.Year, DueWeek: GetLeftoverScheduledWeeks(x.Entry).DefaultIfEmpty(0).First()))
            .Where(x => x.DueWeek > 0)
            .Select(x => new UpcomingAlert
            {
                Type = x.Type,
                Area = x.Entry.Area,
                EquipmentName = x.Entry.EquipmentName,
                InventoryTag = x.Entry.InventoryTag,
                DueDate = AbsoluteWeekToDate(x.Year, x.DueWeek)
            })
            .Where(alert => (alert.DueDate - today).Days <= withinDays)
            .OrderBy(alert => alert.DueDate)
            .ToList();
    }

    private static DateTime AbsoluteWeekToDate(int year, int week)
    {
        var month = (week - 1) / 4 + 1;
        var weekInMonth = (week - 1) % 4 + 1;
        var day = (weekInMonth - 1) * 7 + 1;
        return new DateTime(year, month, day);
    }

    private static List<int> GetLeftoverScheduledWeeks(ScheduleEntry entry)
    {
        var scheduled = entry.ScheduledWeeks.OrderBy(w => w).ToList();
        var executed = entry.ExecutedWeeks.OrderBy(w => w).ToList();
        var pairCount = Math.Min(scheduled.Count, executed.Count);

        return scheduled.Skip(pairCount).ToList();
    }

    private static IEnumerable<int> GetDelayWeeks(ScheduleEntry entry, int currentWeek)
    {
        var scheduled = entry.ScheduledWeeks.OrderBy(w => w).ToList();
        var executed = entry.ExecutedWeeks.OrderBy(w => w).ToList();
        var pairCount = Math.Min(scheduled.Count, executed.Count);

        var lateDelays = Enumerable.Range(0, pairCount)
            .Select(i => executed[i] - scheduled[i])
            .Where(delta => delta > 0);

        var overdueDelays = GetLeftoverScheduledWeeks(entry)
            .Where(week => week < currentWeek)
            .Select(week => currentWeek - week);

        return lateDelays.Concat(overdueDelays);
    }

    private static AreaCompliance GetOrAddArea(Dictionary<string, AreaCompliance> map, string area)
    {
        var key = string.IsNullOrWhiteSpace(area) ? "(Sin área)" : area;
        if (!map.TryGetValue(key, out var value))
        {
            value = new AreaCompliance { Area = key };
            map[key] = value;
        }

        return value;
    }

    private static int CurrentAbsoluteWeek(int year)
    {
        if (DateTime.Today.Year != year)
        {
            return DateTime.Today.Year > year ? 49 : 0;
        }

        var week = Math.Min(4, (DateTime.Today.Day - 1) / 7 + 1);
        return (DateTime.Today.Month - 1) * 4 + week;
    }
}
