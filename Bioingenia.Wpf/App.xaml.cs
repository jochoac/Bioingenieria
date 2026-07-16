using System.Windows;
using Bioingenieria.Views;

namespace Bioingenieria;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var login = new LoginWindow();
        if (login.ShowDialog() != true
            || login.AuthenticatedUser is null
            || login.EquipmentService is null
            || login.UserService is null
            || login.ScheduleService is null)
        {
            Shutdown();
            return;
        }

        // MainWindow lands in the next migration step; for now, confirm login succeeded.
        MessageBox.Show(
            $"Login OK: {login.AuthenticatedUser.Username} ({login.AuthenticatedUser.Role})",
            "Verificación temporal",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        Shutdown();
    }
}
