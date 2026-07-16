using System.Windows;
using System.Windows.Input;
using Bioingenieria.Controls;
using Bioingenieria.Models;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class MainWindow : Window
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly ScheduleService _scheduleService;
    private readonly User _currentUser;

    public MainWindow(EquipmentService equipmentService, UserService userService, ScheduleService scheduleService, User currentUser)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _scheduleService = scheduleService;
        _currentUser = currentUser;

        InitializeComponent();

        AdminButton.Visibility = _currentUser.Role == UserRole.Administrator ? Visibility.Visible : Visibility.Collapsed;
        Title = $"Buscador de Documentación por Equipo — {_currentUser.Username}";

        DisplayResults(_equipmentService.GetAll());
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        DisplayResults(_equipmentService.SearchBySerial(SearchTextBox.Text));
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        e.Handled = true;
        DisplayResults(_equipmentService.SearchBySerial(SearchTextBox.Text));
    }

    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        var adminWindow = new AdminWindow(_equipmentService, _userService, _scheduleService) { Owner = this };
        adminWindow.ShowDialog();

        DisplayResults(_equipmentService.SearchBySerial(SearchTextBox.Text));
    }

    private void DisplayResults(IReadOnlyList<Equipment> equipments)
    {
        ResultsItemsControl.Items.Clear();

        var isAdmin = _currentUser.Role == UserRole.Administrator;

        foreach (var equipment in equipments)
        {
            var card = new EquipmentCardControl(_equipmentService, isAdmin);
            card.SetEquipment(equipment);
            card.EquipmentDeleted += (_, _) => DisplayResults(_equipmentService.SearchBySerial(SearchTextBox.Text));
            ResultsItemsControl.Items.Add(card);
        }
    }
}
