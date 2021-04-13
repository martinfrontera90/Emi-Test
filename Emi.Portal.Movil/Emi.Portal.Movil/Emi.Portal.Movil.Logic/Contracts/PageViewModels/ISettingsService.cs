namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    public interface ISettingsService
    {
        void AddOrUpdateStorageValue(string key, string value);
        void Delete(string key);
        string GetResource(string key);
        string GetSetting(string key);
        string GetStorageValue(string key);
    }
}
