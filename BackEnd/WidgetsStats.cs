using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class WidgetsStats
    {
        public Dictionary<string, double> CalcPieChartStats(List<ExpensesObj> Expenses)
        {
            Dictionary<string, double> statsDictionary = new Dictionary<string, double>();

            foreach (var expensesObj in Expenses)
            {
                if (statsDictionary.ContainsKey(expensesObj.Category))
                {
                    statsDictionary[expensesObj.Category] += expensesObj.ActualAmount;
                }
                else
                {
                    statsDictionary.Add(expensesObj.Category, expensesObj.ActualAmount);
                }
            }

            return statsDictionary;
        }


    }
}
