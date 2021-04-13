namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseBase
    {
        public int StatusCode { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}