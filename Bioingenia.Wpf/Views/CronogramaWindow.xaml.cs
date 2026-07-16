using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Bioingenieria.Models;
using Bioingenieria.Services;
using ScottPlot;

namespace Bioingenieria.Views;

public partial class CronogramaWindow : Window
{
    private static readonly Brush SuccessBrush = (Brush)Application.Current.Resources["SuccessBrush"];
    private static readonly Brush WarningBrush = (Brush)Application.Current.Resources["WarningBrush"];
    private static readonly Brush CriticalBrush = (Brush)Application.Current.Resources["CriticalBrush"];

    private static readonly ScottPlot.Color PrimaryPlotColor = ScottPlot.Color.FromHex("#0078A8");
    private static readonly ScottPlot.Color SuccessPlotColor = ScottPlot.Color.FromHex("#1BAF7A");
    private static readonly ScottPlot.Color NeutralPlotColor = ScottPlot.Color.FromHex("#C7D0D8");

    private readonly ScheduleService _scheduleService;
    private ScheduleType _currentType = ScheduleType.Maintenance;
    private ComplianceIndicators _indicators = new();
    private AreaCompliance? _selectedArea;
    private AreaCompliance? _selectedComplianceArea;
    private Semester _semester = SemesterExtensions.Current;
    private int _year = DateTime.Today.Year;

    public CronogramaWindow(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;

        InitializeComponent();
        PlotView.Plot.Benchmark.IsVisible = false;
        SemesterPlotView.Plot.Benchmark.IsVisible = false;

        YearTextBox.Text = _year.ToString();
        SelectSemester(_semester);
        SelectType(ScheduleType.Maintenance);
        RefreshAlerts();
    }

    private void MaintenanceTypeButton_Click(object sender, RoutedEventArgs e) => SelectType(ScheduleType.Maintenance);

    private void CalibrationTypeButton_Click(object sender, RoutedEventArgs e) => SelectType(ScheduleType.Calibration);

    private void FirstHalfButton_Click(object sender, RoutedEventArgs e)
    {
        SelectSemester(Semester.FirstHalf);
        DisplaySemesterTab();
    }

    private void SecondHalfButton_Click(object sender, RoutedEventArgs e)
    {
        SelectSemester(Semester.SecondHalf);
        DisplaySemesterTab();
    }

