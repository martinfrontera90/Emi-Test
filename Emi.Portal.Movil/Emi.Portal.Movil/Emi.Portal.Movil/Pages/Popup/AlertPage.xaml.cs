namespace Emi.Portal.Movil.Pages.Popup
{
    using System;
    using AiForms.Dialogs.Abstractions;

    public partial class AlertPage : DialogView
	{
        public AlertPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DialogNotifier.Complete();
        }
    }
}
