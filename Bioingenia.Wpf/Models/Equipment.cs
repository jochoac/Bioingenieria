namespace Bioingenieria.Models;

public class Equipment
{
    public string SerialNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FolderPath { get; set; } = string.Empty;
    public List<DocumentCategory> Categories { get; set; } = new();
}