    private void YearTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !e.Text.All(char.IsDigit);
    }

    private void YearUpButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(YearTextBox.Text, out var year))
        {
            year = _year;
        }

        _year = Math.Min(2100, year + 1);
        YearTextBox.Text = _year.ToString();
    }

    private void YearDownButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(YearTextBox.Text, out var year))
        {
            year = _year;
        }

        _year = Math.Max(2000, year - 1);
        YearTextBox.Text = _year.ToString();
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Title = "Seleccionar cronograma",
            Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
        };

        if (dialog.ShowDialog(this) != true)
        {
            return;
        }

        if (!int.TryParse(YearTextBox.Text, out var year))
        {
            year = _year;
        }

        try
        {
            _scheduleService.ImportFromExcel(dialog.FileName, _currentType, year);
            RefreshView();
            RefreshAlerts();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"No se pudo importar el archivo: {ex.Message}",
                "Error al importar",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void SelectType(ScheduleType type)
    {
        _currentType = type;

        MaintenanceTypeButton.IsChecked = type == ScheduleType.Maintenance;
        CalibrationTypeButton.IsChecked = type == ScheduleType.Calibration;

        RefreshView();
    }

    private void SelectSemester(Semester semester)
    {
        _semester = semester;

        FirstHalfButton.IsChecked = semester == Semester.FirstHalf;
        SecondHalfButton.IsChecked = semester == Semester.SecondHalf;
    }

    private void AreaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_selectedArea is not null || AreaDataGrid.SelectedItem is not AreaRow row)
        {
            return;
        }

        _selectedArea = row.Model;
        UpdateDrilldownView();
    }

    private void BreadcrumbButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedArea is null)
        {
            return;
        }

        _selectedArea = null;
        UpdateDrilldownView();
    }

    private void ComplianceLevelDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_selectedComplianceArea is not null || ComplianceLevelDataGrid.SelectedItem is not ComplianceRow row)
        {
            return;
        }

        _selectedComplianceArea = row.Model;
        UpdateComplianceLevelView();
    }

    private void ComplianceBreadcrumbButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedComplianceArea is null)
        {
            return;
        }

        _selectedComplianceArea = null;
        UpdateComplianceLevelView();
    }

    private void AlertToggleButton_Click(object sender, RoutedEventArgs e)
    {
        AlertDataGrid.Visibility = AlertDataGrid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        AlertToggleButton.Content = AlertDataGrid.Visibility == Visibility.Visible ? "Ocultar detalle ▴" : "Ver detalle ▾";
    }

    private void RefreshView()
    {
        _selectedArea = null;
        _selectedComplianceArea = null;

        var import = _scheduleService.GetImport(_currentType);

        if (import is null)
        {
            StatusTextBlock.Text = "Sin importar todavía.";
            SummaryTextBlock.Text = "Importa un cronograma para ver los indicadores.";
            _indicators = new ComplianceIndicators();
            UpdateDrilldownView();
            UpdateComplianceLevelView();
            DisplaySemesterTab();
            return;
        }

        SetStatusText(import);

        _indicators = _scheduleService.ComputeIndicators(_currentType);
        DisplaySummary(_indicators);
        UpdateDrilldownView();
        UpdateComplianceLevelView();
        DisplaySemesterTab();
    }

    private void SetStatusText(ScheduleImport import)
    {
        StatusTextBlock.Inlines.Clear();
        StatusTextBlock.Inlines.Add($"Importado {import.ImportedAtUtc.ToLocalTime():dd/MM/yyyy HH:mm} — ");

        if (!string.IsNullOrEmpty(import.SourceFilePath) && File.Exists(import.SourceFilePath))
        {
            var link = new Hyperlink(new Run(import.SourceFileName)) { NavigateUri = new Uri(import.SourceFilePath) };
            link.RequestNavigate += (_, e) =>
            {
                try
                {
                    FileOpenerService.Open(import.SourceFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                e.Handled = true;
            };
            StatusTextBlock.Inlines.Add(link);
        }
        else
        {
            StatusTextBlock.Inlines.Add(import.SourceFileName);
        }

        StatusTextBlock.Inlines.Add($" (año {import.Year})");
    }

    private void DisplaySummary(ComplianceIndicators indicators)
    {
        SummaryTextBlock.Text =
            $"Programados: {indicators.Scheduled}    Ejecutados: {indicators.Executed} ({indicators.CompliancePercentage:0.0}%)\n" +
            $"A tiempo: {indicators.OnTime}    Adelantados: {indicators.Early}    Atrasados: {indicators.Late} (prom. {indicators.AverageLateWeeks:0.0} sem.)\n" +
            $"Vencidos: {indicators.Overdue}    Ejecutados sin programación: {indicators.UnscheduledExecutions}";
    }

    private void UpdateDrilldownView()
    {
        if (_selectedArea is null)
        {
            BreadcrumbButton.Content = "Todas las áreas";
            BreadcrumbButton.IsEnabled = false;
            AreaColumnHeader.Header = "Área";
            AreaDataGrid.ItemsSource = _indicators.ByArea
                .Select(a => new AreaRow(a.Area, a.Scheduled, a.Executed, a.CompliancePercentage, a.OnTime, a.Late, a.Overdue, a))
                .ToList();
            DisplayChart(
                "Cumplimiento por área (%)",
                _indicators.ByArea.Select(a => a.Area).ToList(),
                _indicators.ByArea.Select(a => a.CompliancePercentage).ToList());
        }
        else
        {
            BreadcrumbButton.Content = $"◀ {_selectedArea.Area}";
            BreadcrumbButton.IsEnabled = true;
            AreaColumnHeader.Header = "Equipo";
            AreaDataGrid.ItemsSource = _selectedArea.ByEquipment
                .Select(eq => new AreaRow(EquipmentLabel(eq.EquipmentName, eq.InventoryTag), eq.Scheduled, eq.Executed, eq.CompliancePercentage, eq.OnTime, eq.Late, eq.Overdue, null))
                .ToList();
            DisplayChart(
                $"{_selectedArea.Area} — cumplimiento por equipo (%)",
                _selectedArea.ByEquipment.Select(eq => eq.EquipmentName).ToList(),
                _selectedArea.ByEquipment.Select(eq => eq.CompliancePercentage).ToList());
        }
    }

    private void DisplayChart(string title, IReadOnlyList<string> labels, IReadOnlyList<double> values)
    {
        PlotView.Plot.Clear();

        if (values.Count == 0)
        {
            PlotView.Refresh();
            return;
        }

        var bars = values
            .Select((value, index) => new Bar
            {
                Position = index,
                Value = value,
                FillColor = PrimaryPlotColor
            })
            .ToList();

        PlotView.Plot.Add.Bars(bars);
        PlotView.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
            bars.Select(b => b.Position).ToArray(),
            labels.ToArray());
        PlotView.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
        PlotView.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
        PlotView.Plot.Axes.Bottom.TickLabelStyle.FontSize = 14;

        using var paint = ScottPlot.Paint.NewDisposablePaint();
        var largestLabelWidth = labels
            .Select(label => PlotView.Plot.Axes.Bottom.TickLabelStyle.Measure(label, paint).Size.Width)
            .DefaultIfEmpty(0f)
            .Max();
        PlotView.Plot.Axes.Bottom.MinimumSize = largestLabelWidth;
        PlotView.Plot.Axes.Right.MinimumSize = largestLabelWidth;

        PlotView.Plot.Title(title);
        PlotView.Plot.Axes.Margins(bottom: 0);
        PlotView.Refresh();
    }

    private void DisplaySemesterTab()
    {
        SemesterTogglePanel.Visibility = _currentType == ScheduleType.Maintenance ? Visibility.Visible : Visibility.Collapsed;

        var summary = _scheduleService.ComputeSemesterSummary(_currentType, _semester);
        var year = int.TryParse(YearTextBox.Text, out var parsedYear) ? parsedYear : _year;
        var title = _currentType == ScheduleType.Maintenance
            ? (_semester == Semester.FirstHalf ? $"Ene - Jun {year}" : $"Jul - Dic {year}")
            : $"Año completo {year}";

        SemesterSummaryTextBlock.Text = $"{title} — Programados: {summary.Scheduled}    Ejecutados: {summary.Executed}";

        DisplayPie(title, summary);
    }

    private void DisplayPie(string title, SemesterSummary summary)
    {
        SemesterPlotView.Plot.Clear();
        SemesterPlotView.Plot.Title(title);

        if (summary.Scheduled == 0 && summary.Executed == 0)
        {
            SemesterPlotView.Refresh();
            return;
        }

        var notExecuted = Math.Max(0, summary.Scheduled - summary.Executed);

        var slices = new List<PieSlice>
        {
            new() { Value = summary.Executed, FillColor = SuccessPlotColor, Label = "Ejecutados", LegendText = "Ejecutados" },
            new() { Value = notExecuted, FillColor = NeutralPlotColor, Label = "No ejecutados", LegendText = "No ejecutados" }
        };

        SemesterPlotView.Plot.Add.Pie(slices);
        SemesterPlotView.Plot.ShowLegend();
        SemesterPlotView.Refresh();
    }

    private void UpdateComplianceLevelView()
    {
        if (_selectedComplianceArea is null)
        {
            ComplianceBreadcrumbButton.Content = "Todas las áreas";
            ComplianceBreadcrumbButton.IsEnabled = false;
            ComplianceAreaColumnHeader.Header = "Área";
            ComplianceLevelDataGrid.ItemsSource = _indicators.ByArea
                .Select(a => new ComplianceRow(a.Area, a.Scheduled, a.Executed, a.CompliancePercentage, a.MaxDelayWeeks, a.Semaphore, a))
                .ToList();
        }
        else
        {
            ComplianceBreadcrumbButton.Content = $"◀ {_selectedComplianceArea.Area}";
            ComplianceBreadcrumbButton.IsEnabled = true;
            ComplianceAreaColumnHeader.Header = "Equipo";
            ComplianceLevelDataGrid.ItemsSource = _selectedComplianceArea.ByEquipment
                .Select(eq => new ComplianceRow(EquipmentLabel(eq.EquipmentName, eq.InventoryTag), eq.Scheduled, eq.Executed, eq.CompliancePercentage, eq.MaxDelayWeeks, eq.Semaphore, null))
                .ToList();
        }
    }

    private void RefreshAlerts()
    {
        var alerts = _scheduleService.GetUpcomingAlerts();
        var overdueCount = alerts.Count(a => a.DueDate < DateTime.Today);
        var upcomingCount = alerts.Count - overdueCount;

        AlertSummaryTextBlock.Text = alerts.Count == 0
            ? "Sin alertas de vencimiento."
            : $"🔴 {overdueCount} vencidos   🟡 {upcomingCount} próximos (<30 días)";

        AlertToggleButton.Visibility = alerts.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        DisplayAlertGrid(alerts);

        if (alerts.Count == 0)
        {
            AlertDataGrid.Visibility = Visibility.Collapsed;
        }

        AlertToggleButton.Content = AlertDataGrid.Visibility == Visibility.Visible ? "Ocultar detalle ▴" : "Ver detalle ▾";
    }

    private void DisplayAlertGrid(IReadOnlyList<UpcomingAlert> alerts)
    {
        AlertDataGrid.ItemsSource = alerts.Select(alert =>
        {
            var daysUntilDue = (alert.DueDate - DateTime.Today).Days;
            var status = daysUntilDue < 0
                ? $"Venció hace {-daysUntilDue} día{(daysUntilDue == -1 ? "" : "s")}"
                : $"Vence en {daysUntilDue} día{(daysUntilDue == 1 ? "" : "s")}";

            return new AlertRow(
                alert.Type == ScheduleType.Maintenance ? "Mantenimiento" : "Calibración",
                alert.Area,
                EquipmentLabel(alert.EquipmentName, alert.InventoryTag),
                status,
                daysUntilDue < 0);
        }).ToList();
    }

    private static string EquipmentLabel(string equipmentName, string? inventoryTag) =>
        string.IsNullOrWhiteSpace(inventoryTag) ? equipmentName : $"{equipmentName} ({inventoryTag})";

    private sealed record AreaRow(string Area, int Scheduled, int Executed, double CompliancePercentage, int OnTime, int Late, int Overdue, AreaCompliance? Model)
    {
        public string CompliancePercentDisplay => $"{CompliancePercentage:0.0}%";
    }

    private sealed record ComplianceRow(string Area, int Scheduled, int Executed, double CompliancePercentage, int MaxDelayWeeks, ComplianceSemaphore Semaphore, AreaCompliance? Model)
    {
        public string CompliancePercentDisplay => $"{CompliancePercentage:0.0}%";

        public string SemaphoreLabel => Semaphore switch
        {
            ComplianceSemaphore.Red => "Rojo",
            ComplianceSemaphore.Yellow => "Amarillo",
            _ => "Verde"
        };

        public Brush SemaphoreBrush => Semaphore switch
        {
            ComplianceSemaphore.Red => CriticalBrush,
            ComplianceSemaphore.Yellow => WarningBrush,
            _ => SuccessBrush
        };

        public Brush SemaphoreForeground => Semaphore == ComplianceSemaphore.Yellow ? Brushes.Black : Brushes.White;
    }

    private sealed record AlertRow(string Type, string Area, string Equipment, string Status, bool Overdue)
    {
        public Brush StatusBrush => Overdue ? CriticalBrush : WarningBrush;
        public Brush StatusForeground => Overdue ? Brushes.White : Brushes.Black;
    }
}
