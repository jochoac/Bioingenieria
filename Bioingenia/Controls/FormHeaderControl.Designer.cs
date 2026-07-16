using Bioingenieria.Theme;

namespace Bioingenieria.Controls;

partial class FormHeaderControl
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
        accentStrip = new Panel();
        SuspendLayout();
        //
        // titleLabel
        //
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Location = new Point(0, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Padding = new Padding(20, 0, 20, 0);
        titleLabel.Size = new Size(500, 45);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "titleLabel";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // accentStrip
        //
        accentStrip.BackColor = AppColors.Accent;
        accentStrip.Dock = DockStyle.Bottom;
        accentStrip.Location = new Point(0, 45);
        accentStrip.Name = "accentStrip";
        accentStrip.Size = new Size(500, 3);
        accentStrip.TabIndex = 1;
        //
        // FormHeaderControl
        //
        BackColor = AppColors.Primary;
        Controls.Add(titleLabel);
        Controls.Add(accentStrip);
        Name = "FormHeaderControl";
        Size = new Size(500, 48);
        ResumeLayout(false);
    }

    private Label titleLabel;
    private Panel accentStrip;
}