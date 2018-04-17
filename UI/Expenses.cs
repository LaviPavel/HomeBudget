using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    static class Expenses
    {
        public static List<ExpenceObj> AllExpences = new List<ExpenceObj>();
        private static int _totalExpences = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void InitDemo()
        {
            var salary = new ExpenceObj("Salary");
            salary.AddSubCategory("income1", 5000);
            AllExpences.Add(salary);

            var bills = new ExpenceObj("Bills");
            bills.AddSubCategory("Electricity", 200);
            bills.AddSubCategory("Water", 60);
            AllExpences.Add(bills);

            var vehicles = new ExpenceObj("Vehicles");
            vehicles.AddSubCategory("Dylan", 200);
            vehicles.AddSubCategory("Sami", 300);
            AllExpences.Add(vehicles);
        }

        public static ExpenceObj GetExpenceObjByName(string categoryName)
        {
            foreach (var obj in AllExpences)
            {
                if (obj.CategoryName == categoryName)
                {
                    return obj;
                }
            }

            return null;
        }

        public static int CalcTotalBalance()
        {
            int totalsalary = 0;

            foreach (var expCategory in AllExpences)
            {
                if (expCategory.CategoryName == "Salary")
                {
                    
                    foreach (var subCategory in expCategory.GetAllSubCategory())
                    {
                        totalsalary += subCategory.ActoualAmount;
                    }
                }
            }

            return totalsalary - _totalExpences;
        }

        public static Dictionary<string, int> CalcExpensesPerCategory()
        {
            Dictionary < string, int> expencesCalc = new Dictionary<string, int>();
            _totalExpences = 0;

            foreach (var expCategory in AllExpences)
            {
                if (expCategory.CategoryName == "Salary") { continue; }

                int sum=0;
                foreach (var subCategory in expCategory.GetAllSubCategory())
                {
                    sum += subCategory.ActoualAmount;
                }
                expencesCalc.Add(expCategory.CategoryName, sum);
                _totalExpences += sum;
            }

            return expencesCalc;
        }

    }
}
