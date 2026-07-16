using Bioingenieria.Controls;
using Bioingenieria.Theme;

namespace Bioingenieria.Forms;

partial class AdminForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        leftPanel = new Panel();
        schedulesButton = new Button();
        manageUsersButton = new Button();
        uploadDocumentButton = new Button();
        newEquipmentButton = new Button();
        treeView = new TreeView();
        headerControl = new FormHeaderControl();
        leftPanel.SuspendLayout();
        SuspendLayout();
        //
        // leftPanel
        //
        leftPanel.BackColor = Color.White;
        leftPanel.Controls.Add(schedulesButton);
        leftPanel.Controls.Add(manageUsersButton);
        leftPanel.Controls.Add(uploadDocumentButton);
        leftPanel.Controls.Add(newEquipmentButton);
        leftPanel.Dock = DockStyle.Left;
        leftPanel.Location = new System.Drawing.Point(0, 48);
        leftPanel.Name = "leftPanel";
        leftPanel.Padding = new Padding(12);
        leftPanel.Size = new System.Drawing.Size(190, 452);
        leftPanel.TabIndex = 1;
        //
        // newEquipmentButton
        //
        newEquipmentButton.Location = new System.Drawing.Point(12, 12);
        newEquipmentButton.Name = "newEquipmentButton";
        newEquipmentButton.Size = new System.Drawing.Size(160, 32);
        newEquipmentButton.TabIndex = 0;
        newEquipmentButton.Text = "Nuevo equipo";
        newEquipmentButton.Click += NewEquipmentButton_Click;
        //
        // uploadDocumentButton
        //
        uploadDocumentButton.Location = new System.Drawing.Point(12, 54);
        uploadDocumentButton.Name = "uploadDocumentButton";
        uploadDocumentButton.Size = new System.Drawing.Size(160, 32);
        uploadDocumentButton.TabIndex = 1;
        uploadDocumentButton.Text = "Subir documento";
        uploadDocumentButton.Click += UploadDocumentButton_Click;
        //
        // manageUsersButton
        //
        manageUsersButton.Location = new System.Drawing.Point(12, 96);
        manageUsersButton.Name = "manageUsersButton";
        manageUsersButton.Size = new System.Drawing.Size(160, 32);
        manageUsersButton.TabIndex = 2;
        manageUsersButton.Text = "Gestionar usuarios";
        manageUsersButton.Click += ManageUsersButton_Click;
        //
        // schedulesButton
        //
        schedulesButton.Location = new System.Drawing.Point(12, 138);
        schedulesButton.Name = "schedulesButton";
        schedulesButton.Size = new System.Drawing.Size(160, 32);
        schedulesButton.TabIndex = 3;
        schedulesButton.Text = "Cronogramas";
        schedulesButton.Click += SchedulesButton_Click;
        //
        // treeView
        //
        treeView.BorderStyle = BorderStyle.None;
        treeView.Dock = DockStyle.Fill;
        treeView.Location = new System.Drawing.Point(190, 48);
        treeView.Name = "treeView";
        treeView.Size = new System.Drawing.Size(510, 452);
        treeView.TabIndex = 2;
        treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
        //
        // headerControl
        //
        headerControl.Dock = DockStyle.Top;
        headerControl.Location = new System.Drawing.Point(0, 0);
        headerControl.Name = "headerControl";
        headerControl.Size = new System.Drawing.Size(700, 48);
        headerControl.TabIndex = 0;
        headerControl.Title = "Administración";
        //
        // AdminForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = AppColors.Surface;
        ClientSize = new System.Drawing.Size(700, 500);
        Controls.Add(treeView);
        Controls.Add(leftPanel);
        Controls.Add(headerControl);
        MinimumSize = new System.Drawing.Size(600, 400);
        Name = "AdminForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Administración";
        leftPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel leftPanel;
    private Button newEquipmentButton;
    private Button uploadDocumentButton;
    private Button manageUsersButton;
    private Button schedulesButton;
    private TreeView treeView;
    private FormHeaderControl headerControl;
}
