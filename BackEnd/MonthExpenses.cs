using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExpensesObj
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public double ExpectedAmount { get; set; }
        public double ActualAmount { get; set; }
        public string Description { get; set; }


        public ExpensesObj(string categoryName, string subCategoryName, double expectedAmount, double actualAmount,
            string description = null)
        {
            CategoryName = categoryName;
            SubCategoryName = subCategoryName;
            ExpectedAmount = expectedAmount;
            ActualAmount = actualAmount;
            Description = description;

        }
    }

    public class MonthExpenses
    {
        private int _month;
        private int _year;
        public Collection<ExpensesObj> Expenses;
        private DbHandler DBhandler = new DbHandler();

        public void LoadData(int month, int year)
        {
            _month = month;
            _year = year;
            var tableName = "MonthData_" + _month + "_" + _year;

            Expenses = DBhandler.GetMonthDataFromDb(tableName);
        }
        
        public void SaveData()
        {
            //save data
        }

    }
}
