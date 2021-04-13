namespace Emi.Portal.Movil.Pages.Services
{
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public partial class AdvanceLocationPage : ContentPage
    {
        //Lat : 6.29326 | Long : -75.57411

        double Lat = 6.29326;
        double Long = -75.57411;

        public AdvanceLocationPage()
        {
            InitializeComponent();

            map.MyLocationEnabled = true;

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Lat, Long), Distance.FromMeters(2)));
            map.Pins.Clear();

            map.Pins.Add(
                new Pin
                {
                    Type = PinType.SearchResult,
                    Label = "Casa",
                    Address = "Cra &1a #97 05",
                    Position = new Position(Lat, Long),
                    Tag = "id_new_york",
                    Icon = BitmapDescriptorFactory.FromBundle("gps.png"),
                    IsDraggable = true
                });

            map.PinDragStart += Map_PinDragStart;
            map.PinDragging += Map_PinDragging;
            map.PinDragEnd += Map_PinDragEnd;
        }

        void Map_PinDragStart(object sender, PinDragEventArgs e)
        {

        }

        void Map_PinDragging(object sender, PinDragEventArgs e)
        {

        }

        void Map_PinDragEnd(object sender, PinDragEventArgs e)
        {

        }

    }
}
