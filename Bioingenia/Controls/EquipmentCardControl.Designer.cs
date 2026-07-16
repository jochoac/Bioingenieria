using Bioingenieria.Theme;

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
        actionsPanel = new FlowLayoutPanel();
        deleteButton = new Button();
        actionsPanel.SuspendLayout();
        SuspendLayout();
        //
        // titleLabel
        //
        titleLabel.AutoSize = true;
        titleLabel.Dock = DockStyle.Top;
        titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        titleLabel.ForeColor = AppColors.PrimaryDark;
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
        // actionsPanel
        //
        actionsPanel.AutoSize = true;
        actionsPanel.Controls.Add(deleteButton);
        actionsPanel.Dock = DockStyle.Bottom;
        actionsPanel.FlowDirection = FlowDirection.RightToLeft;
        actionsPanel.Location = new Point(8, 75);
        actionsPanel.Name = "actionsPanel";
        actionsPanel.Size = new Size(300, 33);
        actionsPanel.TabIndex = 2;
        actionsPanel.WrapContents = false;
        //
        // deleteButton
        //
        deleteButton.AutoSize = true;
        deleteButton.Margin = new Padding(4);
        deleteButton.Name = "deleteButton";
        deleteButton.TabIndex = 0;
        deleteButton.Text = "Eliminar";
        deleteButton.Click += deleteButton_Click;
        //
        // EquipmentCardControl
        //
        BackColor = Color.White;
        BorderStyle = BorderStyle.None;
        Controls.Add(chipsPanel);
        Controls.Add(titleLabel);
        Controls.Add(actionsPanel);
        Margin = new Padding(8);
        Name = "EquipmentCardControl";
        Padding = new Padding(14, 10, 10, 10);
        Size = new Size(360, 123);
        actionsPanel.ResumeLayout(false);
        actionsPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label titleLabel;
    private FlowLayoutPanel chipsPanel;
    private FlowLayoutPanel actionsPanel;
    private Button deleteButton;
}
