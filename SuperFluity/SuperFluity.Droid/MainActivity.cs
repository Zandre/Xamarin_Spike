using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Graphics.Drawables;
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid
{
    [Activity(Label = "SuperFluity", MainLauncher = true, Icon = "@drawable/icon150_150")]
    public class MainActivity : Activity
    {
        enum PossibleActivities {   ReturnHere = 0,
                                    Listing = 1,
                                    UserBookings = 2,
                                    MapOffAllLocations = 3
                                };

        Button btnNewBooking;
        Button btnListing;
        Button btnUserBookings;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            this.ActionBar.Title = "SuperFluity";

            btnListing = FindViewById<Button>(Resource.Id.btnListing);
            btnListing.Tag = (int)PossibleActivities.Listing;
            btnListing.Click += Login;

            btnUserBookings = FindViewById<Button>(Resource.Id.btnMyBookings);
            btnUserBookings.Tag = (int)PossibleActivities.UserBookings;
            btnUserBookings.Click += Login;

            btnNewBooking = FindViewById<Button>(Resource.Id.btnNewBooking);
            btnNewBooking.Click += BtnNewBooking_Click;           
        }

        #region Options Menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_mainactivity, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.mainactivity_login:
                    StartActivityForResult(new Intent(this, typeof(LoginActivity)), (int)PossibleActivities.ReturnHere);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        #endregion

        private void Login(object sender, EventArgs e)
        { 
            if(!CurrentSession.UserIsLoggedOn())
            {
                StartActivityForResult(new Intent(this, typeof(LoginActivity)), Convert.ToInt32(((Button)sender).Tag));
            } 
            else
            {
                GoToActivity((PossibleActivities)Convert.ToInt32(((Button)sender).Tag));
            }           
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (resultCode)
            {
                case Result.Canceled:
                    break;
                case Result.Ok:
                    PossibleActivities activity = (PossibleActivities)requestCode;
                    GoToActivity(activity);
                    break;
            }
        }

        private void GoToActivity(PossibleActivities possibleActivity)
        {
            Intent intent = new Intent();
            switch (possibleActivity)
            {
                case PossibleActivities.Listing:
                    intent = new Intent(this, typeof(Listings));
                    break;
                case PossibleActivities.UserBookings:
                    intent = new Intent(this, typeof(ListOfUserBookings));
                    break;
                case PossibleActivities.MapOffAllLocations:
                    intent = new Intent(this, typeof(MapOfAllLocations));
                    break;
                case PossibleActivities.ReturnHere:
                    return;
                    //do nothing                    
            }
            StartActivity(intent);
        }

        private void BtnNewBooking_Click(object sender, EventArgs e)
        {
            GoToActivity(PossibleActivities.MapOffAllLocations);
        }
    }
}


