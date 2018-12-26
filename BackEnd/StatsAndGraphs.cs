using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd
{
    class StatsAndGraphs : IStatsAndGraphs
    {
        public double PeriodBalance { get; set; }
        public Dictionary<string, double> ExpensesPerCategory { get; set; }


        public async Task CalcStats(List<ExpensesObj> expenses)
        {
            double totalExpenses = 0;
            double totalIncomes = 0;

            Dictionary<string, double> statsDictionary = new Dictionary<string, double>();

            foreach (var expensesObj in expenses)
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

            PeriodBalance = totalIncomes - totalExpenses;
            ExpensesPerCategory = statsDictionary;
        }

        
    }


}
