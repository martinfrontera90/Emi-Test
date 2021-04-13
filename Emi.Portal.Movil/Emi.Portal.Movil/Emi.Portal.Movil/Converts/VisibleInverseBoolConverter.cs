namespace Emi.Portal.Movil.Converts
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;

	public class VisibleInverseBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
	}
}
