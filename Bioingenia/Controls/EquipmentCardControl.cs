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
        actionsPanel.Visible = isAdmin;
    }

    public void SetEquipment(Equipment equipment)
    {
        _serialNumber = equipment.SerialNumber;
        titleLabel.Text = $"{equipment.SerialNumber} — {equipment.Name}";
        chipsPanel.Controls.Clear();

        foreach (var category in equipment.Categories)
        {
            var chip = new Button
            {
                Text = category.DisplayName,
                AutoSize = true,
                Margin = new Padding(4),
                UseVisualStyleBackColor = true
            };

            chip.Click += (_, _) => OnCategoryChipClick(category);
            chipsPanel.Controls.Add(chip);
        }

        PerformLayout();
        var actionsHeight = actionsPanel.Visible ? actionsPanel.Height : 0;
        Height = titleLabel.Height + chipsPanel.Height + actionsHeight + Padding.Vertical;
    }

    private static void OnCategoryChipClick(DocumentCategory category)
    {
        if (category.FilePaths.Count == 1)
        {
            FileOpenerService.Open(category.FilePaths[0]);
            return;
        }

        var menu = new ContextMenuStrip();
        foreach (var filePath in category.FilePaths)
        {
            menu.Items.Add(Path.GetFileName(filePath), null, (_, _) => FileOpenerService.Open(filePath));
        }

        menu.Closed += Menu_Closed;
        menu.Show(Cursor.Position);
    }

    private static void Menu_Closed(object? sender, ToolStripDropDownClosedEventArgs e)
    {
        // Disposing synchronously here races with ToolStripManager's internal teardown of the
        // dropdown (it still touches the control's Handle after Closed fires) and crashes the
        // app with an ObjectDisposedException. Deferring past the current message avoids that.
        var menu = (ContextMenuStrip)sender!;
        menu.BeginInvoke((MethodInvoker)menu.Dispose);
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
        if (_serialNumber is null)
        {
            return;
        }

        var confirm = MessageBox.Show(
            this,
            $"¿Seguro que deseas eliminar el equipo '{_serialNumber}' y toda su documentación asociada? Esta acción no se puede deshacer.",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _equipmentService.DeleteEquipment(_serialNumber);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error al eliminar equipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // MainForm disposes this card (including this button) when it refreshes results in
        // response to EquipmentDeleted; doing that synchronously while still inside this
        // control's own Click handler risks touching a disposed Handle, same class of bug
        // as Menu_Closed above. Defer past the current message to avoid it.
        BeginInvoke((MethodInvoker)(() => EquipmentDeleted?.Invoke(this, EventArgs.Empty)));
    }
}