namespace Emi.Portal.Movil.Logic.Contracts.Controls
{
    using System.Windows.Input;
    public interface ILocation
    {
        string Name { get; set; }
        string Description { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        string Icon { get; set; }

        ICommand SelectCommand { get; }
    }
}
