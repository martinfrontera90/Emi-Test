namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using GalaSoft.MvvmLight;

    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        public ResponseLogin User { get; set; }
    }
}
