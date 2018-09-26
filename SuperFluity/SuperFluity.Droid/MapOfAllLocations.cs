using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid
{
    [Activity(Label = "MapOfAllLocations")]
    public class MapOfAllLocations : Activity, IOnMapReadyCallback
    {
        GoogleMap map;
        Dictionary<string, CompanyLocation> locationDictionary = new Dictionary<string, CompanyLocation>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapOfCompanyLocations);
            ActionBar.Title = "Available Locations";
            SetUpMap();
        }

        private void SetUpMap()
        {
            if (map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.listofcompanylocations_fragment).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MarkerClick += Map_MarkerClick;

            bool zoomtoFirstMarker = false;

            foreach (SystemUser systemUser in CurrentSession.ValidUsers)
            {
                foreach (Company company in systemUser.UserCompanies)
                {
                    foreach (CompanyLocation location in company.CompanyLocations)
                    {
                        MarkerOptions markerOptions = new MarkerOptions()
                            .SetPosition(new LatLng(location.Latitude, location.Longitude))
                            .SetTitle(location.Title)
                            .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.icon25_25));



                        Marker marker = map.AddMarker(markerOptions);
                        locationDictionary.Add(marker.Id, location);

                        if (!zoomtoFirstMarker)
                        {
                            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(new LatLng(location.Latitude, location.Longitude), 10);
                            map.MoveCamera(camera);
                            zoomtoFirstMarker = true;
                        }
                    }
                }
            }
        }

        private Dialog dialog;
        private void Map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            CompanyLocation selectedLocation = locationDictionary[e.Marker.Id];
            CurrentSession.SelectedCompanyLocation = selectedLocation;

            dialog = new Dialog(this);
            dialog.RequestWindowFeature((int)WindowFeatures.ContextMenu);
            dialog.SetTitle(selectedLocation.Title);
            dialog.SetContentView(Resource.Layout.locationSelectedForBooking);

            Button btnViewOffices = dialog.FindViewById<Button>(Resource.Id.btn_viewOfficesForBooking);
            btnViewOffices.Click += BtnViewOffices_Click;
            TextView tvNumberOfOffices = dialog.FindViewById<TextView>(Resource.Id.tvCompanyLocation_NumberOfOffices);
            tvNumberOfOffices.Text = selectedLocation.OfficeSpaces.Count + " office(s) available";
            RatingBar rbLocationRating = dialog.FindViewById<RatingBar>(Resource.Id.rbCompanyLocationRating);
            float rating = Convert.ToSingle(selectedLocation.Rating);
            rbLocationRating.Rating = rating;

            dialog.Show();
        }

        private void BtnViewOffices_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OfficeSpaces));
            intent.PutExtra("accessFunction", 1);
            StartActivity(intent);
        }
    }
}