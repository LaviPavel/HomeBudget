using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public interface IBackEnd
    {
        ObservableCollection<ExpensesObj> Expenses { get; set; }

        double GetBalance();
        Dictionary<string, double> GetExpensesPerCategory();
        void LoadData(DateTime dateTime);
        void LoadDataRange(DateTime startDateTime, DateTime endDateTime);
        void LoadDataRange(Dictionary<int, int> selectedDates);
        void UpdateExObj_ToDB(UpdateAction action, ExpensesObj objToActionOn);
    }
}
