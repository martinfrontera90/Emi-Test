using System;
namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponsePQRSCreated
    {
        public string SettledNumber { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public bool Success { get; set; }
    }
}
