using Bioingenieria.Services;
using Bioingenieria.Theme;

namespace Bioingenieria.Forms;

public partial class AdminForm : Form
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly ScheduleService _scheduleService;

    public AdminForm(EquipmentService equipmentService, UserService userService, ScheduleService scheduleService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _scheduleService = scheduleService;

        InitializeComponent();
        this.ApplyAppIcon();
        newEquipmentButton.ApplySecondaryStyle();
        uploadDocumentButton.ApplySecondaryStyle();
        manageUsersButton.ApplySecondaryStyle();
        schedulesButton.ApplySecondaryStyle();
        LoadTree();
    }

    private void LoadTree()
    {
        treeView.Nodes.Clear();

        foreach (var equipment in _equipmentService.GetAll())
        {
            var equipmentNode = new TreeNode($"{equipment.SerialNumber} — {equipment.Name}");

            foreach (var category in equipment.Categories)
            {
                var categoryNode = new TreeNode(category.DisplayName);

                foreach (var filePath in category.FilePaths)
                {
                    categoryNode.Nodes.Add(new TreeNode(Path.GetFileName(filePath)) { Tag = filePath });
                }

                equipmentNode.Nodes.Add(categoryNode);
            }

            treeView.Nodes.Add(equipmentNode);
        }

        treeView.ExpandAll();
    }

    private void TreeView_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node?.Tag is string filePath && File.Exists(filePath))
        {
            FileOpenerService.Open(filePath);
        }
    }

    private void NewEquipmentButton_Click(object? sender, EventArgs e)
    {
        using var form = new NewEquipmentForm(_equipmentService);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadTree();
        }
    }

    private void UploadDocumentButton_Click(object? sender, EventArgs e)
    {
        using var form = new UploadDocumentForm(_equipmentService);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadTree();
        }
    }

    private void ManageUsersButton_Click(object? sender, EventArgs e)
    {
        using var form = new UsersForm(_userService);
        form.ShowDialog(this);
    }

    private void SchedulesButton_Click(object? sender, EventArgs e)
    {
        using var form = new CronogramaForm(_scheduleService);
        form.ShowDialog(this);
    }
}
