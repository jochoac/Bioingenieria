using System.Windows;
using Bioingenieria.Views;

namespace Bioingenieria;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Closing LoginWindow would otherwise trigger the default OnLastWindowClose shutdown
        // before MainWindow ever gets created, since it's the only open window at that point.
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

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

        var main = new MainWindow(login.EquipmentService, login.UserService, login.ScheduleService, login.AuthenticatedUser);
        MainWindow = main;
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        main.Show();
    }
}
