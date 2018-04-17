using System;
using System.Data;
using log4net;

namespace BackEnd
{
    public class ConnectingLayer
    {
        private static ILog _log = LogManager.GetLogger(typeof(ConnectingLayer));
        private MonthExpenses _monthlyExpenses = new MonthExpenses();
        public DataTable MonthlyExpensesDataTable = new DataTable();

        public void InitDatatable()
        {
            MonthlyExpensesDataTable.Columns.Add(new DataColumn("Category"));
            MonthlyExpensesDataTable.Columns.Add(new DataColumn("SubCategory"));
            MonthlyExpensesDataTable.Columns.Add(new DataColumn("Expected Amount"));
            MonthlyExpensesDataTable.Columns.Add(new DataColumn("Actual Amount"));
            MonthlyExpensesDataTable.Columns.Add(new DataColumn("Description"));
        }

        public void UpdateDataTableData(int month, int year)
        {
            try
            {
                _monthlyExpenses.LoadData(month, year);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Error accrued while loading MonthData {0}", ex);
            }
            

            foreach (var rowData in _monthlyExpenses.Expenses)
            {
                MonthlyExpensesDataTable.Rows.Add(rowData.CategoryName, rowData.SubCategoryName, rowData.ExpectedAmount, rowData.ActualAmount, rowData.Description);
            }
            MonthlyExpensesDataTable.AcceptChanges();
        }

    }
}