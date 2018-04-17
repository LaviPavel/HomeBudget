using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;

using System.Runtime.CompilerServices;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Func<ChartPoint, string> PointLabel { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public MainWindow()
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            InitializeComponent();
            Bla();
            BalanceDemo();

            DataContext = this;
        }

        public void BalanceDemo()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<ObservableValue> {new ObservableValue(12)},
                    DataLabels = true
                }
            };
            Labels = new[] {"Balance"};
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {

        }
        public void Bla()
        {
            try
            {
                //File.Copy(Environment.CurrentDirectory + @"\x64\SQLite.Interop.dll",
                    //Environment.CurrentDirectory + @"\SQLite.Interop.dll");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            SQLiteConnection db = new SQLiteConnection(@"Data Source=C:\pavel\sqlite\Demo.db;FailIfMissing=True;");
            db.Open();
            using (SQLiteCommand comm = db.CreateCommand())
            {
                comm.CommandText = "select * from vw_ExpensesUI";
                comm.ExecuteNonQuery();

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(comm);
                DataTable dt = new DataTable("vw_ExpensesUI");
                dataAdapter.Fill(dt);
                DataGrid.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);



            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PieChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
