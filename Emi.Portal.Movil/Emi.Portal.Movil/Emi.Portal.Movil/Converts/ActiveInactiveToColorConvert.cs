namespace Emi.Portal.Movil.Converts
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;
    public class ActiveInactiveToColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return (Color)App.Current.Resources["ActivePerson"];

            return (Color)App.Current.Resources["InactivePerson"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
