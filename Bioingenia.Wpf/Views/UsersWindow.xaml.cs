using System.Windows;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class UsersWindow : Window
{
    private readonly UserService _userService;

    public UsersWindow(UserService userService)
    {
        _userService = userService;
        InitializeComponent();
        LoadUsers();
    }

    private void LoadUsers()
    {
        UsersDataGrid.ItemsSource = _userService.GetAll()
            .Select(u => new UserRow(u.Username, u.Role, u.IsActive))
            .ToList();
    }

    private void NewUserButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new UserEditWindow(_userService, null) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadUsers();
        }
    }

    private void EditUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (UsersDataGrid.SelectedItem is not UserRow row)
        {
            MessageBox.Show(this, "Selecciona un usuario de la lista.", "Editar usuario", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var window = new UserEditWindow(_userService, row.Username) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadUsers();
        }
    }

    private sealed record UserRow(string Username, UserRole Role, bool IsActive)
    {
        public string ActiveDisplay => IsActive ? "Sí" : "No";
    }
}
