namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using System.Windows.Input;
    public interface ILandingPageViewModel
    {
        ICommand ServicesCommand { get; }
        string UserName { get; set; }
        ICommand RefreshHomeCommand { get; }
    }
}


