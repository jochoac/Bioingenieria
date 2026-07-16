using Bioingenieria.Models;
using Bioingenieria.Services;
using ScottPlot;

namespace Bioingenieria.Forms;

public partial class CronogramaForm : Form
{
    private static readonly System.Drawing.Color GoodColor = System.Drawing.ColorTranslator.FromHtml("#0CA30C");
    private static readonly System.Drawing.Color WarningColor = System.Drawing.ColorTranslator.FromHtml("#FAB219");
    private static readonly System.Drawing.Color CriticalColor = System.Drawing.ColorTranslator.FromHtml("#D03B3B");

    private readonly ScheduleService _scheduleService;
    private ScheduleType _currentType = ScheduleType.Maintenance;
    private ComplianceIndicators _indicators = new();
    private AreaCompliance? _selectedArea;
    private AreaCompliance? _selectedComplianceArea;
    private Semester _semester = SemesterExtensions.Current;

    public CronogramaForm(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;

        InitializeComponent();
        plotView.Plot.Benchmark.IsVisible = false;
        semesterPlotView.Plot.Benchmark.IsVisible = false;

        yearUpDown.Value = DateTime.Today.Year;
        SelectSemester(_semester);
        SelectType(ScheduleType.Maintenance);
        RefreshAlerts();
    }

    private void MaintenanceTypeButton_Click(object? sender, EventArgs e)
    {
        SelectType(ScheduleType.Maintenance);
    }

    private void CalibrationTypeButton_Click(object? sender, EventArgs e)
    {
        SelectType(ScheduleType.Calibration);
    }

    private void FirstHalfButton_Click(object? sender, EventArgs e)
    {
        SelectSemester(Semester.FirstHalf);
        DisplaySemesterTab();
    }

    private void SecondHalfButton_Click(object? sender, EventArgs e)
    {
        SelectSemester(Semester.SecondHalf);
        DisplaySemesterTab();
    }

    private void ImportButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Seleccionar cronograma",
            Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _scheduleService.ImportFromExcel(dialog.FileName, _currentType, (int)yearUpDown.Value);
            RefreshView();
            RefreshAlerts();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"No se pudo importar el archivo: {ex.Message}",
                "Error al importar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SelectType(ScheduleType type)
    {
        _currentType = type;

        maintenanceTypeButton.BackColor = type == ScheduleType.Maintenance ? SystemColors.Highlight : SystemColors.Control;
        maintenanceTypeButton.ForeColor = type == ScheduleType.Maintenance ? SystemColors.HighlightText : SystemColors.ControlText;
        calibrationTypeButton.BackColor = type == ScheduleType.Calibration ? SystemColors.Highlight : SystemColors.Control;
        calibrationTypeButton.ForeColor = type == ScheduleType.Calibration ? SystemColors.HighlightText : SystemColors.ControlText;

        RefreshView();
    }

    private void SelectSemester(Semester semester)
    {
        _semester = semester;

        firstHalfButton.BackColor = semester == Semester.FirstHalf ? SystemColors.Highlight : SystemColors.Control;
        firstHalfButton.ForeColor = semester == Semester.FirstHalf ? SystemColors.HighlightText : SystemColors.ControlText;
        secondHalfButton.BackColor = semester == Semester.SecondHalf ? SystemColors.Highlight : SystemColors.Control;
        secondHalfButton.ForeColor = semester == Semester.SecondHalf ? SystemColors.HighlightText : SystemColors.ControlText;
    }

