using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BackEnd;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;

namespace WPF_UI
{
    public partial class ExpensesManager : INotifyPropertyChanged
    {
        private ExpensesTab _expensesTab;
        private AnalysisTab _analisysTab;

        private Func<ChartPoint, string> PointLabel { get; set; }
        
        public ExpensesManager()
        {
            DataContext = this;
            _expensesTab = ExpensesTab.Instance;
            _analisysTab = AnalysisTab.Instance;
            PieChartSeriesCollection = new SeriesCollection();

            InitializeComponent();
            Expenses.CollectionChanged += Expenses_CollectionChanged;

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #region ExpensesTab

        private string mothBalanceValue;
        public string MothBalanceValue
        {
            get
            {
                return mothBalanceValue;

            }
            set
            {
                mothBalanceValue = value;
                OnPropertyChanged("MothBalanceValue");
            }
        }

        private SolidColorBrush mothBalanceColor;
        public SolidColorBrush MothBalanceColor
        {
            get { return mothBalanceColor; }

            set
            {
                mothBalanceColor = value;
                OnPropertyChanged("MothBalanceColor");
            }
        }

        public SeriesCollection PieChartSeriesCollection { get; set; }
        public ObservableCollection<ExpensesObj> Expenses
        {
            get => _expensesTab.Expenses;
            set => value = _expensesTab.Expenses;
        }

        
        private async void DataGridNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate != null)
            {
                Expenses.Add(new ExpensesObj("new", "new", 0, 0, Guid.NewGuid()));
            }
            else
            {
                await UiNotification("Please select Moth to fill monthly expenses ");
            }
        }
        private async Task UpdatePieStatsAsync(Dictionary<string, double> expensesPerCategory)
        {
            foreach (var item in expensesPerCategory)
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
        private void Item_ExpensesObjChanged(Guid toUpdateObjGuid)
        {
            foreach (var expensesObj in _expensesTab.Expenses)
            {
                if (expensesObj.IdGuid == toUpdateObjGuid)
                {
                    _expensesTab.UpdateExpensesObject(UpdateAction.Update, expensesObj);
                    break;
                }
            }
            TriggerStatsCalcAsync();
        }
        private async Task UpdateBalanceAsync(double balance)
        {
            var fontColor = Brushes.Green;
            if (balance < 0)
            {
                fontColor = Brushes.Red;
            }

            MothBalanceValue = balance.ToString(CultureInfo.InvariantCulture);
            MothBalanceColor = fontColor;
        }
        private async void TriggerStatsCalcAsync()
        {
            PieChartSeriesCollection.Clear();
            try
            {
                await UpdatePieStatsAsync(_expensesTab.GetExpensesPerCategory());
                await UpdateBalanceAsync(_expensesTab.GetExpensesBalance());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Expenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ExpensesObj addedExpensesObj = Expenses.Last();
                    addedExpensesObj.ExpensesObjChanged += Item_ExpensesObjChanged;
                    _expensesTab.UpdateExpensesObject(UpdateAction.Add, addedExpensesObj);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removeExpensesObj = e.OldItems[0] as ExpensesObj;
                    _expensesTab.UpdateExpensesObject(UpdateAction.Remove, removeExpensesObj);
                    break;
                default:
                    foreach (var item in Expenses)
                    {
                        item.ExpensesObjChanged += Item_ExpensesObjChanged;
                    }
                    break;
            }

            TriggerStatsCalcAsync();
        }
        private void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dg = sender as DataGrid;

            // alter this condition for whatever valid keys you want - avoid arrows/tab, etc.
            if (dg != null && !dg.IsReadOnly && StaticMethods.isAlphanumeric(e.Key))
            {
                var cell = dg.GetSelectedCell();
                if (cell != null && cell.Column is DataGridTemplateColumn)
                {
                    cell.Focus();
                    dg.BeginEdit();

                    TextBox textbox = StaticMethods.FindVisualChild<TextBox>(cell);
                    if (textbox != null && textbox.IsFocused == false)
                    {
                        textbox.Focus();

                        textbox.Clear();
                        textbox.AppendText(StaticMethods.GetCharFromKey(e.Key).ToString());
                        textbox.CaretIndex = textbox.Text.Length;
                    }

                    e.Handled = true;
                }
            }
        }
        private void OnDelete(object sender, RoutedEventArgs e)
        {
            //todo: if rowid 0 popup delete all
            ExpensesObj removeExpensesObj = ((FrameworkElement)sender).DataContext as ExpensesObj;
            Expenses.Remove(removeExpensesObj);
        }
        #endregion

        #region AnalisysTab

        #endregion

        private async Task NotificationBoxClearAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            NotificationTextBox.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public async Task UiNotification(string message)
        {
            NotificationTextBox.Text = message;
            await NotificationBoxClearAsync();
        }

    }
}
