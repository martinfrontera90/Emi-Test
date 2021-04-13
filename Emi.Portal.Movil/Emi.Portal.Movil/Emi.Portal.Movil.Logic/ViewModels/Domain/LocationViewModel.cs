namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using GalaSoft.MvvmLight;
    public class LocationViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public double Distance { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
