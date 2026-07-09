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
        resultsPanel = new FlowLayoutPanel();
        topPanel.SuspendLayout();
        SuspendLayout();
        //
        // topPanel
        //
        topPanel.Controls.Add(adminButton);
        topPanel.Controls.Add(searchButton);
        topPanel.Controls.Add(searchTextBox);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new System.Drawing.Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Padding = new Padding(12);
        topPanel.Size = new System.Drawing.Size(900, 56);
        topPanel.TabIndex = 0;
        //
        // searchTextBox
        //
        searchTextBox.Location = new System.Drawing.Point(12, 15);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Buscar por número de serie...";
        searchTextBox.Size = new System.Drawing.Size(300, 27);
        searchTextBox.TabIndex = 0;
        searchTextBox.KeyDown += SearchTextBox_KeyDown;
        //
        // searchButton
        //
        searchButton.Location = new System.Drawing.Point(320, 14);
        searchButton.Name = "searchButton";
        searchButton.Size = new System.Drawing.Size(100, 29);
        searchButton.TabIndex = 1;
        searchButton.Text = "Buscar";
        searchButton.UseVisualStyleBackColor = true;
        searchButton.Click += SearchButton_Click;
        //
        // adminButton
        //
        adminButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        adminButton.Location = new System.Drawing.Point(760, 14);
        adminButton.Name = "adminButton";
        adminButton.Size = new System.Drawing.Size(120, 29);
        adminButton.TabIndex = 2;
        adminButton.Text = "Administración";
        adminButton.UseVisualStyleBackColor = true;
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
        ClientSize = new System.Drawing.Size(900, 550);
        Controls.Add(resultsPanel);
        Controls.Add(topPanel);
        MinimumSize = new System.Drawing.Size(700, 400);
        Name = "MainForm";
        Text = "Buscador de Documentación por Equipo";
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel topPanel;
    private TextBox searchTextBox;
    private Button searchButton;
    private Button adminButton;
    private FlowLayoutPanel resultsPanel;
}
