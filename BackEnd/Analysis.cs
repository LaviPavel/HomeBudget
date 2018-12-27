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
        }

        public async void LoadDataRange(DateTime startDateTime, DateTime endDateTime)
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
            await _statsAndGraphs.CalcStats(Expenses.ToList());
        }

        public Dictionary<string, double> GetExpensesPerCategory()
        {
            return _statsAndGraphs.ExpensesPerCategory;
        }

        public void LoadDataRange(Dictionary<int, int> selectedDates)
        {
            throw new NotImplementedException();
        }

        #region NotRelevantToAnalysis
        
        public double GetBalance()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime dateTime)
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
