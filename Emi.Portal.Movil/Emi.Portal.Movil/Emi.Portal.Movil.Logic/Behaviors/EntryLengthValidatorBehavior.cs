namespace Emi.Portal.Movil.Logic.Behaviors
{
    using Xamarin.Forms;
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty =
                    BindableProperty.Create("MaxLength", typeof(string), typeof(string), default(string));

        public string MaxLength
        {
            get { return (string)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }       

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;           
            if (entry.Text != null && entry.Text.Length > int.Parse(MaxLength))
            {                                
                entry.Text = e.OldTextValue;                
            }
        }
    }
}
