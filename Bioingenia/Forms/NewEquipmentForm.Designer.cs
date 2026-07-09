namespace Bioingenieria.Forms;

partial class NewEquipmentForm
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
        serialLabel = new Label();
        serialTextBox = new TextBox();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        errorLabel = new Label();
        saveButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        //
        // serialLabel
        //
        serialLabel.AutoSize = true;
        serialLabel.Location = new System.Drawing.Point(20, 20);
        serialLabel.Name = "serialLabel";
        serialLabel.Size = new System.Drawing.Size(140, 20);
        serialLabel.TabIndex = 0;
        serialLabel.Text = "Número de serie";
        //
        // serialTextBox
        //
        serialTextBox.Location = new System.Drawing.Point(20, 43);
        serialTextBox.Name = "serialTextBox";
        serialTextBox.Size = new System.Drawing.Size(280, 27);
        serialTextBox.TabIndex = 1;
        //
        // nameLabel
        //
        nameLabel.AutoSize = true;
        nameLabel.Location = new System.Drawing.Point(20, 80);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new System.Drawing.Size(120, 20);
        nameLabel.TabIndex = 2;
        nameLabel.Text = "Nombre";
        //
        // nameTextBox
        //
        nameTextBox.Location = new System.Drawing.Point(20, 103);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new System.Drawing.Size(280, 27);
        nameTextBox.TabIndex = 3;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = System.Drawing.Color.Firebrick;
        errorLabel.Location = new System.Drawing.Point(20, 138);
        errorLabel.MaximumSize = new System.Drawing.Size(280, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 4;
        errorLabel.Visible = false;
        //
        // saveButton
        //
        saveButton.Location = new System.Drawing.Point(20, 180);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(130, 32);
        saveButton.TabIndex = 5;
        saveButton.Text = "Guardar";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        //
        // cancelButton
        //
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(170, 180);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(130, 32);
        cancelButton.TabIndex = 6;
        cancelButton.Text = "Cancelar";
        cancelButton.UseVisualStyleBackColor = true;
        //
        // NewEquipmentForm
        //
        AcceptButton = saveButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(320, 230);
        Controls.Add(serialLabel);
        Controls.Add(serialTextBox);
        Controls.Add(nameLabel);
        Controls.Add(nameTextBox);
        Controls.Add(errorLabel);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "NewEquipmentForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Nuevo equipo";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label serialLabel;
    private TextBox serialTextBox;
    private Label nameLabel;
    private TextBox nameTextBox;
    private Label errorLabel;
    private Button saveButton;
    private Button cancelButton;
}
