namespace Bioingenieria.Models;

public class DocumentCategory
{
    public string FolderKey { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public List<string> FilePaths { get; set; } = new();
}
