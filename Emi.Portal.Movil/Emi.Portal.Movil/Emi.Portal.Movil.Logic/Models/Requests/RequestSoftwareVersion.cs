namespace Emi.Portal.Movil.Logic.Models.Requests
{
	using Emi.Portal.Movil.Logic.Contracts.Services;
    using CommonServiceLocator;

	public class RequestSoftwareVersion : Request
    {
		public string Code{ get; set; }

		public RequestSoftwareVersion()
        {
			IPhoneService phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();
			Action = "GetSoftwareVersion";
			Code = phoneService.IsiOS ? "Version App IOs" : "Version App Android";
			Controller = "Contents";
		}
    }
}
