namespace Emi.Portal.Movil.Services
{
    using System;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Microsoft.AppCenter.Crashes;

    public class ExceptionService : IExceptionService
    {
        public void RegisterException(Exception ex) => Crashes.TrackError(ex);
    }
}
