using Bioingenieria.Models;
using Bioingenieria.Services;
using Bioingenieria.Theme;

namespace Bioingenieria.Forms;

public partial class LoginForm : Form
{
    public User? AuthenticatedUser { get; private set; }
    public EquipmentService? EquipmentService { get; private set; }
    public UserService? UserService { get; private set; }
    public ScheduleService? ScheduleService { get; private set; }

    private string _equipmentRootPath = string.Empty;

    public LoginForm()
    {
        InitializeComponent();
        this.ApplyAppIcon();
        logoPictureBox.LoadAppLogo();
    }

    private void LoginForm_Load(object? sender, EventArgs e)
    {
        if (!EnsureEquipmentRoot())
        {
            Close();
            return;
        }

        UserService = new UserService(ConfigService.UsersFilePath(_equipmentRootPath));
    }

    private bool EnsureEquipmentRoot()
    {
        var config = ConfigService.Load();
        if (ConfigService.IsEquipmentRootValid(config.EquipmentRootPath))
        {
            _equipmentRootPath = config.EquipmentRootPath;
            return true;
        }

        MessageBox.Show(
            this,
            "Selecciona la carpeta raíz donde se guarda la documentación de los equipos.",
            "Configuración inicial",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        using var dialog = new FolderBrowserDialog
        {
            Description = "Carpeta raíz de equipos (EQUIPOS_ROOT)"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return false;
        }

        _equipmentRootPath = dialog.SelectedPath;
        config.EquipmentRootPath = _equipmentRootPath;
        ConfigService.Save(config);
        return true;
    }

    private void LoginButton_Click(object? sender, EventArgs e)
    {
        if (UserService is null)
        {
            return;
        }

        var authService = new AuthService(UserService);
        var user = authService.TryLogin(usernameTextBox.Text.Trim(), passwordTextBox.Text);

        if (user is null)
        {
            errorLabel.Text = "Usuario o contraseña incorrectos.";
            errorLabel.Visible = true;
            return;
        }

        AuthenticatedUser = user;
        EquipmentService = new EquipmentService(_equipmentRootPath);
        ScheduleService = new ScheduleService(_equipmentRootPath);
        DialogResult = DialogResult.OK;
        Close();
    }
}
