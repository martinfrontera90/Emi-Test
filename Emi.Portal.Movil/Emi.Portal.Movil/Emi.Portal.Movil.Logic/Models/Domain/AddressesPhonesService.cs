namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    public class AddressesPhonesService
    {
        public List<Address> Addresses { get; set; }
        public ContactPhoneService ContactPhoneService { get; set; }
        public List<Country> Countries { get; set; }
    }
}
