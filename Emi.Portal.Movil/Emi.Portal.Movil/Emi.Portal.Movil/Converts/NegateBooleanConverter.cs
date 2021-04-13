namespace Emi.Portal.Movil.Converters
{
    using System;
    using Xamarin.Forms;

    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return !(bool)value;
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return !(bool)value;
            }
            return false;
        }
    }
}
