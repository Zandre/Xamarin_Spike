using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using SuperFluity.Office_Space_Classes;
using Android.Gms.Maps.Model;

namespace SuperFluity.Droid
{
    [Activity(Label = "ListOfCompanyLocations")]
    public class MapOfCompanyLocations : Activity, IOnMapReadyCallback
    {
        GoogleMap map;
        Dictionary<string, CompanyLocation> locationDictionary = new Dictionary<string, CompanyLocation>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapOfCompanyLocations);
            ActionBar.Title = CurrentSession.SelectedCompany.Title + " Locations";
            SetUpMap();
        }

        #region Google Maps
        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MarkerClick += Map_MarkerClick;
            map.MapClick += Map_MapClick;

            bool zoomtoFirstMarker = false;

            foreach(CompanyLocation location in CurrentSession.SelectedCompany.CompanyLocations)
            {
                MarkerOptions markerOptions = new MarkerOptions()
                    .SetPosition(new LatLng(location.Latitude, location.Longitude))
                    .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.icon25_25))
                    .SetTitle(location.Title);


                Marker marker = map.AddMarker(markerOptions);
                locationDictionary.Add(marker.Id, location);                                 

                if(!zoomtoFirstMarker)
                {
                    CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(new LatLng(location.Latitude, location.Longitude), 10);
                    map.MoveCamera(camera);
                    zoomtoFirstMarker = true;
                }
            }
        }

        private void Map_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            CurrentSession.SelectedCompanyLocation = null;
            
            Intent intent = new Intent();
            intent = new Intent(this, typeof(CompanyLocationDetail));
            intent.PutExtra("Latitude", e.Point.Latitude.ToString());
            intent.PutExtra("Longitude", e.Point.Longitude.ToString());
            StartActivity(intent);
        }

        private void Map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            CompanyLocation selectedLocation = locationDictionary[e.Marker.Id];
            CurrentSession.SelectedCompanyLocation = selectedLocation;
            ShowPopupMenu();
        }

        private void SetUpMap()
        {
            if (map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.listofcompanylocations_fragment).GetMapAsync(this);
            }
        }
        #endregion

        #region Popup Menu
        private Dialog dialog;
        private void ShowPopupMenu()
        {
            dialog = new Dialog(this);
            dialog.RequestWindowFeature((int)WindowFeatures.ContextMenu);
            dialog.SetTitle(CurrentSession.SelectedCompanyLocation.Title);
            dialog.SetContentView(Resource.Layout.maplisting_menu);

            Button btnViewOfficeSpace = dialog.FindViewById<Button>(Resource.Id.btn_viewofficespace);
            btnViewOfficeSpace.Click += BtnViewOfficeSpace_Click;
            Button btnDeleteLocation = dialog.FindViewById<Button>(Resource.Id.btn_deletelocation);
            btnDeleteLocation.Click += BtnDeleteLocation_Click;

            dialog.Show();
        }

        private void BtnViewOfficeSpace_Click(object sender, EventArgs e)
        {
            if (dialog != null && dialog.IsShowing)
            {
                dialog.Cancel();
            }
            Intent intent = new Intent(this, typeof(OfficeSpaces));
            intent.PutExtra("accessFunction", 0);
            StartActivity(intent);            
        }

        private void BtnDeleteLocation_Click(object sender, EventArgs e)
        {
            if (dialog != null && dialog.IsShowing)
            {
                dialog.Cancel();
            }
            //delete from db goes here
        }
        #endregion

    }
}