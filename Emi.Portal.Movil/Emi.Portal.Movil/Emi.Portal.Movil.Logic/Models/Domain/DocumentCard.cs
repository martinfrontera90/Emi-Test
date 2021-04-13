namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class DocumentCard
    {
        [JsonProperty("Documento")]
        public string Document { get; set; }

        [JsonProperty("TipoDocumento")]
        public string DocumentType { get; set; }

        [JsonProperty("ClienteFinanciero")]
        public string ClienteFinanciero { get; set; }

        [JsonProperty("TiendaCliente")]
        public string TiendaCliente { get; set; }
    }
}
