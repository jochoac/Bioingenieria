using Bioingenieria.Services;

namespace Bioingenieria.Forms;

public partial class UploadDocumentForm : Form
{
    private readonly EquipmentService _equipmentService;
    private string? _selectedFilePath;

    public UploadDocumentForm(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        InitializeComponent();
        LoadEquipmentOptions();
        LoadCategoryOptions();
    }

    private void LoadEquipmentOptions()
    {
        var items = _equipmentService.GetAll()
            .Select(e => $"{e.SerialNumber} — {e.Name}")
            .ToArray();

        equipmentComboBox.Items.Clear();
        equipmentComboBox.Items.AddRange(items);
        equipmentComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        equipmentComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
    }

    private void LoadCategoryOptions()
    {
        var keys = _equipmentService.GetAll()
            .SelectMany(e => e.Categories.Select(c => c.FolderKey))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(k => k)
            .ToArray();

        categoryComboBox.Items.Clear();
        categoryComboBox.Items.AddRange(keys);
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog { Title = "Seleccionar documento" };
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _selectedFilePath = dialog.FileName;
            filePathTextBox.Text = _selectedFilePath;
        }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        var serial = ExtractSerial(equipmentComboBox.Text);
        var category = categoryComboBox.Text.Trim();

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

        DialogResult = DialogResult.OK;
        Close();
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
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return confirm == DialogResult.Yes && TrySave(serial, category, overwrite: true);
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
        errorLabel.Text = message;
        errorLabel.Visible = true;
    }
}
