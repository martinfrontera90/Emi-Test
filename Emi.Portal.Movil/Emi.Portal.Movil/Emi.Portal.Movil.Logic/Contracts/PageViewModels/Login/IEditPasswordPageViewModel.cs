namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Login
{
    using System;
    using System.Windows.Input;

    public interface IEditPasswordPageViewModel
    {
        ICommand ChangePasswordCommand { get; }
        void CleanData();
        string Email { get; set; }
        string Code { get; set; }
    }
}
