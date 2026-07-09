using System.Diagnostics;

namespace Bioingenieria.Services;

public static class FileOpenerService
{
    public static void Open(string filePath)
    {
        try
        {
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"No se pudo abrir el archivo:\n{filePath}\n\n{ex.Message}",
                "Error al abrir archivo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
