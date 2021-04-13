namespace Emi.Portal.Movil.Pages.Popup
{
    using System;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms;

    public partial class ContingencyMessagePage : PopupPage
    {
        public ContingencyMessagePage()
        {
            InitializeComponent();
            ChangeItem();
        }

        private void ChangeItem()
        {
            IContingencyMessagePageViewModel contingencyMessage = ServiceLocator.Current.GetInstance<IContingencyMessagePageViewModel>();
            var cant = contingencyMessage.Messages.Count;

            Device.StartTimer(TimeSpan.FromSeconds(10), (Func<bool>)(() =>
            {
                Caru.Position = (Caru.Position + 1) % cant;
                return true;
            }));
        }
    }
}
