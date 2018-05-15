using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_UI
{
    public class ConvertorAddMathActions : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sumedValue = 0;

            if (double.TryParse(value.ToString(), out double num))
            {
                return num;
            }

            foreach (var item in value.ToString().Split('+'))
            {
                if (double.TryParse(item, out double itemAsDouble))
                {
                    sumedValue += itemAsDouble;
                }
                else
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            return sumedValue;
        }
    }
}
