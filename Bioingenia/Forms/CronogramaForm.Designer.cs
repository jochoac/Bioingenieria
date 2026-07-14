using ScottPlot.WinForms;

namespace Bioingenieria.Forms;

partial class CronogramaForm
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
        topPanel = new Panel();
        statusLabel = new Label();
        importButton = new Button();
        yearUpDown = new NumericUpDown();
        yearLabel = new Label();
        calibrationTypeButton = new Button();
        maintenanceTypeButton = new Button();
        summaryLabel = new Label();
        leftPanel = new Panel();
        breadcrumbLabel = new LinkLabel();
        areaGridView = new DataGridView();
        areaColumn = new DataGridViewTextBoxColumn();
        scheduledColumn = new DataGridViewTextBoxColumn();
        executedColumn = new DataGridViewTextBoxColumn();
        complianceColumn = new DataGridViewTextBoxColumn();
        onTimeColumn = new DataGridViewTextBoxColumn();
        lateColumn = new DataGridViewTextBoxColumn();
        overdueColumn = new DataGridViewTextBoxColumn();
        plotView = new FormsPlot();
        bottomPanel = new Panel();
        closeButton = new Button();
        topPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)yearUpDown).BeginInit();
        leftPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)areaGridView).BeginInit();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        //
        // topPanel
        //
        topPanel.Controls.Add(statusLabel);
        topPanel.Controls.Add(importButton);
        topPanel.Controls.Add(yearUpDown);
        topPanel.Controls.Add(yearLabel);
        topPanel.Controls.Add(calibrationTypeButton);
        topPanel.Controls.Add(maintenanceTypeButton);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new System.Drawing.Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Size = new System.Drawing.Size(900, 80);
        topPanel.TabIndex = 0;
        //
        // maintenanceTypeButton
        //
        maintenanceTypeButton.Location = new System.Drawing.Point(12, 12);
        maintenanceTypeButton.Name = "maintenanceTypeButton";
        maintenanceTypeButton.Size = new System.Drawing.Size(140, 30);
        maintenanceTypeButton.TabIndex = 0;
        maintenanceTypeButton.Text = "Mantenimiento";
        maintenanceTypeButton.UseVisualStyleBackColor = false;
        maintenanceTypeButton.Click += MaintenanceTypeButton_Click;
        //
        // calibrationTypeButton
        //
        calibrationTypeButton.Location = new System.Drawing.Point(160, 12);
        calibrationTypeButton.Name = "calibrationTypeButton";
        calibrationTypeButton.Size = new System.Drawing.Size(140, 30);
        calibrationTypeButton.TabIndex = 1;
        calibrationTypeButton.Text = "Calibración";
        calibrationTypeButton.UseVisualStyleBackColor = false;
        calibrationTypeButton.Click += CalibrationTypeButton_Click;
        //
        // yearLabel
        //
        yearLabel.AutoSize = true;
        yearLabel.Location = new System.Drawing.Point(320, 17);
        yearLabel.Name = "yearLabel";
        yearLabel.Size = new System.Drawing.Size(40, 20);
        yearLabel.TabIndex = 2;
        yearLabel.Text = "Año:";
        //
        // yearUpDown
        //
        yearUpDown.Location = new System.Drawing.Point(365, 14);
        yearUpDown.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        yearUpDown.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
        yearUpDown.Name = "yearUpDown";
        yearUpDown.Size = new System.Drawing.Size(70, 27);
        yearUpDown.TabIndex = 3;
        yearUpDown.Value = new decimal(new int[] { 2026, 0, 0, 0 });
        //
        // importButton
        //
        importButton.Location = new System.Drawing.Point(450, 12);
        importButton.Name = "importButton";
        importButton.Size = new System.Drawing.Size(140, 30);
        importButton.TabIndex = 4;
        importButton.Text = "Importar...";
        importButton.UseVisualStyleBackColor = true;
        importButton.Click += ImportButton_Click;
        //
        // statusLabel
        //
        statusLabel.AutoSize = true;
        statusLabel.ForeColor = System.Drawing.Color.DimGray;
        statusLabel.Location = new System.Drawing.Point(12, 52);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(120, 20);
        statusLabel.TabIndex = 5;
        statusLabel.Text = "Sin importar todavía.";
        //
        // summaryLabel
        //
        summaryLabel.AutoSize = true;
        summaryLabel.Dock = DockStyle.Top;
        summaryLabel.Location = new System.Drawing.Point(0, 80);
        summaryLabel.MinimumSize = new System.Drawing.Size(0, 70);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Padding = new Padding(12, 8, 12, 8);
        summaryLabel.TabIndex = 1;
        summaryLabel.Text = "Importa un cronograma para ver los indicadores.";
        //
        // leftPanel
        //
        leftPanel.Controls.Add(areaGridView);
        leftPanel.Controls.Add(breadcrumbLabel);
        leftPanel.Dock = DockStyle.Left;
        leftPanel.Location = new System.Drawing.Point(0, 150);
        leftPanel.Name = "leftPanel";
        leftPanel.Size = new System.Drawing.Size(380, 420);
        leftPanel.TabIndex = 2;
        //
        // breadcrumbLabel
        //
        breadcrumbLabel.Dock = DockStyle.Top;
        breadcrumbLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        breadcrumbLabel.Name = "breadcrumbLabel";
        breadcrumbLabel.Padding = new Padding(12, 8, 12, 4);
        breadcrumbLabel.Size = new System.Drawing.Size(380, 30);
        breadcrumbLabel.TabIndex = 0;
        breadcrumbLabel.TabStop = true;
        breadcrumbLabel.Text = "Todas las áreas";
        breadcrumbLabel.LinkClicked += BreadcrumbLabel_LinkClicked;
        //
        // areaGridView
        //
        areaGridView.AllowUserToAddRows = false;
        areaGridView.AllowUserToDeleteRows = false;
        areaGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        areaGridView.Columns.AddRange(new DataGridViewColumn[] { areaColumn, scheduledColumn, executedColumn, complianceColumn, onTimeColumn, lateColumn, overdueColumn });
        areaGridView.Dock = DockStyle.Fill;
        areaGridView.Location = new System.Drawing.Point(0, 30);
        areaGridView.MultiSelect = false;
        areaGridView.Name = "areaGridView";
        areaGridView.ReadOnly = true;
        areaGridView.RowHeadersVisible = false;
        areaGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        areaGridView.Size = new System.Drawing.Size(380, 390);
        areaGridView.TabIndex = 1;
        areaGridView.CellClick += AreaGridView_CellClick;
        //
        // areaColumn
        //
        areaColumn.HeaderText = "Área";
        areaColumn.Name = "areaColumn";
        areaColumn.ReadOnly = true;
        areaColumn.FillWeight = 34;
        //
        // scheduledColumn
        //
        scheduledColumn.HeaderText = "Prog.";
        scheduledColumn.Name = "scheduledColumn";
        scheduledColumn.ReadOnly = true;
        scheduledColumn.FillWeight = 11;
        //
        // executedColumn
        //
        executedColumn.HeaderText = "Ejec.";
        executedColumn.Name = "executedColumn";
        executedColumn.ReadOnly = true;
        executedColumn.FillWeight = 11;
        //
        // complianceColumn
        //
        complianceColumn.HeaderText = "% Cumpl.";
        complianceColumn.Name = "complianceColumn";
        complianceColumn.ReadOnly = true;
        complianceColumn.FillWeight = 16;
        //
        // onTimeColumn
        //
        onTimeColumn.HeaderText = "A tiempo";
        onTimeColumn.Name = "onTimeColumn";
        onTimeColumn.ReadOnly = true;
        onTimeColumn.FillWeight = 14;
        //
        // lateColumn
        //
        lateColumn.HeaderText = "Atrasados";
        lateColumn.Name = "lateColumn";
        lateColumn.ReadOnly = true;
        lateColumn.FillWeight = 14;
        //
        // overdueColumn
        //
        overdueColumn.HeaderText = "Vencidos";
        overdueColumn.Name = "overdueColumn";
        overdueColumn.ReadOnly = true;
        overdueColumn.FillWeight = 14;
        //
        // plotView
        //
        plotView.Dock = DockStyle.Fill;
        plotView.Location = new System.Drawing.Point(380, 150);
        plotView.Name = "plotView";
        plotView.Size = new System.Drawing.Size(520, 420);
        plotView.TabIndex = 3;
        //
        // bottomPanel
        //
        bottomPanel.Controls.Add(closeButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new System.Drawing.Point(0, 570);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Padding = new Padding(12);
        bottomPanel.Size = new System.Drawing.Size(900, 56);
        bottomPanel.TabIndex = 4;
        //
        // closeButton
        //
        closeButton.DialogResult = DialogResult.Cancel;
        closeButton.Location = new System.Drawing.Point(768, 12);
        closeButton.Name = "closeButton";
        closeButton.Size = new System.Drawing.Size(120, 32);
        closeButton.TabIndex = 0;
        closeButton.Text = "Cerrar";
        closeButton.UseVisualStyleBackColor = true;
        //
        // CronogramaForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = closeButton;
        ClientSize = new System.Drawing.Size(900, 626);
        Controls.Add(plotView);
        Controls.Add(leftPanel);
        Controls.Add(summaryLabel);
        Controls.Add(topPanel);
        Controls.Add(bottomPanel);
        MinimumSize = new System.Drawing.Size(800, 550);
        Name = "CronogramaForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cronogramas de mantenimiento y calibración";
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)yearUpDown).EndInit();
        leftPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)areaGridView).EndInit();
        bottomPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel topPanel;
    private Button maintenanceTypeButton;
    private Button calibrationTypeButton;
    private Label yearLabel;
    private NumericUpDown yearUpDown;
    private Button importButton;
    private Label statusLabel;
    private Label summaryLabel;
    private Panel leftPanel;
    private LinkLabel breadcrumbLabel;
    private DataGridView areaGridView;
    private DataGridViewTextBoxColumn areaColumn;
    private DataGridViewTextBoxColumn scheduledColumn;
    private DataGridViewTextBoxColumn executedColumn;
    private DataGridViewTextBoxColumn complianceColumn;
    private DataGridViewTextBoxColumn onTimeColumn;
    private DataGridViewTextBoxColumn lateColumn;
    private DataGridViewTextBoxColumn overdueColumn;
    private FormsPlot plotView;
    private Panel bottomPanel;
    private Button closeButton;
}
