using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ExpensesTab
    {
        private static ExpensesTab _instance;
        public static IBackEnd _monthlyExpenses;

        public static ExpensesTab Instance => _instance ?? (_instance = new ExpensesTab());

        public ObservableCollection<ExpensesObj> Expenses
        {
            get => _monthlyExpenses.Expenses;
            set => value = _monthlyExpenses.Expenses;
        }

        private ExpensesTab()
        {
            _monthlyExpenses = MonthExpenses.Instance;
        }

        public static void LoadData(DateTime monthToLoad)
        {
            _monthlyExpenses.LoadData(monthToLoad);
        }

        public void UpdateExpensesObject(UpdateAction update, ExpensesObj expensesObj)
        {
            _monthlyExpenses.UpdateExObj_ToDB(update, expensesObj);
        }

        public Dictionary<string, double> GetExpensesPerCategory()
        {
            return _monthlyExpenses.GetExpensesPerCategory();
        }

        public double GetExpensesBalance()
        {
            return _monthlyExpenses.GetBalance();
        }

    }
}
