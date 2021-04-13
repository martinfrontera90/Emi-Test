namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class Point
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public int Position { get; set; }

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
