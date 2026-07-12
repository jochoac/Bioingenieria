using Bioingenieria.Controls;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Forms;

public partial class MainForm : Form
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly User _currentUser;

    public MainForm(EquipmentService equipmentService, UserService userService, User currentUser)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _currentUser = currentUser;

        InitializeComponent();

        adminButton.Visible = _currentUser.Role == UserRole.Administrator;
        Text = $"Buscador de Documentación por Equipo — {_currentUser.Username}";

        DisplayResults(_equipmentService.GetAll());
    }

    private void SearchButton_Click(object? sender, EventArgs e)
    {
        DisplayResults(_equipmentService.SearchBySerial(searchTextBox.Text));
    }

    private void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        DisplayResults(_equipmentService.SearchBySerial(searchTextBox.Text));
    }

    private void AdminButton_Click(object? sender, EventArgs e)
    {
        using var adminForm = new AdminForm(_equipmentService, _userService);
        adminForm.ShowDialog(this);

        DisplayResults(_equipmentService.SearchBySerial(searchTextBox.Text));
    }

    private void DisplayResults(IReadOnlyList<Equipment> equipments)
    {
        resultsPanel.SuspendLayout();

        foreach (Control control in resultsPanel.Controls)
        {
            control.Dispose();
        }

        resultsPanel.Controls.Clear();

        var cardWidth = resultsPanel.ClientSize.Width
                        - resultsPanel.Padding.Horizontal
                        - SystemInformation.VerticalScrollBarWidth;
        var isAdmin = _currentUser.Role == UserRole.Administrator;

        foreach (var equipment in equipments)
        {
            var card = new EquipmentCardControl(_equipmentService, isAdmin) { Width = Math.Max(cardWidth, 250) };
            card.SetEquipment(equipment);
            card.EquipmentDeleted += (_, _) => DisplayResults(_equipmentService.SearchBySerial(searchTextBox.Text));
            resultsPanel.Controls.Add(card);
        }

        resultsPanel.ResumeLayout();
    }
}