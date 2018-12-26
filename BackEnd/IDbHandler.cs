using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace BackEnd
{
    public interface IDbHandler
    {
        SQLiteConnection Connection { get; set; }
        void UpdateObjInMonthTable(UpdateAction action, ExpensesObj newExpensesObj, DateTime dateTime);
        ObservableCollection<ExpensesObj> GetMonthDataFromDb(DateTime dateTime);

    }
}
