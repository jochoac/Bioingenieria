using Bioingenieria.Theme;

namespace Bioingenieria.Forms;

partial class MainForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        topPanel = new Panel();
        adminButton = new Button();
        searchButton = new Button();
        searchTextBox = new TextBox();
        brandLabel = new Label();
        logoPictureBox = new PictureBox();
        resultsPanel = new FlowLayoutPanel();
        topPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
        SuspendLayout();
        //
        // topPanel
        //
        topPanel.BackColor = AppColors.Primary;
        topPanel.Controls.Add(adminButton);
        topPanel.Controls.Add(searchButton);
        topPanel.Controls.Add(searchTextBox);
        topPanel.Controls.Add(brandLabel);
        topPanel.Controls.Add(logoPictureBox);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new System.Drawing.Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Padding = new Padding(12);
        topPanel.Size = new System.Drawing.Size(900, 56);
        topPanel.TabIndex = 0;
        //
        // logoPictureBox
        //
        logoPictureBox.Location = new System.Drawing.Point(12, 10);
        logoPictureBox.Name = "logoPictureBox";
        logoPictureBox.Size = new System.Drawing.Size(36, 36);
        logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        logoPictureBox.TabIndex = 0;
        logoPictureBox.TabStop = false;
        //
        // brandLabel
        //
        brandLabel.AutoSize = true;
        brandLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        brandLabel.ForeColor = Color.White;
        brandLabel.Location = new System.Drawing.Point(54, 17);
        brandLabel.Name = "brandLabel";
        brandLabel.Size = new System.Drawing.Size(90, 20);
        brandLabel.TabIndex = 1;
        brandLabel.Text = "Bioingenia";
        //
        // searchTextBox
        //
        searchTextBox.Location = new System.Drawing.Point(180, 15);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Buscar por número de serie...";
        searchTextBox.Size = new System.Drawing.Size(260, 27);
        searchTextBox.TabIndex = 2;
        searchTextBox.KeyDown += SearchTextBox_KeyDown;
        //
        // searchButton
        //
        searchButton.BackColor = Color.White;
        searchButton.FlatAppearance.BorderSize = 0;
        searchButton.FlatStyle = FlatStyle.Flat;
        searchButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        searchButton.ForeColor = AppColors.PrimaryDark;
        searchButton.Location = new System.Drawing.Point(448, 14);
        searchButton.Name = "searchButton";
        searchButton.Size = new System.Drawing.Size(90, 29);
        searchButton.TabIndex = 3;
        searchButton.Text = "Buscar";
        searchButton.UseVisualStyleBackColor = false;
        searchButton.Click += SearchButton_Click;
        //
        // adminButton
        //
        adminButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        adminButton.BackColor = AppColors.PrimaryDark;
        adminButton.FlatAppearance.BorderColor = Color.White;
        adminButton.FlatAppearance.BorderSize = 1;
        adminButton.FlatStyle = FlatStyle.Flat;
        adminButton.ForeColor = Color.White;
        adminButton.Location = new System.Drawing.Point(748, 14);
        adminButton.Name = "adminButton";
        adminButton.Size = new System.Drawing.Size(140, 29);
        adminButton.TabIndex = 4;
        adminButton.Text = "Administración";
        adminButton.UseVisualStyleBackColor = false;
        adminButton.Click += AdminButton_Click;
        //
        // resultsPanel
        //
        resultsPanel.AutoScroll = true;
        resultsPanel.Dock = DockStyle.Fill;
        resultsPanel.FlowDirection = FlowDirection.TopDown;
        resultsPanel.Location = new System.Drawing.Point(0, 56);
        resultsPanel.Name = "resultsPanel";
        resultsPanel.Padding = new Padding(12);
        resultsPanel.Size = new System.Drawing.Size(900, 494);
        resultsPanel.TabIndex = 1;
        resultsPanel.WrapContents = false;
        //
        // MainForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = AppColors.Surface;
        ClientSize = new System.Drawing.Size(900, 550);
        Controls.Add(resultsPanel);
        Controls.Add(topPanel);
        MinimumSize = new System.Drawing.Size(700, 400);
        Name = "MainForm";
        Text = "Buscador de Documentación por Equipo";
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel topPanel;
    private PictureBox logoPictureBox;
    private Label brandLabel;
    private TextBox searchTextBox;
    private Button searchButton;
    private Button adminButton;
    private FlowLayoutPanel resultsPanel;
}
