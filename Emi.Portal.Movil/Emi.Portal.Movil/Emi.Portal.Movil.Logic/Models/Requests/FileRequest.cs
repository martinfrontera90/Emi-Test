namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class FileRequest : Request
    {
        public string ServiceMiddlewareId { get; set; }
        public string ServiceDate { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string FileContentType { get; set; }

        public FileRequest()
        {
            Action = AppConfigurations.FileAction;
            Controller = AppConfigurations.VideoCallController;
        }
    }
}
