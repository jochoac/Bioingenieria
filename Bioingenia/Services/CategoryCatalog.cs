using System.Globalization;

namespace Bioingenieria.Services;

public static class CategoryCatalog
{
    private static readonly Dictionary<string, string> DisplayNames = new(StringComparer.OrdinalIgnoreCase)
    {
        ["hoja_vida"] = "Hoja de vida",
        ["manual_usuario"] = "Manual de usuario",
        ["manual_servicio"] = "Manual de servicio",
        ["mantenimiento"] = "Mantenimiento",
        ["calibracion"] = "Calibración",
        ["garantia"] = "Garantía",
        ["factura"] = "Factura"
    };

    public static string GetDisplayName(string folderKey)
    {
        if (DisplayNames.TryGetValue(folderKey, out var displayName))
        {
            return displayName;
        }

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(folderKey.Replace('_', ' '));
    }
}
