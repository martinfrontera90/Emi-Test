namespace Emi.Portal.Movil.Helpers
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Emi.Portal.Movil.Views;
    using Xamarin.Forms;

    public class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MessageChatViewModel;
            if (messageVm == null)
                return null;


            return (messageVm.User.Equals("User")) ? incomingDataTemplate : outgoingDataTemplate;
        }
    }
}
