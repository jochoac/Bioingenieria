using System.Windows;
using System.Windows.Controls;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Controls;

public partial class EquipmentCardControl : UserControl
{
    private readonly EquipmentService _equipmentService;
    private string? _serialNumber;

    public event EventHandler? EquipmentDeleted;

    public EquipmentCardControl(EquipmentService equipmentService, bool isAdmin)
    {
        _equipmentService = equipmentService;
        InitializeComponent();
        ActionsPanel.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
    }

    public void SetEquipment(Equipment equipment)
    {
        _serialNumber = equipment.SerialNumber;
        TitleTextBlock.Text = $"{equipment.SerialNumber} — {equipment.Name}";
        ChipsPanel.Children.Clear();

        foreach (var category in equipment.Categories)
        {
            var chip = new Button
            {
                Content = category.DisplayName,
                Style = (Style)FindResource("ChipButtonStyle")
            };

            chip.Click += (_, _) => OnCategoryChipClick(chip, category);
            ChipsPanel.Children.Add(chip);
        }
    }

    private static void OnCategoryChipClick(Button chip, DocumentCategory category)
    {
        if (category.FilePaths.Count == 1)
        {
            FileOpenerService.Open(category.FilePaths[0]);
            return;
        }

        var menu = new ContextMenu();
        foreach (var filePath in category.FilePaths)
        {
            var item = new MenuItem { Header = Path.GetFileName(filePath) };
            item.Click += (_, _) => FileOpenerService.Open(filePath);
            menu.Items.Add(item);
        }

        chip.ContextMenu = menu;
        menu.PlacementTarget = chip;
        menu.IsOpen = true;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_serialNumber is null)
        {
            return;
        }

        var confirm = MessageBox.Show(
            Window.GetWindow(this),
            $"¿Seguro que deseas eliminar el equipo '{_serialNumber}' y toda su documentación asociada? Esta acción no se puede deshacer.",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirm != MessageBoxResult.Yes)
        {
            return;
        }

        try
        {
            _equipmentService.DeleteEquipment(_serialNumber);
        }
        catch (Exception ex)
        {
            MessageBox.Show(Window.GetWindow(this), ex.Message, "Error al eliminar equipo", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        EquipmentDeleted?.Invoke(this, EventArgs.Empty);
    }
}
