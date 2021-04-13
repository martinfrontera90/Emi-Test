namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Emi.Portal.Movil.Logic.Enumerations;
    using Newtonsoft.Json;
    public class Question
    {
        public string Code { get; set; }
        [JsonProperty(PropertyName = "Question")]
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public string DescriptionResponse { get; set; }
        public string LabelTrue { get; set; }
        public string LabelFalse { get; set; }
    }
}
