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
        Task LoadData(DateTime dateTime);
        Task LoadDataRange(DateTime startDateTime, DateTime endDateTime);
        Task LoadDataRange(Dictionary<int, int> selectedDates);
        void UpdateExObj_ToDB(UpdateAction action, ExpensesObj objToActionOn);
    }
}
