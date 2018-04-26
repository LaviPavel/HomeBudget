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
            MonthlyExpensesDataTable.AcceptChanges();
        }

        public void LoadDataTableData(int month, int year)
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

        public void HandleDataTableUpdate(int row, int column)
        {

            
            if (_monthlyExpenses.Expenses.Count < row + 1)
            {
                if (column == 1)
                {
                    if (MonthlyExpensesDataTable.Rows[row][0] == null)
                    {
                        
                    }
                        //add only is there is a category and subcategory
                        // check all the rest for null, add new expenses based on the check.
                    _monthlyExpenses.Expenses.Add(new ExpensesObj(MonthlyExpensesDataTable.Rows[row][0].ToString(), MonthlyExpensesDataTable.Rows[row][1].ToString(), 0, 0));
                }

            }
            else
            {
                var dataTableChangedCell = MonthlyExpensesDataTable.Rows[row][column];
                switch (column)
                {
                    case 0:
                        if (!dataTableChangedCell.Equals(_monthlyExpenses.Expenses[row].CategoryName))
                        {
                            _monthlyExpenses.Expenses[row].CategoryName = dataTableChangedCell.ToString();
                        }
                        break;
                    case 1:
                        if (!dataTableChangedCell.Equals(_monthlyExpenses.Expenses[row].SubCategoryName))
                        {
                            _monthlyExpenses.Expenses[row].SubCategoryName = dataTableChangedCell.ToString();
                        }
                        break;
                    case 2:
                        if (!dataTableChangedCell.Equals(_monthlyExpenses.Expenses[row].ExpectedAmount))
                        {
                            _monthlyExpenses.Expenses[row].ExpectedAmount = Convert.ToDouble(dataTableChangedCell);
                        }
                        break;
                    case 3:
                        if (!dataTableChangedCell.Equals(_monthlyExpenses.Expenses[row].ActualAmount))
                        {
                            _monthlyExpenses.Expenses[row].ActualAmount = Convert.ToDouble(dataTableChangedCell);
                        }
                        break;
                    case 4:
                        if (!dataTableChangedCell.Equals(_monthlyExpenses.Expenses[row].Description))
                        {
                            _monthlyExpenses.Expenses[row].Description = dataTableChangedCell.ToString();
                        }
                        break;
                    default:
                        throw new Exception("illigal column " + dataTableChangedCell);
                }
                
            }

            //todo: as new thread with db lock
            //db
            // if new category => add
            // if new subcategory => check with categories and add/update
            // add/update month table


        }

    }
}