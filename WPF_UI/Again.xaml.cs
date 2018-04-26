using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using BackEnd;

namespace WPF_UI
{
    public partial class Again : INotifyPropertyChanged
    {
        private DataTable _uiDataTable;
        public static ConnectingLayer ConnectingLayerObject = new ConnectingLayer();
        public event PropertyChangedEventHandler PropertyChanged;
        public DataTable UiDataTable
        {
            get { return _uiDataTable; }
            set
            {
                if (_uiDataTable != value)
                {
                    _uiDataTable = value;
                    OnPropertyChanged();
                    UiDataTable.AcceptChanges();
                }
            }
        }


        public Again()
        {
            DataContext = this;
            InitializeComponent();
            InitMonthDataGrid();
            DataGrid.CellEditEnding += DataGrid_CellEditEnding;
        }

        public void InitMonthDataGrid()
        {
            ConnectingLayerObject.InitDatatable();
            UiDataTable = ConnectingLayerObject.MonthlyExpensesDataTable;
            UiDataTable.AcceptChanges();
        }

        private void DataGrid_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            UiDataTable.AcceptChanges();
            ConnectingLayerObject.MonthlyExpensesDataTable = UiDataTable;
            ConnectingLayerObject.MonthlyExpensesDataTable.AcceptChanges();
            ConnectingLayerObject.HandleDataTableUpdate(DataGrid.Items.IndexOf(DataGrid.CurrentItem),
                e.Column.DisplayIndex);
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
