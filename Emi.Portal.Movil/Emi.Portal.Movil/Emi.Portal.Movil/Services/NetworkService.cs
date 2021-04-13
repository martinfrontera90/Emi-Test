namespace Emi.Portal.Movil.Services
{
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Plugin.Connectivity;

    public class NetworkService : INetworkService
    {
        public bool IsNetworkAvailable
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
    }
}
