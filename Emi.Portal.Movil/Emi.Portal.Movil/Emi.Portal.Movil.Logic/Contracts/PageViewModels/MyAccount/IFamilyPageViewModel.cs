namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface IFamilyPageViewModel
    {
        ICommand AddMemberCommand { get; }
        ICommand InformationCommand { get; }
        Task LoadFamily();
        PersonViewModel MemberSelected { get; set; }        
        ICommand RefreshFamilyCommand { get; }
        string TitlePage { get; set; }
    }
}
