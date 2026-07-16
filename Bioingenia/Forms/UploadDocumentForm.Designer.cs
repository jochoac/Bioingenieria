using Bioingenieria.Controls;
using Bioingenieria.Theme;

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
        headerControl = new FormHeaderControl();
        SuspendLayout();
        //
        // equipmentLabel
        //
        equipmentLabel.AutoSize = true;
        equipmentLabel.ForeColor = AppColors.Text;
        equipmentLabel.Location = new System.Drawing.Point(20, 68);
        equipmentLabel.Name = "equipmentLabel";
        equipmentLabel.Size = new System.Drawing.Size(70, 20);
        equipmentLabel.TabIndex = 0;
        equipmentLabel.Text = "Equipo";
        //
        // equipmentComboBox
        //
        equipmentComboBox.Location = new System.Drawing.Point(20, 91);
        equipmentComboBox.Name = "equipmentComboBox";
        equipmentComboBox.Size = new System.Drawing.Size(360, 28);
        equipmentComboBox.TabIndex = 1;
        //
        // categoryLabel
        //
        categoryLabel.AutoSize = true;
        categoryLabel.ForeColor = AppColors.Text;
        categoryLabel.Location = new System.Drawing.Point(20, 128);
        categoryLabel.Name = "categoryLabel";
        categoryLabel.Size = new System.Drawing.Size(90, 20);
        categoryLabel.TabIndex = 2;
        categoryLabel.Text = "Categoría";
        //
        // categoryComboBox
        //
        categoryComboBox.DropDownStyle = ComboBoxStyle.DropDown;
        categoryComboBox.Location = new System.Drawing.Point(20, 151);
        categoryComboBox.Name = "categoryComboBox";
        categoryComboBox.Size = new System.Drawing.Size(360, 28);
        categoryComboBox.TabIndex = 3;
        //
        // filePathLabel
        //
        filePathLabel.AutoSize = true;
        filePathLabel.ForeColor = AppColors.Text;
        filePathLabel.Location = new System.Drawing.Point(20, 188);
        filePathLabel.Name = "filePathLabel";
        filePathLabel.Size = new System.Drawing.Size(70, 20);
        filePathLabel.TabIndex = 4;
        filePathLabel.Text = "Archivo";
        //
        // filePathTextBox
        //
        filePathTextBox.Location = new System.Drawing.Point(20, 211);
        filePathTextBox.Name = "filePathTextBox";
        filePathTextBox.ReadOnly = true;
        filePathTextBox.Size = new System.Drawing.Size(270, 27);
        filePathTextBox.TabIndex = 5;
        //
        // browseButton
        //
        browseButton.Location = new System.Drawing.Point(300, 210);
        browseButton.Name = "browseButton";
        browseButton.Size = new System.Drawing.Size(80, 29);
        browseButton.TabIndex = 6;
        browseButton.Text = "Buscar...";
        browseButton.Click += BrowseButton_Click;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = AppColors.Critical;
        errorLabel.Location = new System.Drawing.Point(20, 246);
        errorLabel.MaximumSize = new System.Drawing.Size(360, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 7;
        errorLabel.Visible = false;
        //
        // saveButton
        //
        saveButton.Location = new System.Drawing.Point(20, 284);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(175, 32);
        saveButton.TabIndex = 8;
        saveButton.Text = "Guardar";
        saveButton.Click += SaveButton_Click;
        //
        // cancelButton
        //
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(205, 284);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(175, 32);
        cancelButton.TabIndex = 9;
        cancelButton.Text = "Cancelar";
        //
        // headerControl
        //
        headerControl.Dock = DockStyle.Top;
        headerControl.Location = new System.Drawing.Point(0, 0);
        headerControl.Name = "headerControl";
        headerControl.Size = new System.Drawing.Size(400, 48);
        headerControl.TabIndex = 10;
        headerControl.Title = "Subir documento";
        //
        // UploadDocumentForm
        //
        AcceptButton = saveButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = AppColors.Surface;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(400, 338);
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
        Controls.Add(headerControl);
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
    private FormHeaderControl headerControl;
}
