using Bioingenieria.Services;

namespace Bioingenieria.Forms;

public partial class UsersForm : Form
{
    private readonly UserService _userService;

    public UsersForm(UserService userService)
    {
        _userService = userService;
        InitializeComponent();
        LoadUsers();
    }

    private void LoadUsers()
    {
        usersGridView.Rows.Clear();

        foreach (var user in _userService.GetAll())
        {
            usersGridView.Rows.Add(user.Username, user.Role, user.IsActive ? "Sí" : "No");
        }
    }

    private void NewUserButton_Click(object? sender, EventArgs e)
    {
        using var form = new UserEditForm(_userService, null);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadUsers();
        }
    }

    private void EditUserButton_Click(object? sender, EventArgs e)
    {
        var username = usersGridView.CurrentRow?.Cells[0].Value as string;
        if (username is null)
        {
            MessageBox.Show(this, "Selecciona un usuario de la lista.", "Editar usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var form = new UserEditForm(_userService, username);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadUsers();
        }
    }
}
