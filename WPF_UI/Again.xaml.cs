using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using BackEnd;

namespace WPF_UI
{
    public partial class Again
    {
        private static MonthExpenses _monthlyExpenses = new MonthExpenses();

        public static MonthExpenses MonthlyExpenses
        {
            get { return _monthlyExpenses; }
            set { _monthlyExpenses = value; }
        }

        public Again()
        {
            DataContext = this;
            InitializeComponent();

            MonthlyExpenses.Expenses.CollectionChanged += Expenses_CollectionChanged;
            DataGrid.ItemsSource = MonthlyExpenses.Expenses;

            this.Closed += new EventHandler(MainWindow_Closed);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MonthlyExpenses.SaveData();
        }

        private void Expenses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in MonthlyExpenses.Expenses)
            {
                item.ExpensesObjChanged += Item_ExpensesObjChanged;
            }

        }

        private void Item_ExpensesObjChanged()
        {
            MonthlyExpenses.SaveData();
        }

        private void DataGridNewRowButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MonthlyExpenses.Expenses.Add(new ExpensesObj("new", "new", 0, 0));
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            ExpensesObj removExpensesObjobj = ((FrameworkElement)sender).DataContext as ExpensesObj;
            MonthlyExpenses.Expenses.Remove(removExpensesObjobj);
        }
    }
}
