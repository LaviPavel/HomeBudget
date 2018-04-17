using System.Data;
using BackEnd;


namespace WPF_UI
{
    public partial class Again
    {
        public static ConnectingLayer ConnectingLayerObject = new ConnectingLayer();

        public Again()
        {
            InitializeComponent();
            InitMonthDataGrid();
        }

        public void InitMonthDataGrid()
        {
            ConnectingLayerObject.InitDatatable();
            DataGrid.DataContext = ConnectingLayerObject.MonthlyExpensesDataTable.DefaultView;
        }





    }
}
