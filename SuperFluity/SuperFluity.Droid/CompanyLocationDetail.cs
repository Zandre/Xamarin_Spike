using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Text.RegularExpressions;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using Android.Runtime;
using static Android.App.TimePickerDialog;
using System.Threading.Tasks;
using Java.Lang;
using Android.Content;
using SuperFluity.Office_Space_Classes;
using SuperFluity.Droid.Fragments;

namespace SuperFluity.Droid
{
    [Activity(Label = "CompanyLocationDetail", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class CompanyLocationDetail : Activity
    {
        Fragment_CompanyLocationDetail _companyDetails;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CompanyLocationDetails);
            ActionBar.Title = "Location Details";

            string lat = Intent.GetStringExtra("Latitude");
            string lon = Intent.GetStringExtra("Longitude");

            _companyDetails = new Fragment_CompanyLocationDetail();
            Bundle bundle = new Bundle();
            bundle.PutString("Latitude", lat);
            bundle.PutString("Longitude", lon);
            _companyDetails.Arguments = bundle;
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.locationdetail_fragmentframe, _companyDetails);
            ft.Commit();
        }

        //public override void OnBackPressed()
        //{
        //    Intent intent = new Intent(this, typeof(MainActivity));
        //    StartActivity(intent);
        //}        
    }
}