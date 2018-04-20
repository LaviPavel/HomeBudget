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
            DataGrid.CellEditEnding += DataGrid_CellEditEnding;
        }

        public void InitMonthDataGrid()
        {
            ConnectingLayerObject.InitDatatable();
            DataGrid.DataContext = ConnectingLayerObject.MonthlyExpensesDataTable.DefaultView;
        }
        private void DataGrid_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            ConnectingLayerObject.HandleDataTableUpdate(DataGrid.Items.IndexOf(DataGrid.CurrentItem),
                e.Column.DisplayIndex);
        }

    }
}
