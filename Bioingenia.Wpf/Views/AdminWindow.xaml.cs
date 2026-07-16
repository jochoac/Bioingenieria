using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bioingenieria.Services;

namespace Bioingenieria.Views;

public partial class AdminWindow : Window
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly ScheduleService _scheduleService;

    public AdminWindow(EquipmentService equipmentService, UserService userService, ScheduleService scheduleService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _scheduleService = scheduleService;

        InitializeComponent();
        LoadTree();
    }

    private void LoadTree()
    {
        EquipmentTreeView.Items.Clear();

        foreach (var equipment in _equipmentService.GetAll())
        {
            var equipmentItem = new TreeViewItem { Header = $"{equipment.SerialNumber} — {equipment.Name}", IsExpanded = true };

            foreach (var category in equipment.Categories)
            {
                var categoryItem = new TreeViewItem { Header = category.DisplayName, IsExpanded = true };

                foreach (var filePath in category.FilePaths)
                {
                    categoryItem.Items.Add(new TreeViewItem { Header = Path.GetFileName(filePath), Tag = filePath });
                }

                equipmentItem.Items.Add(categoryItem);
            }

            EquipmentTreeView.Items.Add(equipmentItem);
        }
    }

    private void EquipmentTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (EquipmentTreeView.SelectedItem is TreeViewItem { Tag: string filePath } && File.Exists(filePath))
        {
            try
            {
                FileOpenerService.Open(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void NewEquipmentButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new NewEquipmentWindow(_equipmentService) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadTree();
        }
    }

    private void UploadDocumentButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new UploadDocumentWindow(_equipmentService) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadTree();
        }
    }

    private void ManageUsersButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new UsersWindow(_userService) { Owner = this };
        window.ShowDialog();
    }

    private void SchedulesButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new CronogramaWindow(_scheduleService) { Owner = this };
        window.ShowDialog();
    }
}
