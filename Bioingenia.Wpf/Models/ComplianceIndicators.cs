namespace Bioingenieria.Models;

public enum ComplianceSemaphore
{
    Green,
    Yellow,
    Red
}

public class AreaCompliance
{
    public string Area { get; set; } = string.Empty;
    public int Scheduled { get; set; }
    public int Executed { get; set; }
    public int OnTime { get; set; }
    public int Early { get; set; }
    public int Late { get; set; }
    public int Overdue { get; set; }
    public int MaxDelayWeeks { get; set; }

    public double CompliancePercentage => Scheduled == 0 ? 0 : Executed * 100.0 / Scheduled;

    public ComplianceSemaphore Semaphore =>
        MaxDelayWeeks >= 4 ? ComplianceSemaphore.Red :
        MaxDelayWeeks >= 2 ? ComplianceSemaphore.Yellow :
        ComplianceSemaphore.Green;

    public List<EquipmentCompliance> ByEquipment { get; set; } = new();
}

public class EquipmentCompliance
{
    public string InventoryTag { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public int Scheduled { get; set; }
    public int Executed { get; set; }
    public int OnTime { get; set; }
    public int Early { get; set; }
    public int Late { get; set; }
    public int Overdue { get; set; }
    public int MaxDelayWeeks { get; set; }

    public double CompliancePercentage => Scheduled == 0 ? 0 : Executed * 100.0 / Scheduled;

    public ComplianceSemaphore Semaphore =>
        MaxDelayWeeks >= 4 ? ComplianceSemaphore.Red :
        MaxDelayWeeks >= 2 ? ComplianceSemaphore.Yellow :
        ComplianceSemaphore.Green;
}

public class ComplianceIndicators
{
    public int Scheduled { get; set; }
    public int Executed { get; set; }
    public int OnTime { get; set; }
    public int Early { get; set; }
    public int Late { get; set; }
    public int Overdue { get; set; }
    public int UnscheduledExecutions { get; set; }
    public double AverageLateWeeks { get; set; }

    public double CompliancePercentage => Scheduled == 0 ? 0 : Executed * 100.0 / Scheduled;

    public List<AreaCompliance> ByArea { get; set; } = new();

    // Key: weeks late (positive = late, negative = early), Value: count of occurrences.
    public Dictionary<int, int> DelayHistogram { get; set; } = new();
}
