namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponsePreRegister
    {        
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }        
        public string Type { get; set; }
        public string Title { get; set; }
        public UserNames UserNames { get; set; }
    }

    public class UserNames
    {
        public object FirstName { get; set; }
        public object SecondName { get; set; }
        public object FirstSurname { get; set; }
        public object SecondSurname { get; set; }

        /// <summary>
        /// With this field it is identified if the data were loaded of PLS or registraduria
        /// </summary>
        public bool DataLoaded { get; set; }
    }
}
