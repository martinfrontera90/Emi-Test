namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;

    public interface IFileSelectService
    {
        Task<FileRequest> AddFile(string serviceDate, string serviceNumber);
        Task<string> SavePdf(string urlPdf = "", string name = "", byte[] array = null);
        Task<FileSelected> SelectFile();
        string MimeType(string extension);
    }
}
