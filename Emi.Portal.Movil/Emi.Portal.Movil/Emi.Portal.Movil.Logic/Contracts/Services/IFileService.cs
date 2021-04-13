namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    public interface IFileService
    {
        Task DeleteFiles();
        bool ExistRecentCache(string fileName, int cacheTime);
        Task<bool> FileExists(string fileName);
        Task<T> LoadAsync<T>(string filename);
        Task SaveAsync<T>(string fileName, T content);
        void Delete(string fileName);
    }
}
