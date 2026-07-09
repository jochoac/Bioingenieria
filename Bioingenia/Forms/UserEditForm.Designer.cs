namespace Bioingenieria.Forms;

partial class UserEditForm
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
        usernameLabel = new Label();
        usernameTextBox = new TextBox();
        passwordLabel = new Label();
        passwordTextBox = new TextBox();
        roleLabel = new Label();
        roleComboBox = new ComboBox();
        activeCheckBox = new CheckBox();
        errorLabel = new Label();
        saveButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        //
        // usernameLabel
        //
        usernameLabel.AutoSize = true;
        usernameLabel.Location = new System.Drawing.Point(20, 20);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Size = new System.Drawing.Size(76, 20);
        usernameLabel.TabIndex = 0;
        usernameLabel.Text = "Usuario";
        //
        // usernameTextBox
        //
        usernameTextBox.Location = new System.Drawing.Point(20, 43);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new System.Drawing.Size(280, 27);
        usernameTextBox.TabIndex = 1;
        //
        // passwordLabel
        //
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new System.Drawing.Point(20, 80);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new System.Drawing.Size(102, 20);
        passwordLabel.TabIndex = 2;
        passwordLabel.Text = "Contraseña";
        //
        // passwordTextBox
        //
        passwordTextBox.Location = new System.Drawing.Point(20, 103);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.Size = new System.Drawing.Size(280, 27);
        passwordTextBox.TabIndex = 3;
        passwordTextBox.UseSystemPasswordChar = true;
        //
        // roleLabel
        //
        roleLabel.AutoSize = true;
        roleLabel.Location = new System.Drawing.Point(20, 140);
        roleLabel.Name = "roleLabel";
        roleLabel.Size = new System.Drawing.Size(40, 20);
        roleLabel.TabIndex = 4;
        roleLabel.Text = "Rol";
        //
        // roleComboBox
        //
        roleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        roleComboBox.Location = new System.Drawing.Point(20, 163);
        roleComboBox.Name = "roleComboBox";
        roleComboBox.Size = new System.Drawing.Size(280, 28);
        roleComboBox.TabIndex = 5;
        //
        // activeCheckBox
        //
        activeCheckBox.AutoSize = true;
        activeCheckBox.Location = new System.Drawing.Point(20, 200);
        activeCheckBox.Name = "activeCheckBox";
        activeCheckBox.Size = new System.Drawing.Size(70, 24);
        activeCheckBox.TabIndex = 6;
        activeCheckBox.Text = "Activo";
        activeCheckBox.UseVisualStyleBackColor = true;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = System.Drawing.Color.Firebrick;
        errorLabel.Location = new System.Drawing.Point(20, 232);
        errorLabel.MaximumSize = new System.Drawing.Size(280, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 7;
        errorLabel.Visible = false;
        //
        // saveButton
        //
        saveButton.Location = new System.Drawing.Point(20, 268);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(130, 32);
        saveButton.TabIndex = 8;
        saveButton.Text = "Guardar";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        //
        // cancelButton
        //
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new System.Drawing.Point(170, 268);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(130, 32);
        cancelButton.TabIndex = 9;
        cancelButton.Text = "Cancelar";
        cancelButton.UseVisualStyleBackColor = true;
        //
        // UserEditForm
        //
        AcceptButton = saveButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(320, 320);
        Controls.Add(usernameLabel);
        Controls.Add(usernameTextBox);
        Controls.Add(passwordLabel);
        Controls.Add(passwordTextBox);
        Controls.Add(roleLabel);
        Controls.Add(roleComboBox);
        Controls.Add(activeCheckBox);
        Controls.Add(errorLabel);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UserEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Usuario";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label passwordLabel;
    private TextBox passwordTextBox;
    private Label roleLabel;
    private ComboBox roleComboBox;
    private CheckBox activeCheckBox;
    private Label errorLabel;
    private Button saveButton;
    private Button cancelButton;
}
