namespace Emi.Portal.Movil.Services
{
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Resources;
    using Plugin.Settings;

    class SettingsService : ISettingsService
    {
        public void AddOrUpdateStorageValue(string key, string value)
        {
            CrossSettings.Current.AddOrUpdateValue(key, value);
        }

        public void Delete(string key)
        {
            CrossSettings.Current.Remove(key);
        }

        public string GetResource(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }

        public string GetSetting(string key)
        {
            return AppConfigurations.ResourceManager.GetString(key);
        }

        public string GetStorageValue(string key)
        {
            return CrossSettings.Current.GetValueOrDefault(key, key);
        }
    }
}
