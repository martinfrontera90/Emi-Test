namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System;

    public interface IRegisterMinorPageViewModel
    {
        string TitlePage { get; set; }

        void LoadData();
    }
}
