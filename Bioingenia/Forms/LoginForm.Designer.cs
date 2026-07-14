namespace Bioingenieria.Forms;

partial class LoginForm
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
        loginButton = new Button();
        errorLabel = new Label();
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
        usernameTextBox.Size = new System.Drawing.Size(260, 27);
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
        passwordTextBox.Size = new System.Drawing.Size(260, 27);
        passwordTextBox.TabIndex = 3;
        passwordTextBox.UseSystemPasswordChar = true;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = System.Drawing.Color.Firebrick;
        errorLabel.Location = new System.Drawing.Point(20, 138);
        errorLabel.MaximumSize = new System.Drawing.Size(260, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 4;
        errorLabel.Visible = false;
        //
        // loginButton
        //
        loginButton.Location = new System.Drawing.Point(20, 168);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(260, 32);
        loginButton.TabIndex = 5;
        loginButton.Text = "Iniciar sesión";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += LoginButton_Click;
        //
        // LoginForm
        //
        AcceptButton = loginButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(300, 220);
        Controls.Add(usernameLabel);
        Controls.Add(usernameTextBox);
        Controls.Add(passwordLabel);
        Controls.Add(passwordTextBox);
        Controls.Add(errorLabel);
        Controls.Add(loginButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Iniciar sesión";
        Load += LoginForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label passwordLabel;
    private TextBox passwordTextBox;
    private Button loginButton;
    private Label errorLabel;
}
