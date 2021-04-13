namespace Emi.Portal.Movil.Controls
{
    using Xamarin.Forms;


    public class ExtendedButton : Button
    {

        public static readonly BindableProperty VerticalContentAlignmentProperty = BindableProperty.Create<ExtendedButton, TextAlignment>(p => p.VerticalContentAlignment, TextAlignment.Center);

        public static readonly BindableProperty HorizontalContentAlignmentProperty = BindableProperty.Create<ExtendedButton, TextAlignment>(p => p.HorizontalContentAlignment, TextAlignment.Center);

        public TextAlignment VerticalContentAlignment
        {
            get { return (TextAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { this.SetValue(VerticalContentAlignmentProperty, value); }
        }
        public TextAlignment HorizontalContentAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { this.SetValue(HorizontalContentAlignmentProperty, value); }
        }
    }
}
