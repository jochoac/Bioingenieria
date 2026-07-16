using System.Windows;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class NewEquipmentWindow : Window
{
    private readonly EquipmentService _equipmentService;

    public NewEquipmentWindow(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var serial = SerialTextBox.Text.Trim();
        var name = NameTextBox.Text.Trim();

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
            DialogResult = true;
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        ErrorTextBlock.Text = message;
        ErrorTextBlock.Visibility = Visibility.Visible;
    }
}
