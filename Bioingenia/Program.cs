using Bioingenieria.Forms;

namespace Bioingenieria;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        using var loginForm = new LoginForm();
        if (loginForm.ShowDialog() != DialogResult.OK
            || loginForm.AuthenticatedUser is null
            || loginForm.EquipmentService is null
            || loginForm.UserService is null
            || loginForm.ScheduleService is null)
        {
            return;
        }

        Application.Run(new MainForm(loginForm.EquipmentService, loginForm.UserService, loginForm.ScheduleService, loginForm.AuthenticatedUser));
    }
}