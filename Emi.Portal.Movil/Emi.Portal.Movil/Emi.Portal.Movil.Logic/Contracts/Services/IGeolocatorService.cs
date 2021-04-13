namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    using Plugin.Geolocator.Abstractions;

    public interface IGeolocatorService
    {
        bool IsGeolocationAvailable { get; }
        bool IsGeolocationEnabled { get; }
        Task<Position> GetPositionAsync();
    }
}
