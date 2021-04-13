namespace Emi.Portal.Movil.Controls
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Emi.Portal.Movil.Logic.Contracts.Controls;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Polygon = Logic.Models.Domain.Polygon;

    public class ExtendedMap : Map
    {
        public List<Position> CoverageCoordinates { get; set; }
        public List<Polygon> Coverages { get; set; }

        public ExtendedMap()
        {
            CoverageCoordinates = new List<Position>();
            Coverages = new List<Polygon>();
        }

        public ExtendedMap(MapSpan region) : base(region)
        {
            CoverageCoordinates = new List<Position>();
            Coverages = new List<Polygon>();
        }

        public static readonly BindableProperty ZoomDistanceProperty =
            BindableProperty.Create("ZoomDistance", typeof(double), typeof(ExtendedMap), default(double));

        public double ZoomDistance
        {
            get { return (double)GetValue(ZoomDistanceProperty); }
            set { SetValue(ZoomDistanceProperty, value); }
        }

        public static readonly BindableProperty CurrentLocationProperty =
             BindableProperty.Create("CurrentLocation", typeof(Position), typeof(ExtendedMap), default(Position), propertyChanged: OnCurrentLocationPropertyChanged);

        public Position CurrentLocation
        {
            get { return (Position)GetValue(CurrentLocationProperty); }
            set { SetValue(CurrentLocationProperty, value); }
        }

        private static void OnCurrentLocationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as ExtendedMap;

            if (newValue != null)
            {
                Distance distance;

                if (map.ZoomDistance <= 0)
                    distance = Distance.FromKilometers(8);
                else
                    distance = Distance.FromKilometers(map.ZoomDistance);

                map.MoveToRegion(MapSpan.FromCenterAndRadius((Xamarin.Forms.Maps.Position)newValue, distance));
            }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ExtendedMap, IEnumerable<ILocation>>(o => o.ItemsSource, default(IEnumerable<ILocation>),
            propertyChanged: OnItemsSourcePropertyChanged);

        public IEnumerable<ILocation> ItemsSource
        {
            get { return (IEnumerable<ILocation>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable<ILocation> oldvalue, IEnumerable<ILocation> newvalue)
        {
            var map = bindable as ExtendedMap;

            if (map != null)
                map.OnItemsSourceChanged(oldvalue, newvalue);
        }

        private void OnItemsSourceChanged(IEnumerable<ILocation> oldvalue, IEnumerable<ILocation> newvalue)
        {
            if (newvalue != null)
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    this.Pins.Clear();

                    foreach (var item in newvalue)
                    {
                        if (item.Latitude != 0 && item.Longitude != 0)
                        {

                            var pin = new Pin()
                            {
                                Address = item.Description,
                                Label = item.Name,
                                Position = new Position(item.Latitude, item.Longitude)
                            };

                            pin.Clicked += (s, e) =>
                            {
                                if (item.SelectCommand != null)
                                {
                                    item.SelectCommand.Execute(null);
                                }
                            };

                            this.Pins.Add(pin);
                        }
                    }
                }

                var newValueCollectionChanged = newvalue as INotifyCollectionChanged;

                if (newValueCollectionChanged != null)
                {
                    newValueCollectionChanged.CollectionChanged += newValueCollectionChanged_CollectionChanged;
                }
            }
        }

        void newValueCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = ((IEnumerable<ILocation>)sender).ToList();

            if (collection != null)
            {
                this.ItemsSource = collection;
            }
        }
    }
}
