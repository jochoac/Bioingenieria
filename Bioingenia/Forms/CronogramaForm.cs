using Bioingenieria.Models;
using Bioingenieria.Services;
using ScottPlot;

namespace Bioingenieria.Forms;

public partial class CronogramaForm : Form
{
    private readonly ScheduleService _scheduleService;
    private ScheduleType _currentType = ScheduleType.Maintenance;
    private ComplianceIndicators _indicators = new();
    private AreaCompliance? _selectedArea;

    public CronogramaForm(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;

        InitializeComponent();
        plotView.Plot.Benchmark.IsVisible = false;

        yearUpDown.Value = DateTime.Today.Year;
        SelectType(ScheduleType.Maintenance);
    }

    private void MaintenanceTypeButton_Click(object? sender, EventArgs e)
    {
        SelectType(ScheduleType.Maintenance);
    }

    private void CalibrationTypeButton_Click(object? sender, EventArgs e)
    {
        SelectType(ScheduleType.Calibration);
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

    private void RefreshView()
    {
        _selectedArea = null;

        var import = _scheduleService.GetImport(_currentType);

        if (import is null)
        {
            statusLabel.Text = "Sin importar todavía.";
            summaryLabel.Text = "Importa un cronograma para ver los indicadores.";
            _indicators = new ComplianceIndicators();
            UpdateDrilldownView();
            return;
        }

        statusLabel.Text = $"Importado {import.ImportedAtUtc.ToLocalTime():dd/MM/yyyy HH:mm} — {import.SourceFileName} (año {import.Year})";

        _indicators = _scheduleService.ComputeIndicators(_currentType);
        DisplaySummary(_indicators);
        UpdateDrilldownView();
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
                Value = value,
                Label = labels[index]
            })
            .ToList();

        plotView.Plot.Add.Bars(bars);
        plotView.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
            bars.Select(b => b.Position).ToArray(),
            bars.Select(b => b.Label).ToArray());
        plotView.Plot.Title(title);
        plotView.Plot.Axes.Margins(bottom: 0);
        plotView.Refresh();
    }
}
