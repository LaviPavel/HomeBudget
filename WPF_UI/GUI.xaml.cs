using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for GUI.xaml
    /// </summary>
    public partial class Gui : INotifyPropertyChanged
    {

        public static readonly DependencyProperty PavelProperty = DependencyProperty.Register(
            "Pavel", 
            typeof(int), 
            typeof(Gui), 
            new PropertyMetadata(0));

        public int Pavel
        {
            get { return (int)this.GetValue(PavelProperty);}
            set
            {
                this.SetValue(PavelProperty, value);
                this.OnPropertyChanged("Pavel");
            }
        }



        //private DateTime _userDateSelection;
        //public event PropertyChangedEventHandler PropertyChanged;
        
        //public DateTime UserDateSelection
        //{
        //    get { return _userDateSelection; }
        //    set
        //    {
        //        _userDateSelection = value;
        //        OnPropertyChanged();
        //    }
        //}


        public Gui()
        {
            InitializeComponent();
            //InitMonthDataGrid();


            this.PropertyChanged += GUI_PropertyChanged;



        }

        private void GUI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //todo....
        }

        //public void InitMonthDataGrid()
        //{
        //    DataTable dtable = new DataTable();
        //    dtable.Columns.Add(new DataColumn("Category"));
        //    dtable.Columns.Add(new DataColumn("SubCategory"));
        //    dtable.Columns.Add(new DataColumn("Expected Amount"));
        //    dtable.Columns.Add(new DataColumn("Actual Amount"));
        //    dtable.Columns.Add(new DataColumn("Description"));


        //    dtable.Rows.Add("bla", "bla2", "5", "20", "none");
        //    dtable.AcceptChanges();

        //    dataGrid.DataContext = dtable.DefaultView;

        //}

        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}







        //private int _boundNumber;
        //public int BoundNumber
        //{
        //    get { return _boundNumber; }
        //    set
        //    {
        //       _boundNumber = value;
        //        OnPropertyChanged();
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PavelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int input = 0;
            if (int.TryParse(value.ToString(), out input))
            {
                return "My value: " + input.ToString();
            }
            else
            {
                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class YearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            return ((DateTime) value).Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            return ((DateTime)value).Month;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
