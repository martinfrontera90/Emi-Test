namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Newtonsoft.Json;

    public class NotificationsSettingsViewModel : INotificationsSettingsViewModel
    {
        INotificationService notificationsService;
        ISettingsService settingsService;

        public void AddTag(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                string serializedTags = settingsService.GetStorageValue("Tags");

                var savedTags = string.IsNullOrEmpty(serializedTags) ? new List<string>() :
                    JsonConvert.DeserializeObject<List<string>>(serializedTags);

                if (!savedTags.Contains(tag))
                    savedTags.Add(tag);

                settingsService.AddOrUpdateStorageValue("Tags", JsonConvert.SerializeObject(savedTags));
            }
        }

        public void RegisterNotifications()
        {
            notificationsService.RegisterNotifications();
        }

        public void RemoveTag(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                string serializedTags = settingsService.GetStorageValue("Tags");

                var savedTags = string.IsNullOrEmpty(serializedTags) ? new List<string>() :
                    JsonConvert.DeserializeObject<List<string>>(serializedTags);

                if (savedTags.Contains(tag))
                    savedTags.Remove(tag);

                settingsService.AddOrUpdateStorageValue("Tags", JsonConvert.SerializeObject(savedTags));
            }
        }

        public NotificationsSettingsViewModel(INotificationService notificationService , ISettingsService settingsService)
        {
            this.notificationsService = notificationService;
            this.settingsService = settingsService;

        }
    }
}
