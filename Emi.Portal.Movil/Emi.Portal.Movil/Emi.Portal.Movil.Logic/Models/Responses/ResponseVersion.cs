namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;

    public class ResponseVersion : ResponseBase
    {
        public int DevicePlatform { get; set; }
        public string Version { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PlatformDescription { get; set; }
    }
}
