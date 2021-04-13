namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
	using Newtonsoft.Json;

	public class RequestCities : Request
    {
		[JsonProperty(PropertyName = "Value")]
		public string DepartamentCode { get; set; }

        public RequestCities()
        {
            Action = "GetCities";
            Controller = AppConfigurations.DataListsController;
        }
    }
}
