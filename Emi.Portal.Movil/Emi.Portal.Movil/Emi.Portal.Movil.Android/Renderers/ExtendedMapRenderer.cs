using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace Emi.Portal.Movil.Droid.Renderers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Emi.Portal.Movil.Controls;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Maps.Android;
    using Xamarin.Forms.Platform.Android;

    public class ExtendedMapRenderer : MapRenderer, GoogleMap.IOnInfoWindowClickListener
    {
        GoogleMap map;
        bool isDrawn;
        List<Position> CoverageCoordinates;
        List<Logic.Models.Domain.Polygon> Coverages;

        public ExtendedMapRenderer() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            ExtendedMap element = (ExtendedMap)Element;

            if (Control != null && element != null)
            {
                CoverageCoordinates = element.CoverageCoordinates;
                Coverages = element.Coverages;
                Control.GetMapAsync(this);
            }
        }

        private void OnGoogleMapReady()
        {
            var element = (ExtendedMap)Element;
            NativeMap.SetOnInfoWindowClickListener(this);

            if (element.ItemsSource != null)
            {
                LoadPins(element);
            }

            if (element.Coverages != null)
            {
                foreach (Logic.Models.Domain.Polygon coverage in Coverages)
                {
                    int inCB = 0x66129EE0;
                    int outCB = 0x6601456B;
                    PolygonOptions polygon = new PolygonOptions();
                    if (coverage.CoverageCode == "CB")
                    {
                        polygon.InvokeFillColor(inCB);
                    }
                    else
                    {
                        polygon.InvokeFillColor(outCB);
                    }
                    polygon.InvokeStrokeColor(0x660000FF);
                    polygon.InvokeStrokeWidth(1);

                    if (coverage.Points != null)
                    {
                        bool Point = false;
                        foreach (Logic.Models.Domain.Point position in coverage.Points)
                        {
                            polygon.Add(new LatLng(position.Latitude, position.Longitude));
                            Point = true;
                        }
                        if (Point)
                        {
                            NativeMap.AddPolygon(polygon);
                        }
                    }

                }
            }

            isDrawn = true;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedMap.ItemsSourceProperty.PropertyName)
            {
                LoadPins(sender);
            }

            if (e.PropertyName == "VisibleRegion" && !isDrawn)
            {
                OnGoogleMapReady();
            }
        }

        private void LoadPins(object sender)
        {
            if (Control != null)
            {
                var formsMap = (ExtendedMap)sender;

                NativeMap.Clear();
                if (formsMap.ItemsSource != null)
                {
                    var formsPins = formsMap.ItemsSource;

                    foreach (var formsPin in formsPins)
                    {
                        var markerWithIcon = new MarkerOptions();

                        markerWithIcon.SetPosition(new LatLng(formsPin.Latitude, formsPin.Longitude));
                        markerWithIcon.SetTitle(formsPin.Name);
                        markerWithIcon.SetSnippet(formsPin.Description);
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.FromResource(GetPinIcon()));

                        NativeMap.AddMarker(markerWithIcon);
                    }
                }
            }
        }

        private int GetPinIcon()
        {
            return Resources.GetIdentifier("gps", "drawable", Forms.Context.PackageName);
        }

        public void OnInfoWindowClick(Marker marker)
        {
            var element = (ExtendedMap)Element;

            foreach (var pin in element.ItemsSource)
            {
                if (pin.Latitude == marker.Position.Latitude &&
                    pin.Longitude == marker.Position.Longitude)
                {
                    if (pin.SelectCommand != null)
                        pin.SelectCommand.Execute(null);
                }
            }
        }
    }
}