    private void AreaGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || _selectedArea is not null)
        {
            return;
        }

        if (areaGridView.Rows[e.RowIndex].Tag is not AreaCompliance area)
        {
            return;
        }

        _selectedArea = area;
        UpdateDrilldownView();
    }

    private void BreadcrumbLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        if (_selectedArea is null)
        {
            return;
        }

        _selectedArea = null;
        UpdateDrilldownView();
    }

    private void ComplianceLevelGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || _selectedComplianceArea is not null)
        {
            return;
        }

        if (complianceLevelGridView.Rows[e.RowIndex].Tag is not AreaCompliance area)
        {
            return;
        }

        _selectedComplianceArea = area;
        UpdateComplianceLevelView();
    }

    private void ComplianceBreadcrumbLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        if (_selectedComplianceArea is null)
        {
            return;
        }

        _selectedComplianceArea = null;
        UpdateComplianceLevelView();
    }

    private void AlertToggleLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        alertGridView.Visible = !alertGridView.Visible;
        alertToggleLabel.Text = alertGridView.Visible ? "Ocultar detalle ▴" : "Ver detalle ▾";
    }

    private void RefreshView()
    {
        _selectedArea = null;
        _selectedComplianceArea = null;

        var import = _scheduleService.GetImport(_currentType);

        if (import is null)
        {
            statusLabel.Text = "Sin importar todavía.";
            summaryLabel.Text = "Importa un cronograma para ver los indicadores.";
            _indicators = new ComplianceIndicators();
            UpdateDrilldownView();
            UpdateComplianceLevelView();
            DisplaySemesterTab();
            return;
        }

        statusLabel.Text = $"Importado {import.ImportedAtUtc.ToLocalTime():dd/MM/yyyy HH:mm} — {import.SourceFileName} (año {import.Year})";

        _indicators = _scheduleService.ComputeIndicators(_currentType);
        DisplaySummary(_indicators);
        UpdateDrilldownView();
        UpdateComplianceLevelView();
        DisplaySemesterTab();
    }

    private void DisplaySummary(ComplianceIndicators indicators)
    {
        summaryLabel.Text =
            $"Programados: {indicators.Scheduled}    Ejecutados: {indicators.Executed} ({indicators.CompliancePercentage:0.0}%)\n" +
            $"A tiempo: {indicators.OnTime}    Adelantados: {indicators.Early}    Atrasados: {indicators.Late} (prom. {indicators.AverageLateWeeks:0.0} sem.)\n" +
            $"Vencidos: {indicators.Overdue}    Ejecutados sin programación: {indicators.UnscheduledExecutions}";
    }

    private void UpdateDrilldownView()
    {
        if (_selectedArea is null)
        {
            breadcrumbLabel.Text = "Todas las áreas";
            breadcrumbLabel.Enabled = false;
            DisplayAreaGrid(_indicators.ByArea);
            DisplayChart(
                "Cumplimiento por área (%)",
                _indicators.ByArea.Select(a => a.Area).ToList(),
                _indicators.ByArea.Select(a => a.CompliancePercentage).ToList());
        }
        else
        {
            breadcrumbLabel.Text = $"◀ {_selectedArea.Area}";
            breadcrumbLabel.Enabled = true;
            DisplayEquipmentGrid(_selectedArea.ByEquipment);
            DisplayChart(
                $"{_selectedArea.Area} — cumplimiento por equipo (%)",
                _selectedArea.ByEquipment.Select(eq => eq.EquipmentName).ToList(),
                _selectedArea.ByEquipment.Select(eq => eq.CompliancePercentage).ToList());
        }
    }

    private void DisplayAreaGrid(IReadOnlyList<AreaCompliance> areas)
    {
        areaColumn.HeaderText = "Área";
        areaGridView.Rows.Clear();

        foreach (var area in areas)
        {
            var rowIndex = areaGridView.Rows.Add(
                area.Area,
                area.Scheduled,
                area.Executed,
                $"{area.CompliancePercentage:0.0}%",
                area.OnTime,
                area.Late,
                area.Overdue);
            areaGridView.Rows[rowIndex].Tag = area;
        }
    }

    private void DisplayEquipmentGrid(IReadOnlyList<EquipmentCompliance> equipment)
    {
        areaColumn.HeaderText = "Equipo";
        areaGridView.Rows.Clear();

        foreach (var item in equipment)
        {
            var label = string.IsNullOrWhiteSpace(item.InventoryTag)
                ? item.EquipmentName
                : $"{item.EquipmentName} ({item.InventoryTag})";

            areaGridView.Rows.Add(
                label,
                item.Scheduled,
                item.Executed,
                $"{item.CompliancePercentage:0.0}%",
                item.OnTime,
                item.Late,
                item.Overdue);
        }
    }

    private void DisplayChart(string title, IReadOnlyList<string> labels, IReadOnlyList<double> values)
    {
        plotView.Plot.Clear();

        if (values.Count == 0)
        {
            plotView.Refresh();
            return;
        }

        var bars = values
            .Select((value, index) => new Bar
            {
                Position = index,
                Value = value
            })
            .ToList();

        plotView.Plot.Add.Bars(bars);
        plotView.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
            bars.Select(b => b.Position).ToArray(),
            labels.ToArray());
        plotView.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
        plotView.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
        plotView.Plot.Axes.Bottom.TickLabelStyle.FontSize = 14;

        using var paint = ScottPlot.Paint.NewDisposablePaint();
        var largestLabelWidth = labels
            .Select(label => plotView.Plot.Axes.Bottom.TickLabelStyle.Measure(label, paint).Size.Width)
            .DefaultIfEmpty(0f)
            .Max();
        plotView.Plot.Axes.Bottom.MinimumSize = largestLabelWidth;
        plotView.Plot.Axes.Right.MinimumSize = largestLabelWidth;

        plotView.Plot.Title(title);
        plotView.Plot.Axes.Margins(bottom: 0);
        plotView.Refresh();
    }

    private void DisplaySemesterTab()
    {
        semesterTogglePanel.Visible = _currentType == ScheduleType.Maintenance;

        var summary = _scheduleService.ComputeSemesterSummary(_currentType, _semester);
        var year = (int)yearUpDown.Value;
        var title = _currentType == ScheduleType.Maintenance
            ? (_semester == Semester.FirstHalf ? $"Ene - Jun {year}" : $"Jul - Dic {year}")
            : $"Año completo {year}";

        semesterSummaryLabel.Text = $"{title} — Programados: {summary.Scheduled}    Ejecutados: {summary.Executed}";

        DisplayPie(title, summary);
    }

    private void DisplayPie(string title, SemesterSummary summary)
    {
        semesterPlotView.Plot.Clear();
        semesterPlotView.Plot.Title(title);

        if (summary.Scheduled == 0 && summary.Executed == 0)
        {
            semesterPlotView.Refresh();
            return;
        }

        var slices = new List<PieSlice>
        {
            new() { Value = summary.Scheduled, FillColor = ScottPlot.Color.FromHex("#2A78D6"), Label = "Programados", LegendText = "Programados" },
            new() { Value = summary.Executed, FillColor = ScottPlot.Color.FromHex("#1BAF7A"), Label = "Ejecutados", LegendText = "Ejecutados" }
        };

        semesterPlotView.Plot.Add.Pie(slices);
        semesterPlotView.Plot.ShowLegend();
        semesterPlotView.Refresh();
    }

    private void UpdateComplianceLevelView()
    {
        if (_selectedComplianceArea is null)
        {
            complianceBreadcrumbLabel.Text = "Todas las áreas";
            complianceBreadcrumbLabel.Enabled = false;
            DisplayComplianceAreaGrid(_indicators.ByArea);
        }
        else
        {
            complianceBreadcrumbLabel.Text = $"◀ {_selectedComplianceArea.Area}";
            complianceBreadcrumbLabel.Enabled = true;
            DisplayComplianceEquipmentGrid(_selectedComplianceArea.ByEquipment);
        }
    }

    private void DisplayComplianceAreaGrid(IReadOnlyList<AreaCompliance> areas)
    {
        complianceAreaColumn.HeaderText = "Área";
        complianceLevelGridView.Rows.Clear();

        foreach (var area in areas)
        {
            var rowIndex = complianceLevelGridView.Rows.Add(
                area.Area,
                area.Scheduled,
                area.Executed,
                $"{area.CompliancePercentage:0.0}%",
                area.MaxDelayWeeks,
                SemaphoreLabel(area.Semaphore));
            complianceLevelGridView.Rows[rowIndex].Tag = area;
            ApplySemaphoreColor(complianceLevelGridView.Rows[rowIndex], area.Semaphore);
        }
    }

    private void DisplayComplianceEquipmentGrid(IReadOnlyList<EquipmentCompliance> equipment)
    {
        complianceAreaColumn.HeaderText = "Equipo";
        complianceLevelGridView.Rows.Clear();

        foreach (var item in equipment)
        {
            var label = string.IsNullOrWhiteSpace(item.InventoryTag)
                ? item.EquipmentName
                : $"{item.EquipmentName} ({item.InventoryTag})";

            var rowIndex = complianceLevelGridView.Rows.Add(
                label,
                item.Scheduled,
                item.Executed,
                $"{item.CompliancePercentage:0.0}%",
                item.MaxDelayWeeks,
                SemaphoreLabel(item.Semaphore));
            ApplySemaphoreColor(complianceLevelGridView.Rows[rowIndex], item.Semaphore);
        }
    }

    private void ApplySemaphoreColor(DataGridViewRow row, ComplianceSemaphore semaphore)
    {
        var cell = row.Cells[complianceSemaphoreColumn.Index];
        cell.Style.BackColor = SemaphoreColor(semaphore);
        cell.Style.ForeColor = semaphore == ComplianceSemaphore.Yellow ? System.Drawing.Color.Black : System.Drawing.Color.White;
    }

    private static System.Drawing.Color SemaphoreColor(ComplianceSemaphore semaphore) => semaphore switch
    {
        ComplianceSemaphore.Red => CriticalColor,
        ComplianceSemaphore.Yellow => WarningColor,
        _ => GoodColor
    };

    private static string SemaphoreLabel(ComplianceSemaphore semaphore) => semaphore switch
    {
        ComplianceSemaphore.Red => "Rojo",
        ComplianceSemaphore.Yellow => "Amarillo",
        _ => "Verde"
    };

    private void RefreshAlerts()
    {
        var alerts = _scheduleService.GetUpcomingAlerts();
        var overdueCount = alerts.Count(a => a.DueDate < DateTime.Today);
        var upcomingCount = alerts.Count - overdueCount;

        alertSummaryLabel.Text = alerts.Count == 0
            ? "Sin alertas de vencimiento."
            : $"🔴 {overdueCount} vencidos   🟡 {upcomingCount} próximos (<30 días)";

        alertToggleLabel.Visible = alerts.Count > 0;

        DisplayAlertGrid(alerts);

        if (alerts.Count == 0)
        {
            alertGridView.Visible = false;
        }

        alertToggleLabel.Text = alertGridView.Visible ? "Ocultar detalle ▴" : "Ver detalle ▾";
    }

    private void DisplayAlertGrid(IReadOnlyList<UpcomingAlert> alerts)
    {
        alertGridView.Rows.Clear();

        foreach (var alert in alerts)
        {
            var daysUntilDue = (alert.DueDate - DateTime.Today).Days;
            var status = daysUntilDue < 0
                ? $"Venció hace {-daysUntilDue} día{(daysUntilDue == -1 ? "" : "s")}"
                : $"Vence en {daysUntilDue} día{(daysUntilDue == 1 ? "" : "s")}";

            var label = string.IsNullOrWhiteSpace(alert.InventoryTag)
                ? alert.EquipmentName
                : $"{alert.EquipmentName} ({alert.InventoryTag})";

            var rowIndex = alertGridView.Rows.Add(
                alert.Type == ScheduleType.Maintenance ? "Mantenimiento" : "Calibración",
                alert.Area,
                label,
                status);

            var statusCell = alertGridView.Rows[rowIndex].Cells[alertStatusColumn.Index];
            statusCell.Style.BackColor = daysUntilDue < 0 ? CriticalColor : WarningColor;
            statusCell.Style.ForeColor = daysUntilDue < 0 ? System.Drawing.Color.White : System.Drawing.Color.Black;
        }
    }
}