namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    public interface IPhoneService
    {        
        int ApplicationIconBadgeNumber { get; set; }
        void Call(string phoneNumber);
        bool CanOpenUrl(string url);
        void OpenUrl(string url);
        string GetIpAddress();
        Task RunOnUIThread(Action action);
        bool IsiOS { get; }
        string DeviceId { get; }
        CultureInfo CurrentCulture { get; }
        Task<string> SaveFiles(string filename, byte[] bytes);
        void DeleteCookie();
    }
}
