namespace Emi.Portal.Movil.Converts
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class EntryInactiveColorTextConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (Color)App.Current.Resources["Color7"];
            }
            return (Color)App.Current.Resources["Color8"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
