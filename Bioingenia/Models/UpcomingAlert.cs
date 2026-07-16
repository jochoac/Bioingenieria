namespace Bioingenieria.Models;

public class UpcomingAlert
{
    public ScheduleType Type { get; set; }
    public string Area { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public string InventoryTag { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}