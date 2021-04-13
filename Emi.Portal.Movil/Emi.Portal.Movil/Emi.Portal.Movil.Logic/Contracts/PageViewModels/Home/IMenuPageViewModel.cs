namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Home
{
    using System.Windows.Input;
    public interface IMenuPageViewModel
    {
        string UserName { get; set; }
        string Email { get; set; }
		string Version { get; set; }
        void LoadMenu();
    }
}
