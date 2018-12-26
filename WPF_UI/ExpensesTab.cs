using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BackEnd;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_UI
{
    public class ExpensesTab : ExpensesManager
    {
        private static ExpensesTab _instance;
        public static ExpensesTab Instance => _instance ?? (_instance = new ExpensesTab());

        private ExpensesTab()
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }
        private async Task UpdatePieStatsAsync(Dictionary<string, double> ExpensesPerCategory)
        {
            foreach (var item in ExpensesPerCategory)
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

        public void Expenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TriggerStatsCalcAsync();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ExpensesObj addedExpensesObj = _monthlyExpenses.Expenses.Last();
                    addedExpensesObj.ExpensesObjChanged += Item_ExpensesObjChanged;
                    _monthlyExpenses.UpdateExObj_ToDB(UpdateAction.Add, addedExpensesObj);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removeExpensesObj = e.OldItems[0] as ExpensesObj;
                    _monthlyExpenses.UpdateExObj_ToDB(UpdateAction.Remove, removeExpensesObj);
                    break;
                default:
                    foreach (var item in _monthlyExpenses.Expenses)
                    {
                        item.ExpensesObjChanged += Item_ExpensesObjChanged;
                    }
                    break;
            }
        }

        private void Item_ExpensesObjChanged(Guid toUpdateObjGuid)
        {
            TriggerStatsCalcAsync();
            foreach (var expensesObj in _monthlyExpenses.Expenses)
            {
                if (expensesObj.IdGuid == toUpdateObjGuid)
                {
                    _monthlyExpenses.UpdateExObj_ToDB(UpdateAction.Update, expensesObj);
                    break;
                }
            }
        }
        private async Task UpdateBalanceAsync()
        {
            var fontColor = Brushes.Green;
            var balance = _monthlyExpenses.GetBalance();
            if (balance < 0)
            {
                fontColor = Brushes.Red;
            }

            TotalBalanceBox.Text = balance.ToString(CultureInfo.InvariantCulture);
            TotalBalanceBox.Foreground = fontColor;
        }

        private async void TriggerStatsCalcAsync()
        {
            PieChartSeriesCollection.Clear();
            try
            {
                await UpdatePieStatsAsync(_monthlyExpenses.GetExpensesPerCategory());
                await UpdateBalanceAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void DataGridNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate != null)
            {
                _monthlyExpenses.Expenses.Add(new ExpensesObj("new", "new", 0, 0, Guid.NewGuid()));
            }
            else
            {
                await UiNotification("Please select Moth to fill monthly expenses ");
            }

        }

        public void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
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

        public void OnDelete(object sender, RoutedEventArgs e)
        {
            //todo: if rowid 0 popup delete all
            ExpensesObj removeExpensesObj = ((FrameworkElement)sender).DataContext as ExpensesObj;
            _monthlyExpenses.Expenses.Remove(removeExpensesObj);
        }

    }
}
