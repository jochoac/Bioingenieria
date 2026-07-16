using System.Windows;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class LoginWindow : Window
{
    public User? AuthenticatedUser { get; private set; }
    public EquipmentService? EquipmentService { get; private set; }
    public UserService? UserService { get; private set; }
    public ScheduleService? ScheduleService { get; private set; }

    private string _equipmentRootPath = string.Empty;

    public LoginWindow()
    {
        InitializeComponent();
        Loaded += LoginWindow_Loaded;
    }

    private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
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
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        using var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = "Carpeta raíz de equipos (EQUIPOS_ROOT)"
        };

        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
        {
            return false;
        }

        _equipmentRootPath = dialog.SelectedPath;
        config.EquipmentRootPath = _equipmentRootPath;
        ConfigService.Save(config);
        return true;
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (UserService is null)
        {
            return;
        }

        var authService = new AuthService(UserService);
        var user = authService.TryLogin(UsernameTextBox.Text.Trim(), PasswordBox.Password);

        if (user is null)
        {
            ErrorTextBlock.Text = "Usuario o contraseña incorrectos.";
            ErrorTextBlock.Visibility = Visibility.Visible;
            return;
        }

        AuthenticatedUser = user;
        EquipmentService = new EquipmentService(_equipmentRootPath);
        ScheduleService = new ScheduleService(_equipmentRootPath);
        DialogResult = true;
    }
}
