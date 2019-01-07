using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace WPF_UI
{
    class AnalysisTab
    {
        private static AnalysisTab _instance;
        public static IBackEnd _expensesAnalysis;

        public static AnalysisTab Instance => _instance ?? (_instance = new AnalysisTab());

       private AnalysisTab()
        {
            _expensesAnalysis = Analysis.Instance;
        }

        public static void LoadDataRange(DateTime starTime, DateTime endTime)
        {
            _expensesAnalysis.LoadDataRange(starTime, endTime);
        }
    }
}
