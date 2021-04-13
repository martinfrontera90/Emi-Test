using System;
namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService
{
    public interface IPQRSPageViewModel
    {
        void LoadData();
        void CleanFirstForm();
        string TitlePage { get; set; }
    }
}
