using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class StatsCalculations
    {
        public double Balance { get; set; }

        public Dictionary<string, double> StatsPerCategotyAndTotalCalc(List<ExpensesObj> Expenses)
        {
            double totalExpenses = 0;
            double totalIncomes = 0;

            Dictionary<string, double> statsDictionary = new Dictionary<string, double>();

            foreach (var expensesObj in Expenses)
            {
                if (expensesObj.Category != "Income")
                {
                    if (statsDictionary.ContainsKey(expensesObj.Category))
                    {
                        statsDictionary[expensesObj.Category] += expensesObj.ActualAmount;
                    }
                    else
                    {
                        statsDictionary.Add(expensesObj.Category, expensesObj.ActualAmount);
                    }
                    totalExpenses += expensesObj.ActualAmount;
                }
                else
                {
                    totalIncomes += expensesObj.ActualAmount;
                }
            }

            Balance = totalIncomes - totalExpenses;
            return statsDictionary;
        }


        
    }
}
