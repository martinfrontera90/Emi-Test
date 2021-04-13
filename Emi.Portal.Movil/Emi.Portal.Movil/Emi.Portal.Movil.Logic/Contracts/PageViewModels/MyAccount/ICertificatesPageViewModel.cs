namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface ICertificatesPageViewModel
    {
        Task LoadData();
        CertificateCardViewModel CardSelected { get; set; }
        string TitlePage { get; set; }
    }
}
