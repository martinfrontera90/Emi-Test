namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Controls
{
    public interface IPdfPageViewModel
    {
        void OpenPdf(string urlPdf = "", string name = "", byte[] array = null, bool share = false, string title = "");
    }
}
