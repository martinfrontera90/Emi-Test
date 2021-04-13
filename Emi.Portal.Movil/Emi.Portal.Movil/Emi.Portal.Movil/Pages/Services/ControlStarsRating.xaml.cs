namespace Emi.Portal.Movil.Pages.Services
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlStarsRating : ContentView
    {
        public ControlStarsRating()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The group name property.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create
        (
           nameof(GroupName),
           typeof(string),
           typeof(ControlStarsRating), string.Empty,
           propertyChanged: CustomGroupNamePropertyChanged
        );

        /// <summary>
        /// Customs the group name property changed.
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void CustomGroupNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ControlStarsRating)bindable;
            string groupname = newValue.ToString();

            control.starOne.GroupName = groupname;
            control.starTwo.GroupName = groupname;
            control.starThree.GroupName = groupname;
            control.starFour.GroupName = groupname;
            control.starFive.GroupName = groupname;
        }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            set { SetValue(GroupNameProperty, value); }
            get { return (string)GetValue(GroupNameProperty); }
        }

        /// <summary>
        /// The group rating control property
        /// </summary>
        public static readonly BindableProperty RatingControlProperty = BindableProperty.Create
        (
           nameof(RatingControl),
           typeof(string),
           typeof(ControlStarsRating), string.Empty,
           defaultBindingMode: BindingMode.TwoWay
        );

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string RatingControl
        {
            set { SetValue(RatingControlProperty, value); }
            get { return (string)GetValue(RatingControlProperty); }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the LabelRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void LabelRating_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RatingControl = ((Label)sender).Text;
        }
    }
}
