using System.Windows;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class UploadDocumentWindow : Window
{
    private readonly EquipmentService _equipmentService;
    private string? _selectedFilePath;

    public UploadDocumentWindow(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        InitializeComponent();
        LoadEquipmentOptions();
        LoadCategoryOptions();
    }

    private void LoadEquipmentOptions()
    {
        EquipmentComboBox.ItemsSource = _equipmentService.GetAll()
            .Select(e => $"{e.SerialNumber} — {e.Name}")
            .ToArray();
    }

    private void LoadCategoryOptions()
    {
        var existingKeys = _equipmentService.GetAll()
            .SelectMany(e => e.Categories.Select(c => c.FolderKey));

        CategoryComboBox.ItemsSource = CategoryCatalog.KnownKeys
            .Concat(existingKeys)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(k => k, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog { Title = "Seleccionar documento" };
        if (dialog.ShowDialog(this) == true)
        {
            _selectedFilePath = dialog.FileName;
            FilePathTextBox.Text = _selectedFilePath;
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var serial = ExtractSerial(EquipmentComboBox.Text);
        var category = CategoryComboBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(serial) || !_equipmentService.SerialExists(serial))
        {
            ShowError("Selecciona un equipo válido.");
            return;
        }

        if (string.IsNullOrWhiteSpace(category) || !EquipmentService.IsValidFolderName(category))
        {
            ShowError("Ingresa o selecciona una categoría válida.");
            return;
        }

        if (string.IsNullOrEmpty(_selectedFilePath))
        {
            ShowError("Selecciona un archivo.");
            return;
        }

        if (!TrySave(serial, category, overwrite: false))
        {
            return;
        }

        DialogResult = true;
    }

    private bool TrySave(string serial, string category, bool overwrite)
    {
        try
        {
            _equipmentService.SaveFileToCategory(serial, category, _selectedFilePath!, overwrite);
            return true;
        }
        catch (IOException)
        {
            var confirm = MessageBox.Show(
                this,
                "Ya existe un archivo con ese nombre en esta categoría. ¿Deseas reemplazarlo?",
                "Confirmar sobrescritura",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            return confirm == MessageBoxResult.Yes && TrySave(serial, category, overwrite: true);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
            return false;
        }
    }

    private static string ExtractSerial(string comboText)
    {
        var separatorIndex = comboText.IndexOf('—');
        return separatorIndex > 0 ? comboText[..separatorIndex].Trim() : comboText.Trim();
    }

    private void ShowError(string message)
    {
        ErrorTextBlock.Text = message;
        ErrorTextBlock.Visibility = Visibility.Visible;
    }
}
