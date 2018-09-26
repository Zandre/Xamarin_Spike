using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SuperFluity.Office_Space_Classes;
using SuperFluity.Droid.Adapters;

namespace SuperFluity.Droid
{
    [Activity(Label = "Listings")]
    public class Listings : Activity
    {
        ListView lvCompanies;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Listing);
            ActionBar.Title = "My Listings";

            lvCompanies = FindViewById<ListView>(Resource.Id.lvCompanies);
            
            //adding fake companies
            AddCompaniesToList();            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            AddCompaniesToList();
        }

        #region Populate ListView with user's existing companies
        private void AddCompaniesToList()
        {
            var adapter = new Listing_Adapter(this, CurrentSession.LoggedOnUser.UserCompanies);
            lvCompanies.Adapter = adapter;
            lvCompanies.ItemClick += OnListItemClick;
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            CurrentSession.SelectedCompany = CurrentSession.LoggedOnUser.UserCompanies[e.Position];            
            ShowPopupMenu();
        }
        #endregion      

        #region Popup Menu
        private Dialog dialog;
        private void ShowPopupMenu()
        {
            dialog = new Dialog(this);
            dialog.RequestWindowFeature((int)WindowFeatures.ContextMenu);
            dialog.SetTitle(CurrentSession.SelectedCompany.Title);
            dialog.SetContentView(Resource.Layout.listings_menu);

            Button btnViewListingOnMap = dialog.FindViewById<Button>(Resource.Id.btn_viewlistingonmap);
            btnViewListingOnMap.Click += BtnViewListingOnMap_Click;
            Button btnDeleteListing = dialog.FindViewById<Button>(Resource.Id.btn_deletelisting);
            btnDeleteListing.Click += BtnDeleteListing_Click;

            dialog.Show();
        }

        private void BtnViewListingOnMap_Click(object sender, EventArgs e)
        {
            if(dialog != null && dialog.IsShowing)
            {
                dialog.Cancel();
            }
            Intent intent = new Intent(this, typeof(MapOfCompanyLocations));
            StartActivity(intent);
        }

        private void BtnDeleteListing_Click(object sender, EventArgs e)
        {
            if (dialog != null && dialog.IsShowing)
            {
                dialog.Cancel();
            }
            //delete from db goes here
        }
        #endregion

        #region Options Menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_listing, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.listing_addnew:
                    StartActivityForResult(new Intent(this, typeof(RegisterListing)),0);
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }
        #endregion

    }
}