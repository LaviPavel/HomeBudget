using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Analysis : IBackEnd
    {
        private static Analysis _instance;
        private readonly IDbHandler _dBhandler;
        private StatsAndGraphs _statsAndGraphs;

        public static Analysis Instance => _instance ?? (_instance = new Analysis());
        public ObservableCollection<ExpensesObj> Expenses { get; set; }

        private Analysis()
        {
            _statsAndGraphs = new StatsAndGraphs();
            _dBhandler = DbHandler.Instance;
            Expenses = new ObservableCollection<ExpensesObj>();

            CalcStats();
        }

        private async void CalcStats()
        {
            await _statsAndGraphs.CalcStats(Expenses.ToList());
        }

        public async Task LoadDataRange(DateTime startDateTime, DateTime endDateTime)
        {
            var date = startDateTime;

            while (date <= endDateTime)
            {
                foreach (var item in _dBhandler.GetMonthDataFromDb(date))
                {
                    Expenses.Add(item);
                }                

                date = date.AddMonths(1);
            }

            CalcStats();
        }

        public Dictionary<string, double> GetExpensesPerCategory()
        {
            return _statsAndGraphs.ExpensesPerCategory;
        }

        public double GetBalance()
        {
            return _statsAndGraphs.PeriodBalance;
        }

        public async Task LoadDataRange(Dictionary<int, int> selectedDates)
        {
            throw new NotImplementedException();
        }

        #region NotRelevantToAnalysis
        
        public async Task LoadData(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateExObj_ToDB(UpdateAction action, ExpensesObj objToActionOn)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
