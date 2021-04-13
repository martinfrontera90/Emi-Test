namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;

    public class AuthorizeChangePassword
    {
        public string Code { get; set; }

        public string Profile { get; set; }

        public string UserName { get; set; }
    }
}
