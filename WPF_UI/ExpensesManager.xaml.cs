using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BackEnd;
using LiveCharts;

namespace WPF_UI
{
    public partial class ExpensesManager
    {
        private ExpensesTab _expensesTab;
        public static IBackEnd _monthlyExpenses = MonthExpenses.Instance;
        public static IBackEnd _expensesAnalysis = Analysis.Instance;

        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection PieChartSeriesCollection { get; set; } = new SeriesCollection();
        public static ObservableCollection<ExpensesObj> Expenses
        {
            get => _monthlyExpenses.Expenses;
            set => value = _monthlyExpenses.Expenses;
        }

        public ExpensesManager()
        {
            DataContext = this;
            InitializeComponent();

           Expenses.CollectionChanged += Expenses_CollectionChanged;
        }

        private async Task NotificationBoxClearAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            NotificationTextBox.Clear();
        }
        private void OnDelete(object sender, RoutedEventArgs e)
        {
            _expensesTab.OnDelete(sender, e);
        }
        private void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            _expensesTab.DataGrid_OnPreviewKeyDown(sender, e);
        }
        private void DataGridNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            _expensesTab.DataGridNewRowButton_Click(sender, e);
        }
        private void Expenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _expensesTab.Expenses_CollectionChanged(sender, e);
        }

        public async Task UiNotification(string message)
        {
            NotificationTextBox.Text = message;
            await NotificationBoxClearAsync();
        }

    }
}
