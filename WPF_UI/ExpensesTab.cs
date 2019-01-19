using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BackEnd;
using WPF_UI.Annotations;

namespace WPF_UI
{
    public class ExpensesTab : INotifyPropertyChanged
    {
        private static ExpensesTab _instance;
        public static IBackEnd MonthlyExpenses;

        public static ExpensesTab Instance => _instance ?? (_instance = new ExpensesTab());

        public ObservableCollection<ExpensesObj> Expenses
        {
            get => MonthlyExpenses.Expenses;
            set => value = MonthlyExpenses.Expenses;
        }

        private string _notification;
        public string NotificationMessage
        {
            get => _notification;
            set
            {
                _notification = value;
                OnPropertyChanged(NotificationMessage);
            }
        }

        private ExpensesTab()
        {
            MonthlyExpenses = MonthExpenses.Instance;
        }

        public async void LoadData(DateTime monthToLoad)
        {
            MonthlyExpenses.LoadData(monthToLoad);
        }

        public void UpdateExpensesObject(UpdateAction update, ExpensesObj expensesObj)
        {
            MonthlyExpenses.UpdateExObj_ToDB(update, expensesObj);
        }

        public Dictionary<string, double> GetExpensesPerCategory()
        {
            return MonthlyExpenses.GetExpensesPerCategory();
        }

        public double GetExpensesBalance()
        {
            return MonthlyExpenses.GetBalance();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
