using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    using System;
    using System.Collections.Generic;
    using CoreGraphics;
    using CoreLocation;
    using Emi.Portal.Movil.Controls;
    using Emi.Portal.Movil.iOS.Controls;
    using Emi.Portal.Movil.Logic.Contracts.Controls;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using MapKit;
    using Microsoft.AppCenter.Analytics;
    using ObjCRuntime;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps.iOS;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedMapRenderer : MapRenderer
    {
        IEnumerable<ILocation> customPins;
        MKPolygonRenderer polygonRenderer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null)
                {
                    var nativeMap = Control as MKMapView;
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    polygonRenderer = null;
                }

                if (e.NewElement != null)
                {
                    var formsMap = (ExtendedMap)e.NewElement;
                    var nativeMap = Control as MKMapView;
                    customPins = formsMap.ItemsSource;

                    nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                    nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;

                    if (formsMap.Coverages != null)
                    {
                        List<IMKOverlay> overlayList = new List<IMKOverlay>();
                        nativeMap.OverlayRenderer = GetOverlayRenderer;
                        foreach (Polygon poligon in formsMap.Coverages)
                        {
                            if (poligon != null && poligon.Points != null && poligon.Points.Count > 0)
                            {
                                
                                CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[poligon.Points.Count];
                                int index = 0;
                                foreach (var point in poligon.Points)
                                {
                                    coords[index] = new CLLocationCoordinate2D(point.Latitude, point.Longitude);
                                    index++;
                                }
                                MKPolygon blockOverlay = MKPolygon.FromCoordinates(coords);
                                blockOverlay.Title = poligon.CoverageCode;
                                overlayList.Add(blockOverlay);
                            }
                        }
                        IMKOverlay[] imko = overlayList.ToArray();
                        nativeMap.AddOverlays(imko);
                    }
                }
            }
            catch (Exception ex)
            {
                Dictionary<string, string> properties = new Dictionary<string, string>();
                properties.Add("Message", ex.Message);
                properties.Add("Source", ex.Source);
                properties.Add("StackTrace", ex.StackTrace);
                Analytics.TrackEvent("OnElementChanged", properties);
            }
        }
        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (!Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                polygonRenderer = new MKPolygonRenderer(overlay as MKPolygon)
                { 
                    FillColor = overlay.GetTitle() == "CB" ? UIColor.FromRGB(18, 158, 224) : UIColor.FromRGB(1, 69, 107),
                    StrokeColor = UIColor.Gray,
                    Alpha = 0.4f,
                    LineWidth = 1
                };
            }
            return polygonRenderer;
        }
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            var userLocationAnnotation = ObjCRuntime.Runtime.GetNSObject(annotation.Handle) as MKUserLocation;
            if (userLocationAnnotation != null)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);

            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView();
                annotationView.Annotation = annotation;

                if (customPin != null && !string.IsNullOrEmpty(customPin.Icon))
                    annotationView.Image = UIImage.FromFile(customPin.Icon);

                annotationView.CalloutOffset = new CGPoint(0, 0);
                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;

            var element = (ExtendedMap)Element;

            foreach (var pin in element.ItemsSource)
            {
                if (pin.Latitude == customView.Annotation.Coordinate.Latitude &&
                    pin.Longitude == customView.Annotation.Coordinate.Longitude)
                {
                    if (pin.SelectCommand != null)
                        pin.SelectCommand.Execute(null);
                }
            }
        }

        ILocation GetCustomPin(MKPointAnnotation annotation)
        {
            var element = (ExtendedMap)Element;

            if (element.ItemsSource != null && annotation != null)
            {
                foreach (var pin in element.ItemsSource)
                {
                    if (pin.Latitude == annotation.Coordinate.Latitude &&
                        pin.Longitude == annotation.Coordinate.Longitude)
                    {
                        return pin;
                    }
                }
            }

            return null;
        }
    }
}