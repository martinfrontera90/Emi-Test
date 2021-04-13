namespace Emi.Portal.Movil.Converters
{
    using System;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Xamarin.Forms;

    public class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rating = (int)value;

            return (rating != 0) ? Enum.GetName(typeof(StartRatingEnum), rating) : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}