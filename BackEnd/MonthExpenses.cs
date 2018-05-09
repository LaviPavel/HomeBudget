using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Timer = System.Timers.Timer;

namespace BackEnd
{
    public class ExpensesObj
    {
        private string _category;
        private string _subCategory;
        private double _expectedAmount;
        private double _actualAmount;
        private string _description;

        public delegate void VoidDelegate();

        public event VoidDelegate ExpensesObjChanged;

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnExpensesObjChanged();
            }
        }

        public string SubCategory
        {
            get { return _subCategory; }
            set
            {
                _subCategory = value;
                OnExpensesObjChanged();
            }
        }

        public double ExpectedAmount
        {
            get { return _expectedAmount; }
            set
            {
                _expectedAmount = value;
                OnExpensesObjChanged();
            }
        }

        public double ActualAmount
        {
            get { return _actualAmount; }
            set
            {
                _actualAmount = value;
                OnExpensesObjChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnExpensesObjChanged();
            }
        }


        public ExpensesObj(string category, string subCategory, double expectedAmount, double actualAmount,
            string description = null)
        {
            Category = category;
            SubCategory = subCategory;
            ExpectedAmount = expectedAmount;
            ActualAmount = actualAmount;
            Description = description;

        }

        protected virtual void OnExpensesObjChanged()
        {
            if (ExpensesObjChanged != null)
            {
                ExpensesObjChanged();
            }
        }

    }

    public class MonthExpenses
    {
        private int _month;
        private int _year;
        private string _tableName;
        private DbHandler DBhandler = new DbHandler();
        private static ILog _log = LogManager.GetLogger(typeof(MonthExpenses));
        private int _saveTriggerCount = 0;
        private TimeSpan _SaveToDbTimeout = TimeSpan.FromMinutes(1);
        private bool _IsSaveTriggered;
        private DateTime _startTime;
        private Task _saveToDbTask;

        public ObservableCollection<ExpensesObj> Expenses = new ObservableCollection<ExpensesObj>();

        public void LoadData(int month, int year)
        {
            _month = month;
            _year = year;
            _tableName = "MonthData_" + _month + "_" + _year;
            Expenses.Clear();

            foreach (var item in DBhandler.GetMonthDataFromDb(_tableName))
            {
                Expenses.Add(item);
            }
        }

        public async void SaveData(bool force = false)
        {
            _saveTriggerCount++;

            if (!_IsSaveTriggered)
            {
                _startTime = DateTime.Now;
                _IsSaveTriggered = true;
            }

            if ((_saveToDbTask == null || _saveToDbTask.IsCompleted) &&
                (_saveTriggerCount > 10 || (DateTime.Now - _startTime > _SaveToDbTimeout) || force))
            {
                _saveTriggerCount = 0;
                _IsSaveTriggered = false;

                _saveToDbTask = DBhandler.SaveToDb();
                await _saveToDbTask;
            }
        }


    }

}
