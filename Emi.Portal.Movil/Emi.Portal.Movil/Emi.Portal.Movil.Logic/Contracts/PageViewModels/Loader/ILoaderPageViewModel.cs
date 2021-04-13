namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Loader
{
    using System.Threading.Tasks;

    public interface ILoaderPageViewModel
    {
        Task Start(string id, string message, string code, string url);
    }
}
