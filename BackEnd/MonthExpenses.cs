using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using log4net;

namespace BackEnd
{
    public class MonthExpenses : IBackEnd
    {
        private static MonthExpenses _instance;
        private DateTime _loadedDataTime;
        private StatsAndGraphs _statsAndGraphs;
        private readonly IDbHandler _dBhandler;
        private ILog _log = LogManager.GetLogger(typeof(MonthExpenses));

        public ObservableCollection<ExpensesObj> Expenses { get; set; }

        private MonthExpenses()
        {
            _statsAndGraphs = new StatsAndGraphs();
            _dBhandler = DbHandler.Instance;
            Expenses = new ObservableCollection<ExpensesObj>();
            CalcStats();
        }
        public static MonthExpenses Instance => _instance ?? (_instance = new MonthExpenses());

        private async void CalcStats()
        {
            await _statsAndGraphs.CalcStats(Expenses.ToList());
        }

        public double GetBalance()
        {
            return _statsAndGraphs.PeriodBalance;
        }
        public Dictionary<string, double> GetExpensesPerCategory()
        {
            return _statsAndGraphs.ExpensesPerCategory;
        }


        public void LoadData(DateTime dateTime)
        {
            _loadedDataTime = dateTime;
            Expenses.Clear();

            foreach (var item in _dBhandler.GetMonthDataFromDb(dateTime))
            {
                Expenses.Add(item);
            }

            if (Expenses.Count < 1)
            {
                Expenses.Add(new ExpensesObj("Income", "Salary", 0, 1000, new Guid()));
            }

            CalcStats();
        }
        public void UpdateExObj_ToDB(UpdateAction action, ExpensesObj objToActionOn)
        {
            try
            {
                _dBhandler.UpdateObjInMonthTable(action, objToActionOn, _loadedDataTime);
                CalcStats();
            }
            catch (Exception ex)
            {
                //todo: throw notification
                throw ex;
            }
            
        }


        #region NotRelevantToMonthExpenses
        public void LoadDataRange(DateTime startDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }

        public void LoadDataRange(Dictionary<int, int> selectedDates)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
