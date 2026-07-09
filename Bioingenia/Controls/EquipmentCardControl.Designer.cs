namespace Bioingenieria.Controls;

partial class EquipmentCardControl
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        titleLabel = new Label();
        chipsPanel = new FlowLayoutPanel();
        SuspendLayout();
        //
        // titleLabel
        //
        titleLabel.AutoSize = true;
        titleLabel.Dock = DockStyle.Top;
        titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        titleLabel.Location = new Point(8, 8);
        titleLabel.Name = "titleLabel";
        titleLabel.Padding = new Padding(0, 0, 0, 6);
        titleLabel.Size = new Size(100, 27);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "titleLabel";
        //
        // chipsPanel
        //
        chipsPanel.AutoSize = true;
        chipsPanel.Dock = DockStyle.Top;
        chipsPanel.FlowDirection = FlowDirection.LeftToRight;
        chipsPanel.Location = new Point(8, 35);
        chipsPanel.Name = "chipsPanel";
        chipsPanel.Size = new Size(300, 40);
        chipsPanel.TabIndex = 1;
        chipsPanel.WrapContents = true;
        //
        // EquipmentCardControl
        //
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(chipsPanel);
        Controls.Add(titleLabel);
        Margin = new Padding(8);
        Name = "EquipmentCardControl";
        Padding = new Padding(8);
        Size = new Size(360, 90);
        ResumeLayout(false);
        PerformLayout();
    }

    private Label titleLabel;
    private FlowLayoutPanel chipsPanel;
}
