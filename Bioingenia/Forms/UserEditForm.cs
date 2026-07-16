using Bioingenieria.Models;
using Bioingenieria.Services;
using Bioingenieria.Theme;

namespace Bioingenieria.Forms;

public partial class UserEditForm : Form
{
    private readonly UserService _userService;
    private readonly string? _existingUsername;

    public UserEditForm(UserService userService, string? existingUsername)
    {
        _userService = userService;
        _existingUsername = existingUsername;

        InitializeComponent();
        this.ApplyAppIcon();
        saveButton.ApplyPrimaryStyle();
        cancelButton.ApplySecondaryStyle();
        roleComboBox.Items.AddRange(Enum.GetValues<UserRole>().Cast<object>().ToArray());

        if (_existingUsername is not null)
        {
            var user = _userService.FindByUsername(_existingUsername);
            if (user is not null)
            {
                usernameTextBox.Text = user.Username;
                usernameTextBox.ReadOnly = true;
                roleComboBox.SelectedItem = user.Role;
                activeCheckBox.Checked = user.IsActive;
            }

            Text = "Editar usuario";
            passwordLabel.Text = "Nueva contraseña (opcional)";
        }
        else
        {
            roleComboBox.SelectedItem = UserRole.Searcher;
            activeCheckBox.Checked = true;
            Text = "Nuevo usuario";
        }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        var username = usernameTextBox.Text.Trim();
        var password = passwordTextBox.Text;
        var role = (UserRole)roleComboBox.SelectedItem!;
        var isActive = activeCheckBox.Checked;

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

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        errorLabel.Text = message;
        errorLabel.Visible = true;
    }
}
