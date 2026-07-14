namespace Bioingenieria.Forms;

partial class UsersForm
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
        usersGridView = new DataGridView();
        usernameColumn = new DataGridViewTextBoxColumn();
        roleColumn = new DataGridViewTextBoxColumn();
        activeColumn = new DataGridViewTextBoxColumn();
        bottomPanel = new Panel();
        closeButton = new Button();
        editUserButton = new Button();
        newUserButton = new Button();
        ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        //
        // usersGridView
        //
        usersGridView.AllowUserToAddRows = false;
        usersGridView.AllowUserToDeleteRows = false;
        usersGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        usersGridView.Columns.AddRange(new DataGridViewColumn[] { usernameColumn, roleColumn, activeColumn });
        usersGridView.Dock = DockStyle.Fill;
        usersGridView.Location = new System.Drawing.Point(0, 0);
        usersGridView.MultiSelect = false;
        usersGridView.Name = "usersGridView";
        usersGridView.ReadOnly = true;
        usersGridView.RowHeadersVisible = false;
        usersGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        usersGridView.Size = new System.Drawing.Size(560, 350);
        usersGridView.TabIndex = 0;
        //
        // usernameColumn
        //
        usernameColumn.HeaderText = "Usuario";
        usernameColumn.Name = "usernameColumn";
        usernameColumn.ReadOnly = true;
        usernameColumn.FillWeight = 40;
        //
        // roleColumn
        //
        roleColumn.HeaderText = "Rol";
        roleColumn.Name = "roleColumn";
        roleColumn.ReadOnly = true;
        roleColumn.FillWeight = 30;
        //
        // activeColumn
        //
        activeColumn.HeaderText = "Activo";
        activeColumn.Name = "activeColumn";
        activeColumn.ReadOnly = true;
        activeColumn.FillWeight = 30;
        //
        // bottomPanel
        //
        bottomPanel.Controls.Add(closeButton);
        bottomPanel.Controls.Add(editUserButton);
        bottomPanel.Controls.Add(newUserButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new System.Drawing.Point(0, 350);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Padding = new Padding(12);
        bottomPanel.Size = new System.Drawing.Size(560, 56);
        bottomPanel.TabIndex = 1;
        //
        // newUserButton
        //
        newUserButton.Location = new System.Drawing.Point(12, 12);
        newUserButton.Name = "newUserButton";
        newUserButton.Size = new System.Drawing.Size(140, 32);
        newUserButton.TabIndex = 0;
        newUserButton.Text = "Nuevo usuario";
        newUserButton.UseVisualStyleBackColor = true;
        newUserButton.Click += NewUserButton_Click;
        //
        // editUserButton
        //
        editUserButton.Location = new System.Drawing.Point(160, 12);
        editUserButton.Name = "editUserButton";
        editUserButton.Size = new System.Drawing.Size(140, 32);
        editUserButton.TabIndex = 1;
        editUserButton.Text = "Editar usuario";
        editUserButton.UseVisualStyleBackColor = true;
        editUserButton.Click += EditUserButton_Click;
        //
        // closeButton
        //
        closeButton.DialogResult = DialogResult.Cancel;
        closeButton.Location = new System.Drawing.Point(428, 12);
        closeButton.Name = "closeButton";
        closeButton.Size = new System.Drawing.Size(120, 32);
        closeButton.TabIndex = 2;
        closeButton.Text = "Cerrar";
        closeButton.UseVisualStyleBackColor = true;
        //
        // UsersForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = closeButton;
        ClientSize = new System.Drawing.Size(560, 406);
        Controls.Add(usersGridView);
        Controls.Add(bottomPanel);
        MinimumSize = new System.Drawing.Size(500, 350);
        Name = "UsersForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Gestionar usuarios";
        ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
        bottomPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private DataGridView usersGridView;
    private DataGridViewTextBoxColumn usernameColumn;
    private DataGridViewTextBoxColumn roleColumn;
    private DataGridViewTextBoxColumn activeColumn;
    private Panel bottomPanel;
    private Button newUserButton;
    private Button editUserButton;
    private Button closeButton;
}
