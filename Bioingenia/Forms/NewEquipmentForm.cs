using Bioingenieria.Services;

namespace Bioingenieria.Forms;

public partial class NewEquipmentForm : Form
{
    private readonly EquipmentService _equipmentService;

    public NewEquipmentForm(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        InitializeComponent();
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        var serial = serialTextBox.Text.Trim();
        var name = nameTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(serial) || string.IsNullOrWhiteSpace(name))
        {
            ShowError("El número de serie y el nombre son obligatorios.");
            return;
        }

        if (!EquipmentService.IsValidFolderName(serial))
        {
            ShowError("El número de serie contiene caracteres no válidos.");
            return;
        }

        if (_equipmentService.SerialExists(serial))
        {
            ShowError($"Ya existe un equipo con la serie '{serial}'.");
            return;
        }

        try
        {
            _equipmentService.CreateEquipment(serial, name);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        errorLabel.Text = message;
        errorLabel.Visible = true;
    }
}
