namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using Emi.Portal.Movil.Logic.Enumerations;
    public class PushMessage
    {
        public string AppPages { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }                               
        public string NeedLogin { get; set; }
        public string PushType { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
    }
}
