namespace Bioingenieria.Models;

public class ScheduleEntry
{
    public string Area { get; set; } = string.Empty;
    public string InventoryTag { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string Periodicity { get; set; } = string.Empty;
    public string WarrantyExpiration { get; set; } = string.Empty;

    // Absolute week-of-year index: (month - 1) * 4 + week, 1..48.
    public List<int> ScheduledWeeks { get; set; } = new();
    public List<int> ExecutedWeeks { get; set; } = new();
}
