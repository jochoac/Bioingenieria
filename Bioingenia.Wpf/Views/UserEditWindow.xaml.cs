using System.Windows;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class UserEditWindow : Window
{
    private readonly UserService _userService;
    private readonly string? _existingUsername;

    public UserEditWindow(UserService userService, string? existingUsername)
    {
        _userService = userService;
        _existingUsername = existingUsername;

        InitializeComponent();
        RoleComboBox.ItemsSource = Enum.GetValues<UserRole>();

        if (_existingUsername is not null)
        {
            var user = _userService.FindByUsername(_existingUsername);
            if (user is not null)
            {
                UsernameTextBox.Text = user.Username;
                UsernameTextBox.IsReadOnly = true;
                RoleComboBox.SelectedItem = user.Role;
                ActiveCheckBox.IsChecked = user.IsActive;
            }

            Title = "Editar usuario";
            HeaderControl.Title = "Editar usuario";
            PasswordLabel.Text = "Nueva contraseña (opcional)";
        }
        else
        {
            RoleComboBox.SelectedItem = UserRole.Searcher;
            ActiveCheckBox.IsChecked = true;
            Title = "Nuevo usuario";
            HeaderControl.Title = "Nuevo usuario";
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text.Trim();
        var password = PasswordBox.Password;
        var role = (UserRole)RoleComboBox.SelectedItem!;
        var isActive = ActiveCheckBox.IsChecked == true;

        if (string.IsNullOrWhiteSpace(username))
        {
            ShowError("El nombre de usuario es obligatorio.");
            return;
        }

        try
        {
            if (_existingUsername is null)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    ShowError("La contraseña es obligatoria para un usuario nuevo.");
                    return;
                }

                _userService.AddUser(username, password, role);
                if (!isActive)
                {
                    _userService.SetRoleAndStatus(username, role, false);
                }
            }
            else
            {
                _userService.SetRoleAndStatus(_existingUsername, role, isActive);
                if (!string.IsNullOrWhiteSpace(password))
                {
                    _userService.ResetPassword(_existingUsername, password);
                }
            }

            DialogResult = true;
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        ErrorTextBlock.Text = message;
        ErrorTextBlock.Visibility = Visibility.Visible;
    }
}
