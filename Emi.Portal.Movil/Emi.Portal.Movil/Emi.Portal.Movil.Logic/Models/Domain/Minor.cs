namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class Minor
    {
        [JsonProperty(PropertyName = "NUMERORADICADO")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "FNACIMIENTO")]
        public string Birthday { get; set; }

        [JsonProperty(PropertyName = "ESTADO")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "APELLIDO_1")]
        public string LastNameOne { get; set; }

        [JsonProperty(PropertyName = "APELLIDO_2")]
        public string LastNameTwo { get; set; }

        [JsonProperty(PropertyName = "CELULAR")]
        public string CellPhone { get; set; }

        [JsonProperty(PropertyName = "CORREO")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "DOCUMENTO")]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "NOMBRE_1")]
        public string NameOne { get; set; }

        [JsonProperty(PropertyName = "NOMBRE_2")]
        public string NameTwo { get; set; }

        [JsonProperty(PropertyName = "TIPODOC")]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "VIDA")]
        public string Life { get; set; }

        public string FullName => $"{NameOne} {NameTwo} {LastNameOne} {LastNameTwo}";
    }
}
