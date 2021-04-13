namespace Emi.Portal.Movil.Converts
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;
    public class EnablePickerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
