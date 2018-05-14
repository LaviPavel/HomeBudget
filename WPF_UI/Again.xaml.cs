using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using BackEnd;

namespace WPF_UI
{
    public partial class Again
    {
        public static MonthExpenses MonthlyExpenses { get; set; } = new MonthExpenses();

        public Again()
        {
            DataContext = this;
            InitializeComponent();

            MonthlyExpenses.Expenses.CollectionChanged += Expenses_CollectionChanged;
            DataGrid.ItemsSource = MonthlyExpenses.Expenses;
        }

        private void Expenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ExpensesObj addedExpensesObj = MonthlyExpenses.Expenses.Last();
                    addedExpensesObj.ExpensesObjChanged += Item_ExpensesObjChanged;
                    MonthlyExpenses.AddRemoveExpensesObjDbTable(UpdateAction.Add, addedExpensesObj);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removeExpensesObj = e.OldItems[0] as ExpensesObj;
                    MonthlyExpenses.AddRemoveExpensesObjDbTable(UpdateAction.Remove, removeExpensesObj);
                    break;
                default:
                    foreach (var item in MonthlyExpenses.Expenses)
                    {
                        item.ExpensesObjChanged += Item_ExpensesObjChanged;
                    }
                    break;
            }
        }

        private void Item_ExpensesObjChanged(Guid expensesObjGuid)
        {
            MonthlyExpenses.UpdateExpensesObjAtDbTable(expensesObjGuid);
        }

        private void DataGridNewRowButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MonthlyExpenses.Expenses.Add(new ExpensesObj("new", "new", 0, 0));
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            ExpensesObj removeExpensesObj = ((FrameworkElement)sender).DataContext as ExpensesObj;
            MonthlyExpenses.Expenses.Remove(removeExpensesObj);
        }
    }
}
