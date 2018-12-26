using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal interface IStatsAndGraphs
    {
        Dictionary<string, double> ExpensesPerCategory { get; set; }
        double PeriodBalance { get; set; }

        Task CalcStats(List<ExpensesObj> expenses);

    }
}
