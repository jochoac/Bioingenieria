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
        alertPanel = new Panel();
        alertSummaryLabel = new Label();
        alertToggleLabel = new LinkLabel();
        alertGridView = new DataGridView();
        alertTypeColumn = new DataGridViewTextBoxColumn();
        alertAreaColumn = new DataGridViewTextBoxColumn();
        alertEquipmentColumn = new DataGridViewTextBoxColumn();
        alertStatusColumn = new DataGridViewTextBoxColumn();
        tabControl = new TabControl();
        areaComplianceTabPage = new TabPage();
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
        semesterTabPage = new TabPage();
        semesterPlotView = new FormsPlot();
        semesterSummaryLabel = new Label();
        semesterTogglePanel = new Panel();
        secondHalfButton = new Button();
        firstHalfButton = new Button();
        complianceLevelTabPage = new TabPage();
        complianceLevelGridView = new DataGridView();
        complianceAreaColumn = new DataGridViewTextBoxColumn();
        complianceScheduledColumn = new DataGridViewTextBoxColumn();
        complianceExecutedColumn = new DataGridViewTextBoxColumn();
        compliancePercentColumn = new DataGridViewTextBoxColumn();
        complianceMaxDelayColumn = new DataGridViewTextBoxColumn();
        complianceSemaphoreColumn = new DataGridViewTextBoxColumn();
        complianceBreadcrumbLabel = new LinkLabel();
        bottomPanel = new Panel();
        closeButton = new Button();
        topPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)yearUpDown).BeginInit();
        alertPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)alertGridView).BeginInit();
        tabControl.SuspendLayout();
        areaComplianceTabPage.SuspendLayout();
        leftPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)areaGridView).BeginInit();
        semesterTabPage.SuspendLayout();
        semesterTogglePanel.SuspendLayout();
        complianceLevelTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)complianceLevelGridView).BeginInit();
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
        // alertPanel
        //
        alertPanel.Controls.Add(alertToggleLabel);
        alertPanel.Controls.Add(alertSummaryLabel);
        alertPanel.Dock = DockStyle.Top;
        alertPanel.Location = new System.Drawing.Point(0, 80);
        alertPanel.Name = "alertPanel";
        alertPanel.Size = new System.Drawing.Size(900, 36);
        alertPanel.TabIndex = 1;
        //
        // alertSummaryLabel
        //
        alertSummaryLabel.AutoSize = true;
        alertSummaryLabel.Location = new System.Drawing.Point(12, 9);
        alertSummaryLabel.Name = "alertSummaryLabel";
        alertSummaryLabel.Size = new System.Drawing.Size(180, 20);
        alertSummaryLabel.TabIndex = 0;
        alertSummaryLabel.Text = "Sin alertas de vencimiento.";
        //
        // alertToggleLabel
        //
        alertToggleLabel.AutoSize = true;
        alertToggleLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        alertToggleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        alertToggleLabel.Location = new System.Drawing.Point(780, 9);
        alertToggleLabel.Name = "alertToggleLabel";
        alertToggleLabel.Size = new System.Drawing.Size(100, 20);
        alertToggleLabel.TabIndex = 1;
        alertToggleLabel.TabStop = true;
        alertToggleLabel.Text = "Ver detalle ▾";
        alertToggleLabel.Visible = false;
        alertToggleLabel.LinkClicked += AlertToggleLabel_LinkClicked;
        //
        // alertGridView
        //
        alertGridView.AllowUserToAddRows = false;
        alertGridView.AllowUserToDeleteRows = false;
        alertGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        alertGridView.Columns.AddRange(new DataGridViewColumn[] { alertTypeColumn, alertAreaColumn, alertEquipmentColumn, alertStatusColumn });
        alertGridView.Dock = DockStyle.Top;
        alertGridView.Location = new System.Drawing.Point(0, 116);
        alertGridView.MultiSelect = false;
        alertGridView.Name = "alertGridView";
        alertGridView.ReadOnly = true;
        alertGridView.RowHeadersVisible = false;
        alertGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        alertGridView.Size = new System.Drawing.Size(900, 160);
        alertGridView.TabIndex = 2;
        alertGridView.Visible = false;
        //
        // alertTypeColumn
        //
        alertTypeColumn.HeaderText = "Tipo";
        alertTypeColumn.Name = "alertTypeColumn";
        alertTypeColumn.ReadOnly = true;
        alertTypeColumn.FillWeight = 20;
        //
        // alertAreaColumn
        //
        alertAreaColumn.HeaderText = "Área";
        alertAreaColumn.Name = "alertAreaColumn";
        alertAreaColumn.ReadOnly = true;
        alertAreaColumn.FillWeight = 20;
        //
        // alertEquipmentColumn
        //
        alertEquipmentColumn.HeaderText = "Equipo";
        alertEquipmentColumn.Name = "alertEquipmentColumn";
        alertEquipmentColumn.ReadOnly = true;
        alertEquipmentColumn.FillWeight = 35;
        //
        // alertStatusColumn
        //
        alertStatusColumn.HeaderText = "Estado";
        alertStatusColumn.Name = "alertStatusColumn";
        alertStatusColumn.ReadOnly = true;
        alertStatusColumn.FillWeight = 25;
        //
        // tabControl
        //
        tabControl.Controls.Add(areaComplianceTabPage);
        tabControl.Controls.Add(semesterTabPage);
        tabControl.Controls.Add(complianceLevelTabPage);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new System.Drawing.Point(0, 116);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new System.Drawing.Size(900, 508);
        tabControl.TabIndex = 3;
        //
        // areaComplianceTabPage
        //
        areaComplianceTabPage.Controls.Add(plotView);
        areaComplianceTabPage.Controls.Add(leftPanel);
        areaComplianceTabPage.Controls.Add(summaryLabel);
        areaComplianceTabPage.Location = new System.Drawing.Point(4, 29);
        areaComplianceTabPage.Name = "areaComplianceTabPage";
        areaComplianceTabPage.Padding = new Padding(0);
        areaComplianceTabPage.Size = new System.Drawing.Size(892, 475);
        areaComplianceTabPage.TabIndex = 0;
        areaComplianceTabPage.Text = "Cumplimiento por área";
        areaComplianceTabPage.UseVisualStyleBackColor = true;
        //
        // summaryLabel
        //
        summaryLabel.AutoSize = true;
        summaryLabel.Dock = DockStyle.Top;
        summaryLabel.Location = new System.Drawing.Point(0, 0);
        summaryLabel.MinimumSize = new System.Drawing.Size(0, 70);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Padding = new Padding(12, 8, 12, 8);
        summaryLabel.TabIndex = 0;
        summaryLabel.Text = "Importa un cronograma para ver los indicadores.";
        //
        // leftPanel
        //
        leftPanel.Controls.Add(areaGridView);
        leftPanel.Controls.Add(breadcrumbLabel);
        leftPanel.Dock = DockStyle.Left;
        leftPanel.Location = new System.Drawing.Point(0, 70);
        leftPanel.Name = "leftPanel";
        leftPanel.Size = new System.Drawing.Size(380, 405);
        leftPanel.TabIndex = 1;
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
        areaGridView.Size = new System.Drawing.Size(380, 375);
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
        plotView.Location = new System.Drawing.Point(380, 70);
        plotView.Name = "plotView";
        plotView.Size = new System.Drawing.Size(512, 405);
        plotView.TabIndex = 2;
        //
        // semesterTabPage
        //
        semesterTabPage.Controls.Add(semesterPlotView);
        semesterTabPage.Controls.Add(semesterSummaryLabel);
        semesterTabPage.Controls.Add(semesterTogglePanel);
        semesterTabPage.Location = new System.Drawing.Point(4, 29);
        semesterTabPage.Name = "semesterTabPage";
        semesterTabPage.Padding = new Padding(0);
        semesterTabPage.Size = new System.Drawing.Size(892, 475);
        semesterTabPage.TabIndex = 1;
        semesterTabPage.Text = "Resumen semestral";
        semesterTabPage.UseVisualStyleBackColor = true;
        //
        // semesterTogglePanel
        //
        semesterTogglePanel.Controls.Add(secondHalfButton);
        semesterTogglePanel.Controls.Add(firstHalfButton);
        semesterTogglePanel.Dock = DockStyle.Top;
        semesterTogglePanel.Location = new System.Drawing.Point(0, 0);
        semesterTogglePanel.Name = "semesterTogglePanel";
        semesterTogglePanel.Size = new System.Drawing.Size(892, 54);
        semesterTogglePanel.TabIndex = 0;
        //
        // firstHalfButton
        //
        firstHalfButton.Location = new System.Drawing.Point(12, 12);
        firstHalfButton.Name = "firstHalfButton";
        firstHalfButton.Size = new System.Drawing.Size(140, 30);
        firstHalfButton.TabIndex = 0;
        firstHalfButton.Text = "Ene - Jun";
        firstHalfButton.UseVisualStyleBackColor = false;
        firstHalfButton.Click += FirstHalfButton_Click;
        //
        // secondHalfButton
        //
        secondHalfButton.Location = new System.Drawing.Point(160, 12);
        secondHalfButton.Name = "secondHalfButton";
        secondHalfButton.Size = new System.Drawing.Size(140, 30);
        secondHalfButton.TabIndex = 1;
        secondHalfButton.Text = "Jul - Dic";
        secondHalfButton.UseVisualStyleBackColor = false;
        secondHalfButton.Click += SecondHalfButton_Click;
        //
        // semesterSummaryLabel
        //
        semesterSummaryLabel.AutoSize = true;
        semesterSummaryLabel.Dock = DockStyle.Top;
        semesterSummaryLabel.Location = new System.Drawing.Point(0, 54);
        semesterSummaryLabel.MinimumSize = new System.Drawing.Size(0, 40);
        semesterSummaryLabel.Name = "semesterSummaryLabel";
        semesterSummaryLabel.Padding = new Padding(12, 8, 12, 8);
        semesterSummaryLabel.TabIndex = 1;
        semesterSummaryLabel.Text = "Importa un cronograma para ver los indicadores.";
        //
        // semesterPlotView
        //
        semesterPlotView.Dock = DockStyle.Fill;
        semesterPlotView.Location = new System.Drawing.Point(0, 94);
        semesterPlotView.Name = "semesterPlotView";
        semesterPlotView.Size = new System.Drawing.Size(892, 381);
        semesterPlotView.TabIndex = 2;
        //
        // complianceLevelTabPage
        //
        complianceLevelTabPage.Controls.Add(complianceLevelGridView);
        complianceLevelTabPage.Controls.Add(complianceBreadcrumbLabel);
        complianceLevelTabPage.Location = new System.Drawing.Point(4, 29);
        complianceLevelTabPage.Name = "complianceLevelTabPage";
        complianceLevelTabPage.Padding = new Padding(0);
        complianceLevelTabPage.Size = new System.Drawing.Size(892, 475);
        complianceLevelTabPage.TabIndex = 2;
        complianceLevelTabPage.Text = "Nivel de cumplimiento";
        complianceLevelTabPage.UseVisualStyleBackColor = true;
        //
        // complianceBreadcrumbLabel
        //
        complianceBreadcrumbLabel.Dock = DockStyle.Top;
        complianceBreadcrumbLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        complianceBreadcrumbLabel.Name = "complianceBreadcrumbLabel";
        complianceBreadcrumbLabel.Padding = new Padding(12, 8, 12, 4);
        complianceBreadcrumbLabel.Size = new System.Drawing.Size(892, 30);
        complianceBreadcrumbLabel.TabIndex = 0;
        complianceBreadcrumbLabel.TabStop = true;
        complianceBreadcrumbLabel.Text = "Todas las áreas";
        complianceBreadcrumbLabel.LinkClicked += ComplianceBreadcrumbLabel_LinkClicked;
        //
        // complianceLevelGridView
        //
        complianceLevelGridView.AllowUserToAddRows = false;
        complianceLevelGridView.AllowUserToDeleteRows = false;
        complianceLevelGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        complianceLevelGridView.Columns.AddRange(new DataGridViewColumn[] { complianceAreaColumn, complianceScheduledColumn, complianceExecutedColumn, compliancePercentColumn, complianceMaxDelayColumn, complianceSemaphoreColumn });
        complianceLevelGridView.Dock = DockStyle.Fill;
        complianceLevelGridView.Location = new System.Drawing.Point(0, 30);
        complianceLevelGridView.MultiSelect = false;
        complianceLevelGridView.Name = "complianceLevelGridView";
        complianceLevelGridView.ReadOnly = true;
        complianceLevelGridView.RowHeadersVisible = false;
        complianceLevelGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        complianceLevelGridView.Size = new System.Drawing.Size(892, 445);
        complianceLevelGridView.TabIndex = 1;
        complianceLevelGridView.CellClick += ComplianceLevelGridView_CellClick;
        //
        // complianceAreaColumn
        //
        complianceAreaColumn.HeaderText = "Área";
        complianceAreaColumn.Name = "complianceAreaColumn";
        complianceAreaColumn.ReadOnly = true;
        complianceAreaColumn.FillWeight = 28;
        //
        // complianceScheduledColumn
        //
        complianceScheduledColumn.HeaderText = "Prog.";
        complianceScheduledColumn.Name = "complianceScheduledColumn";
        complianceScheduledColumn.ReadOnly = true;
        complianceScheduledColumn.FillWeight = 12;
        //
        // complianceExecutedColumn
        //
        complianceExecutedColumn.HeaderText = "Ejec.";
        complianceExecutedColumn.Name = "complianceExecutedColumn";
        complianceExecutedColumn.ReadOnly = true;
        complianceExecutedColumn.FillWeight = 12;
        //
        // compliancePercentColumn
        //
        compliancePercentColumn.HeaderText = "% Cumpl.";
        compliancePercentColumn.Name = "compliancePercentColumn";
        compliancePercentColumn.ReadOnly = true;
        compliancePercentColumn.FillWeight = 16;
        //
        // complianceMaxDelayColumn
        //
        complianceMaxDelayColumn.HeaderText = "Retraso máx. (sem.)";
        complianceMaxDelayColumn.Name = "complianceMaxDelayColumn";
        complianceMaxDelayColumn.ReadOnly = true;
        complianceMaxDelayColumn.FillWeight = 16;
        //
        // complianceSemaphoreColumn
        //
        complianceSemaphoreColumn.HeaderText = "Semáforo";
        complianceSemaphoreColumn.Name = "complianceSemaphoreColumn";
        complianceSemaphoreColumn.ReadOnly = true;
        complianceSemaphoreColumn.FillWeight = 16;
        //
        // bottomPanel
        //
        bottomPanel.Controls.Add(closeButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new System.Drawing.Point(0, 624);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Padding = new Padding(12);
        bottomPanel.Size = new System.Drawing.Size(900, 56);
        bottomPanel.TabIndex = 4;
        //
        // closeButton
        //
        closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
        ClientSize = new System.Drawing.Size(1300, 820);
        Controls.Add(tabControl);
        Controls.Add(alertGridView);
        Controls.Add(alertPanel);
        Controls.Add(topPanel);
        Controls.Add(bottomPanel);
        MinimumSize = new System.Drawing.Size(1000, 650);
        Name = "CronogramaForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cronogramas de mantenimiento y calibración";
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)yearUpDown).EndInit();
        alertPanel.ResumeLayout(false);
        alertPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)alertGridView).EndInit();
        tabControl.ResumeLayout(false);
        areaComplianceTabPage.ResumeLayout(false);
        areaComplianceTabPage.PerformLayout();
        leftPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)areaGridView).EndInit();
        semesterTabPage.ResumeLayout(false);
        semesterTabPage.PerformLayout();
        semesterTogglePanel.ResumeLayout(false);
        complianceLevelTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)complianceLevelGridView).EndInit();
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
    private Panel alertPanel;
    private Label alertSummaryLabel;
    private LinkLabel alertToggleLabel;
    private DataGridView alertGridView;
    private DataGridViewTextBoxColumn alertTypeColumn;
    private DataGridViewTextBoxColumn alertAreaColumn;
    private DataGridViewTextBoxColumn alertEquipmentColumn;
    private DataGridViewTextBoxColumn alertStatusColumn;
    private TabControl tabControl;
    private TabPage areaComplianceTabPage;
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
    private TabPage semesterTabPage;
    private Panel semesterTogglePanel;
    private Button firstHalfButton;
    private Button secondHalfButton;
    private Label semesterSummaryLabel;
    private FormsPlot semesterPlotView;
    private TabPage complianceLevelTabPage;
    private LinkLabel complianceBreadcrumbLabel;
    private DataGridView complianceLevelGridView;
    private DataGridViewTextBoxColumn complianceAreaColumn;
    private DataGridViewTextBoxColumn complianceScheduledColumn;
    private DataGridViewTextBoxColumn complianceExecutedColumn;
    private DataGridViewTextBoxColumn compliancePercentColumn;
    private DataGridViewTextBoxColumn complianceMaxDelayColumn;
    private DataGridViewTextBoxColumn complianceSemaphoreColumn;
    private Panel bottomPanel;
    private Button closeButton;
}