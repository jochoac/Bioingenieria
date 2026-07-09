using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Controls;

public partial class EquipmentCardControl : UserControl
{
    public EquipmentCardControl()
    {
        InitializeComponent();
    }

    public void SetEquipment(Equipment equipment)
    {
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
        Height = titleLabel.Height + chipsPanel.Height + Padding.Vertical;
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
}
