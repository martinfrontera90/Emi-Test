﻿namespace Emi.Portal.Movil.Converts
{
    using System;
    using System.Globalization;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public class CoordinationPendingToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;
            if (count > 0)
            {
                return AppResources.TitleConfirmedCoordination;
            }
            return AppResources.TitleNoConfirmedCoordination;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
