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
using Android.Views.InputMethods;
using Android.Graphics;
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid
{
    [Activity(Label = "RegisterListing")]
    public class RegisterListing : Activity
    {

        public static readonly int PickImageId = 1000;
        EditText etCompanyName;
        Button btnSaveCompany;
        ImageView ivLogo;
        private bool LogoHasBeenPicked;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterListing);
            ActionBar.Title = "Create New Listing";

            etCompanyName = FindViewById<EditText>(Resource.Id.etListingCompanyName);
            btnSaveCompany = FindViewById<Button>(Resource.Id.btnListingSave);
            btnSaveCompany.Click += SaveCompany;
            ivLogo = FindViewById<ImageView>(Resource.Id.ivListingCompanyLogo);
            ivLogo.Click += PickLogo;
        }

        #region Create & Save new Company
        private void SaveCompany(object sender, EventArgs e)
        {
            if (CompanyIsValid())
            {
                ivLogo.BuildDrawingCache(true);
                Bitmap bitmapDrawingCache = ivLogo.GetDrawingCache(true);

                Android.Graphics.Drawables.BitmapDrawable drawable = (Android.Graphics.Drawables.BitmapDrawable)ivLogo.Drawable;
                Bitmap bitmap = drawable.Bitmap;

                Company newCompany = new Company(etCompanyName.Text, bitmap);
                CurrentSession.LoggedOnUser.AddUserCompany(newCompany);

                SetResult(Result.Ok);
                Finish();
            }
        }

        private bool CompanyIsValid()
        {
            if (etCompanyName.Text == null || etCompanyName.Text == String.Empty || etCompanyName.Text.Length > 80)
            {
                Toast.MakeText(this, "Invalid Company Name", ToastLength.Long).Show();
                return false;
            }
            else if (!LogoHasBeenPicked)
            {
                Toast.MakeText(this, "Invalid Logo", ToastLength.Long).Show();
                return false;
            }
            return true;
        }
        #endregion

        #region Pick Logo
        private void PickLogo(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri imageUri = data.Data;
                ivLogo.SetImageURI(imageUri);
                LogoHasBeenPicked = true;
            }
        }
        #endregion
    }
}