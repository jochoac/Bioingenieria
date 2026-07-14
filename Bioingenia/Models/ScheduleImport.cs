namespace Bioingenieria.Models;

public class ScheduleImport
{
    public ScheduleType Type { get; set; }
    public int Year { get; set; }
    public DateTime ImportedAtUtc { get; set; }
    public string SourceFileName { get; set; } = string.Empty;
    public List<ScheduleEntry> Entries { get; set; } = new();
}
