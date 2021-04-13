using System;
namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class RealTimeConfigurationResponse : ResponseBase
    {
        public string ApiKey { get; set; }
        public string AuthDomain { get; set; }
        public string DatabaseURL { get; set; }
        public string ProjectId { get; set; }
        public string StorageBucket { get; set; }
        public string MessagingSenderId { get; set; }
        public string AppId { get; set; }
    }
}
