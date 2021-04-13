namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    public interface INotificationsSettingsViewModel
    {
        void RegisterNotifications();
        void AddTag(string tag);
        void RemoveTag(string tag);
    }
}