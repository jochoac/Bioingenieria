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

        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(Cursor.Position);
    }
}
