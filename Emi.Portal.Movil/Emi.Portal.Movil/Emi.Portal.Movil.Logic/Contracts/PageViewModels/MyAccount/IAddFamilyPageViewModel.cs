namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public interface IAddFamilyPageViewModel
    {
        ICommand AddMemberCommand { get; }
        Person Member { get; set; }
        string Message { get; set; }        
        string Title { get; set; }
        void ShowResult();

    }

}
