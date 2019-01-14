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
        private Func<ChartPoint, string> PointLabel { get; set; }

        public static ExpensesTab ExpensesTabInstance;
        public static AnalysisTab AnalysisTabInstance;

        public ExpensesManager()
        {
            DataContext = this;
            ExpensesTabInstance = ExpensesTab.Instance;
            AnalysisTabInstance = AnalysisTab.Instance;
            PieChartSeriesCollection = new SeriesCollection();

            InitializeComponent();
            Expenses.CollectionChanged += Expenses_CollectionChanged;

            AnalysisTabInstance.PropertyChanged += AnalysisTabInstance_PropertyChanged;
            ExpensesTabInstance.PropertyChanged += ExpensesTabInstance_PropertyChanged;

            PointLabel = chartPoint =>
                $"{chartPoint.Y} ({chartPoint.Participation:P})";
        }

       #region ExpensesTab

        private string _mothBalanceValue;
        public string MothBalanceValue
        {
            get => _mothBalanceValue;
            set
            {
                _mothBalanceValue = value;
                OnPropertyChanged("MothBalanceValue");
            }
        }
        private SolidColorBrush _mothBalanceColor;
        public SolidColorBrush MothBalanceColor
        {
            get => _mothBalanceColor;

            set
            {
                _mothBalanceColor = value;
                OnPropertyChanged("MothBalanceColor");
            }
        }

        private string _expensesNotificationMessage;
        public string ExpensesNotificationMessage
        {
            get => _expensesNotificationMessage;
            set
            {
                _expensesNotificationMessage = value;
                OnPropertyChanged("ExpensesNotificationMessage");
            }
        }

        private void ExpensesTabInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as ExpensesTab;
            ExpensesNotificationMessage = s.NotificationMessage;
        }

        public SeriesCollection PieChartSeriesCollection { get; set; }
        public ObservableCollection<ExpensesObj> Expenses
        {
            get => ExpensesTabInstance.Expenses;
            set => value = ExpensesTabInstance.Expenses;
        }

        
        private async void DataGridNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate != null)
            {
                Expenses.Add(new ExpensesObj("new", "new", 0, 0, Guid.NewGuid()));
            }
            else
            {
                ExpensesNotificationMessage = "Please select Moth to fill monthly expenses ";
                NotificationBoxClearAsync();
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
            foreach (var expensesObj in ExpensesTabInstance.Expenses)
            {
                if (expensesObj.IdGuid == toUpdateObjGuid)
                {
                    ExpensesTabInstance.UpdateExpensesObject(UpdateAction.Update, expensesObj);
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
                await UpdatePieStatsAsync(ExpensesTabInstance.GetExpensesPerCategory());
                await UpdateBalanceAsync(ExpensesTabInstance.GetExpensesBalance());
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
                    ExpensesTabInstance.UpdateExpensesObject(UpdateAction.Add, addedExpensesObj);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removeExpensesObj = e.OldItems[0] as ExpensesObj;
                    ExpensesTabInstance.UpdateExpensesObject(UpdateAction.Remove, removeExpensesObj);
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


        #region AnalysisTab

        private string _analysisNotificationMessage;
        public string AnalysisNotificationMessage
        {
            get => _analysisNotificationMessage;
            set
            {
                _analysisNotificationMessage = value;
                OnPropertyChanged("AnalysisNotificationMessage");
            }
        }

        private void AnalysisTabInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as AnalysisTab;
            AnalysisNotificationMessage = s.NotificationMessage;
            NotificationBoxClearAsync();
        }

        #endregion

        //todo: what if there are multiple errors between the delays, some will be cleared after 1 seconds or so
        private async Task NotificationBoxClearAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            ExpensesNotificationTextBox.Clear();
            AnalysisNotificationTextBox.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        
    }
}
