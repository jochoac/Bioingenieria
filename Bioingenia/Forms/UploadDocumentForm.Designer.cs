namespace Bioingenieria.Forms;

partial class UploadDocumentForm
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
        equipmentLabel = new Label();
        equipmentComboBox = new ComboBox();
        categoryLabel = new Label();
        categoryComboBox = new ComboBox();
        filePathLabel = new Label();
        filePathTextBox = new TextBox();
        browseButton = new Button();
        errorLabel = new Label();
        saveButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        //
        // equipmentLabel
        //
        equipmentLabel.AutoSize = true;
        equipmentLabel.Location = new System.Drawing.Point(20, 20);
        equipmentLabel.Name = "equipmentLabel";
        equipmentLabel.Size = new System.Drawing.Size(70, 20);
        equipmentLabel.TabIndex = 0;
        equipmentLabel.Text = "Equipo";
        //
        // equipmentComboBox
        //
        equipmentComboBox.Location = new System.Drawing.Point(20, 43);
        equipmentComboBox.Name = "equipmentComboBox";
        equipmentComboBox.Size = new System.Drawing.Size(360, 28);
        equipmentComboBox.TabIndex = 1;
        //
        // categoryLabel
        //
        categoryLabel.AutoSize = true;
        categoryLabel.Location = new System.Drawing.Point(20, 80);
        categoryLabel.Name = "categoryLabel";
        categoryLabel.Size = new System.Drawing.Size(90, 20);
        categoryLabel.TabIndex = 2;
        categoryLabel.Text = "Categoría";
        //
        // categoryComboBox
        //
        categoryComboBox.Location = new System.Drawing.Point(20, 103);
        categoryComboBox.Name = "categoryComboBox";
        categoryComboBox.Size = new System.Drawing.Size(360, 28);
        categoryComboBox.TabIndex = 3;
        //
        // filePathLabel
        //
        filePathLabel.AutoSize = true;
        filePathLabel.Location = new System.Drawing.Point(20, 140);
        filePathLabel.Name = "filePathLabel";
        filePathLabel.Size = new System.Drawing.Size(70, 20);
        filePathLabel.TabIndex = 4;
        filePathLabel.Text = "Archivo";
        //
        // filePathTextBox
        //
        filePathTextBox.Location = new System.Drawing.Point(20, 163);
        filePathTextBox.Name = "filePathTextBox";
        filePathTextBox.ReadOnly = true;
        filePathTextBox.Size = new System.Drawing.Size(270, 27);
        filePathTextBox.TabIndex = 5;
        //
        // browseButton
        //
        browseButton.Location = new System.Drawing.Point(300, 162);
        browseButton.Name = "browseButton";
        browseButton.Size = new System.Drawing.Size(80, 29);
        browseButton.TabIndex = 6;
        browseButton.Text = "Buscar...";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += BrowseButton_Click;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = System.Drawing.Color.Firebrick;
        errorLabel.Location = new System.Drawing.Point(20, 198);
        errorLabel.MaximumSize = new System.Drawing.Size(360, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 7;
        errorLabel.Visible = false;
        //
        // saveButton
        //
        saveButton.Location = new System.Drawing.Point(20, 236);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(175, 32);
        saveButton.TabIndex = 8;
        saveButton.Text = "Guardar";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        //
        // cancelButton
        //
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(205, 236);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(175, 32);
        cancelButton.TabIndex = 9;
        cancelButton.Text = "Cancelar";
        cancelButton.UseVisualStyleBackColor = true;
        //
        // UploadDocumentForm
        //
        AcceptButton = saveButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(400, 290);
        Controls.Add(equipmentLabel);
        Controls.Add(equipmentComboBox);
        Controls.Add(categoryLabel);
        Controls.Add(categoryComboBox);
        Controls.Add(filePathLabel);
        Controls.Add(filePathTextBox);
        Controls.Add(browseButton);
        Controls.Add(errorLabel);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UploadDocumentForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Subir documento";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label equipmentLabel;
    private ComboBox equipmentComboBox;
    private Label categoryLabel;
    private ComboBox categoryComboBox;
    private Label filePathLabel;
    private TextBox filePathTextBox;
    private Button browseButton;
    private Label errorLabel;
    private Button saveButton;
    private Button cancelButton;
}
