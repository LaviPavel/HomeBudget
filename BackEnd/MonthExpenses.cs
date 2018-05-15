using System;
using System.Collections.ObjectModel;
using log4net;

namespace BackEnd
{
    public class ExpensesObj
    {
        private Guid _idGuid;
        private string _category;
        private string _subCategory;
        private double _expectedAmount;
        private double _actualAmount;
        private string _description;

        public delegate void VoidDelegate(Guid idGuid);
        public event VoidDelegate ExpensesObjChanged;

        public Guid IdGuid
        {
            get { return _idGuid;}
            set { _idGuid = value; }
        }
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
            IdGuid = Guid.NewGuid();
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
                ExpensesObjChanged(this.IdGuid);
            }
        }
    }

    public class MonthExpenses
    {
        private int _month;
        private int _year;
        private DbHandler DBhandler = new DbHandler();
        private static ILog _log = LogManager.GetLogger(typeof(MonthExpenses));

        public ObservableCollection<ExpensesObj> Expenses = new ObservableCollection<ExpensesObj>();

        public void LoadData(int month, int year)
        {
            _month = month;
            _year = year;
            DBhandler.MonthTableName = "MonthData_" + _month + "_" + _year;

            Expenses.Clear();
            foreach (var item in DBhandler.GetMonthDataFromDb())
            {
                Expenses.Add(item);
            }
        }
        public void UpdateExObj_ToDB(UpdateAction action, ExpensesObj objToActionOn)
        {
            DBhandler.UpdateObjInMonthTable(action, objToActionOn);
        }
    }

}
