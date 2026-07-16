using Bioingenieria.Theme;

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
        logoPictureBox = new PictureBox();
        titleLabel = new Label();
        usernameLabel = new Label();
        usernameTextBox = new TextBox();
        passwordLabel = new Label();
        passwordTextBox = new TextBox();
        loginButton = new Button();
        errorLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
        SuspendLayout();
        //
        // logoPictureBox
        //
        logoPictureBox.Location = new System.Drawing.Point(118, 20);
        logoPictureBox.Name = "logoPictureBox";
        logoPictureBox.Size = new System.Drawing.Size(84, 84);
        logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        logoPictureBox.TabIndex = 0;
        logoPictureBox.TabStop = false;
        //
        // titleLabel
        //
        titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        titleLabel.ForeColor = AppColors.PrimaryDark;
        titleLabel.Location = new System.Drawing.Point(10, 110);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new System.Drawing.Size(300, 30);
        titleLabel.TabIndex = 1;
        titleLabel.Text = "Bioingenia";
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        //
        // usernameLabel
        //
        usernameLabel.AutoSize = true;
        usernameLabel.ForeColor = AppColors.Text;
        usernameLabel.Location = new System.Drawing.Point(30, 155);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Size = new System.Drawing.Size(76, 20);
        usernameLabel.TabIndex = 2;
        usernameLabel.Text = "Usuario";
        //
        // usernameTextBox
        //
        usernameTextBox.Location = new System.Drawing.Point(30, 178);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new System.Drawing.Size(260, 27);
        usernameTextBox.TabIndex = 3;
        //
        // passwordLabel
        //
        passwordLabel.AutoSize = true;
        passwordLabel.ForeColor = AppColors.Text;
        passwordLabel.Location = new System.Drawing.Point(30, 215);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new System.Drawing.Size(102, 20);
        passwordLabel.TabIndex = 4;
        passwordLabel.Text = "Contraseña";
        //
        // passwordTextBox
        //
        passwordTextBox.Location = new System.Drawing.Point(30, 238);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.Size = new System.Drawing.Size(260, 27);
        passwordTextBox.TabIndex = 5;
        passwordTextBox.UseSystemPasswordChar = true;
        //
        // errorLabel
        //
        errorLabel.AutoSize = true;
        errorLabel.ForeColor = AppColors.Critical;
        errorLabel.Location = new System.Drawing.Point(30, 273);
        errorLabel.MaximumSize = new System.Drawing.Size(260, 0);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new System.Drawing.Size(0, 20);
        errorLabel.TabIndex = 6;
        errorLabel.Visible = false;
        //
        // loginButton
        //
        loginButton.BackColor = AppColors.Primary;
        loginButton.FlatAppearance.BorderSize = 0;
        loginButton.FlatAppearance.MouseOverBackColor = AppColors.PrimaryDark;
        loginButton.FlatStyle = FlatStyle.Flat;
        loginButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        loginButton.ForeColor = Color.White;
        loginButton.Location = new System.Drawing.Point(30, 303);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(260, 36);
        loginButton.TabIndex = 7;
        loginButton.Text = "Iniciar sesión";
        loginButton.UseVisualStyleBackColor = false;
        loginButton.Click += LoginButton_Click;
        //
        // LoginForm
        //
        AcceptButton = loginButton;
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = AppColors.Surface;
        ClientSize = new System.Drawing.Size(320, 370);
        Controls.Add(logoPictureBox);
        Controls.Add(titleLabel);
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
        ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox logoPictureBox;
    private Label titleLabel;
    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label passwordLabel;
    private TextBox passwordTextBox;
    private Button loginButton;
    private Label errorLabel;
}
