namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface ICallViewModel
    {
        void CallCategory();
        ICommand CallCommand { get; }
        ICommand CallCategoryCommand { get; }
        Task Init();
    }
}
