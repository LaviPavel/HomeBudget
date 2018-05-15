using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BackEnd;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_UI
{
    public partial class Again
    {
        private WidgetsStats ChartsStats { get; set; }
        
        public SeriesCollection PieChartSeriesCollection { get; set; } = new SeriesCollection();
        public static MonthExpenses MonthlyExpenses { get; set; } = new MonthExpenses();
        private Func<ChartPoint, string> PointLabel { get; set; }

        public Again()
        {
            DataContext = this;
            InitializeComponent();

            MonthlyExpenses.Expenses.CollectionChanged += Expenses_CollectionChanged;
            DataGrid.ItemsSource = MonthlyExpenses.Expenses;

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            ChartsStats = new WidgetsStats();
        }

        private async Task NotificationBoxClearAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            NotificationTextBox.Clear();
        }
        private async Task CalcPieStatsAsync()
        {

            foreach (var item in ChartsStats.CalcPieChartStats(MonthlyExpenses.Expenses.ToList()))
            {
                PieChartSeriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<double> { item.Value },
                    PushOut = 5,
                    DataLabels = true,
                    LabelPoint = PointLabel
                });
            }
        }
        private async void TriggerCalcPieStatsAsync()
        {
            PieChartSeriesCollection.Clear();
            try
            {
                await CalcPieStatsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void DataGridNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate != null)
            {
                MonthlyExpenses.Expenses.Add(new ExpensesObj("new", "new", 0, 0));
            }
            NotificationTextBox.Text = "Please select Moth to fill monthly expenses ";
            await NotificationBoxClearAsync();
        }
        private void Expenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TriggerCalcPieStatsAsync();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ExpensesObj addedExpensesObj = MonthlyExpenses.Expenses.Last();
                    addedExpensesObj.ExpensesObjChanged += Item_ExpensesObjChanged;
                    MonthlyExpenses.UpdateExObj_ToDB(UpdateAction.Add, addedExpensesObj);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removeExpensesObj = e.OldItems[0] as ExpensesObj;
                    MonthlyExpenses.UpdateExObj_ToDB(UpdateAction.Remove, removeExpensesObj);
                    break;
                default:
                    foreach (var item in MonthlyExpenses.Expenses)
                    {
                        item.ExpensesObjChanged += Item_ExpensesObjChanged;
                    }
                    break;
            }
        }
        private void Item_ExpensesObjChanged(Guid ToUpdateObjGuid)
        {
            TriggerCalcPieStatsAsync();
            foreach (var expensesObj in MonthlyExpenses.Expenses)
            {
                if (expensesObj.IdGuid == ToUpdateObjGuid)
                {
                    MonthlyExpenses.UpdateExObj_ToDB(UpdateAction.Update, expensesObj);
                    break;
                }
            }
        }
        private void OnDelete(object sender, RoutedEventArgs e)
        {
            ExpensesObj removeExpensesObj = ((FrameworkElement)sender).DataContext as ExpensesObj;
            MonthlyExpenses.Expenses.Remove(removeExpensesObj);
        }
    }
}
