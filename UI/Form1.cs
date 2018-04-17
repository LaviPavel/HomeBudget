using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using MetroFramework.Forms;
using Color = System.Windows.Media.Color;

namespace UI
{
    public partial class Form1 : MetroForm
    {
        private ExpenceObj _expobj = new ExpenceObj();
        public Form1()
        {
            Expenses.InitDemo();
            InitializeComponent();
            InitDataCategories();
            
        }

        private void InitDataCategories()
        {
            Category.DataSource = Expenses.AllExpences;
            Category.DisplayMember = "CategoryName";
        }

        private void UpdateBalanceGadgetData()
        {
            Color cGood = Color.FromRgb(50, 205, 50);
            Color cBad = Color.FromRgb(220, 20, 60);
            Color toColor = cBad;
            int balance = Expenses.CalcTotalBalance();

            if (balance > 0)
            {
                toColor = cGood;
            }

            BalanceGauge.From = -3000;
            BalanceGauge.FromColor = cBad;
            BalanceGauge.To = 3000;
            BalanceGauge.Value = balance;
            BalanceGauge.ToColor = toColor;
        }

        private void UpdatePieGadgetData()
        {
            ExpensesPie.Series.Clear();
            var bla = Expenses.CalcExpensesPerCategory();
            foreach (var item in bla)
            {
                var pieSubject = new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<double> { item.Value },
                    PushOut = 15,
                    DataLabels = true,
                };

                ExpensesPie.Series.Add(pieSubject);
            }
        }

        private void monthDataGrid_CellEndEdit(
            object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewComboBoxCell catBoxCell = (DataGridViewComboBoxCell)MonthDataGrid.Rows[e.RowIndex].Cells[0];
            DataGridViewComboBoxCell subCatBoxCell = (DataGridViewComboBoxCell)MonthDataGrid.Rows[e.RowIndex].Cells[1];
            DataGridViewTextBoxCell expectedValue = (DataGridViewTextBoxCell)MonthDataGrid.Rows[e.RowIndex].Cells[2];
            DataGridViewTextBoxCell actualValue = (DataGridViewTextBoxCell)MonthDataGrid.Rows[e.RowIndex].Cells[3];

            switch (e.ColumnIndex)
            {
                case 0: // Update subCategories
                    _expobj = Expenses.GetExpenceObjByName(catBoxCell.Value.ToString());

                    // Set SubCategory Combobox values
                    subCatBoxCell.DataSource = _expobj.GetAllSubCategory();
                    subCatBoxCell.DisplayMember = "SubCategoryName";
                    break;

                case 1: // show expectedValue
                    if (catBoxCell.Value != null && subCatBoxCell != null)
                    {
                        expectedValue.Value = _expobj.GetExpectedValueByName(subCatBoxCell.Value.ToString());
                    }
                    break;

                case 2: // set expectedValue
                    if (catBoxCell.Value != null && subCatBoxCell != null)
                    {
                        _expobj.SetExpectedValueByName(subCatBoxCell.Value.ToString(), Convert.ToInt32(expectedValue.Value));
                    }
                    break;

                case 3: // set actualdValue
                    if (catBoxCell.Value != null && subCatBoxCell != null)
                    {
                        _expobj.SetActualValueByName(subCatBoxCell.Value.ToString(), Convert.ToInt32(actualValue.Value));
                        UpdatePieGadgetData();
                        UpdateBalanceGadgetData();
                    }
                    break;

                default: break;
            }
        }

        private void monthDataGrid_RowsRemoved(
            object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Update the balance column whenever rows are deleted.
            UpdateBalanceGadgetData();
            UpdatePieGadgetData();
        }

        private void monthDataGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BalanceGauge_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            
        }

        private void ExpensesPie_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
