using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Locations;
using SuperFluity.Office_Space_Classes;
using System.Text.RegularExpressions;

namespace SuperFluity.Droid.Fragments
{
    public class Fragment_CompanyLocationDetail : Fragment
    {
        #region Control Variables
        Button btnSave;
        Button btnSetHours;

        EditText etCompanyName;
        EditText etContactNumber;
        EditText etEmail;
        EditText etStreetAddress;
        EditText etLatitude;
        EditText etLongitude;
        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_CompanyLocationDetail, container, false);

            btnSave = rootView.FindViewById<Button>(Resource.Id.btnSaveCompanyLocation);
            btnSave.Click += Save;
            btnSetHours = rootView.FindViewById<Button>(Resource.Id.btnSetOfficeHours);
            btnSetHours.Click += BtnSetHours_Click;           

            etCompanyName = rootView.FindViewById<EditText>(Resource.Id.etCompanyLocationName);
            etContactNumber = rootView.FindViewById<EditText>(Resource.Id.etCompanyLocationContactNumber);
            etEmail = rootView.FindViewById<EditText>(Resource.Id.etCompanyLocationEmail);
            etStreetAddress = rootView.FindViewById<EditText>(Resource.Id.etCompanyLocationAddress);
            etLatitude = rootView.FindViewById<EditText>(Resource.Id.etCompanyLatitude);
            etLongitude = rootView.FindViewById<EditText>(Resource.Id.etCompanyLongitude);


            if(CurrentSession.SelectedCompanyLocation != null)
            {
                etCompanyName.Text = CurrentSession.SelectedCompanyLocation.Title;
                etContactNumber.Text = CurrentSession.SelectedCompanyLocation.ContactNumber;
                etEmail.Text = CurrentSession.SelectedCompanyLocation.Email;
                etStreetAddress.Text = CurrentSession.SelectedCompanyLocation.StreetAddress;
                etLatitude.Text = CurrentSession.SelectedCompanyLocation.Latitude.ToString();
                etLongitude.Text = CurrentSession.SelectedCompanyLocation.Longitude.ToString();
                btnSetHours.Enabled = true;
            }
            else
            {
                Bundle bundle = this.Arguments;
                string lat = bundle.GetString("Latitude");
                double latitude = Convert.ToDouble(lat);
                etLatitude.Text = lat;
                string lon = bundle.GetString("Longitude");
                double longitude = Convert.ToDouble(lat);
                etLongitude.Text = lon;
                GetAddress(latitude, longitude);
                btnSetHours.Enabled = false;
            }

            return rootView;
        }

        private void BtnSetHours_Click(object sender, EventArgs e)
        {
            Fragment_EditOfficeHours fragment_officeHours = new Fragment_EditOfficeHours();
            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(Resource.Id.locationdetail_fragmentframe, fragment_officeHours);
            ft.Commit();
        }

        void GetAddress(double selectedLatitude, double selectLongitude)
        {
            Geocoder geocoder = new Geocoder(this.Activity);
            IList<Address> addressList = geocoder.GetFromLocation(selectedLatitude, selectLongitude, 1);
            Address selectedAddress = addressList.FirstOrDefault();

            if (selectedAddress != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < selectedAddress.MaxAddressLineIndex; i++)
                {
                    deviceAddress.Append(selectedAddress.GetAddressLine(i) + ", ");
                }
                string address = deviceAddress.ToString();
                //remove last comma and blank space
                address = address.Remove(address.Length - 2);
                etStreetAddress.Text = address;
            }
        }

        #region Validate Before Saving
        private void Save(object sender, EventArgs e)
        {
            if (Validate())
            {
                if(CurrentSession.SelectedCompanyLocation != null)
                {
                    CurrentSession.SelectedCompanyLocation.Title = etCompanyName.Text;
                    CurrentSession.SelectedCompanyLocation.Longitude = Convert.ToDouble(etLongitude.Text);
                    CurrentSession.SelectedCompanyLocation.Latitude = Convert.ToDouble(etLatitude.Text);
                    CurrentSession.SelectedCompanyLocation.StreetAddress = etStreetAddress.Text;
                    CurrentSession.SelectedCompanyLocation.ContactNumber = etContactNumber.Text;
                    CurrentSession.SelectedCompanyLocation.Email = etEmail.Text;
                }
                else
                {
                    CompanyLocation newLocation = new CompanyLocation(etCompanyName.Text,
                                                                        Convert.ToDouble(etLongitude.Text),
                                                                        Convert.ToDouble(etLatitude.Text),
                                                                        etStreetAddress.Text, etContactNumber.Text,
                                                                        etEmail.Text,
                                                                        CompanyLocation.Status.Active,
                                                                        2);

                    CurrentSession.SelectedCompany.AddCompanyLocation(newLocation);
                    CurrentSession.SelectedCompanyLocation = newLocation;
                }

                Toast.MakeText(this.Activity, "SAVE SUCCESS :-)", ToastLength.Short).Show();

                btnSetHours.Enabled = true;
            }
        }

        private bool Validate()
        {
            return (ValidateName() &&
                    ValidateContactNumber() &&
                    ValidateEmail() &&
                    ValidateStreetAddress() &&
                    ValidateLocation());
        }

        private bool ValidateName()
        {
            if (etCompanyName.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Location Title", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateContactNumber()
        {
            if (!Regex.Match(etContactNumber.Text, @"^(\d{10})$").Success)
            {
                Toast.MakeText(this.Activity, "Invalid Contact Number", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateEmail()
        {
            //http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address

            var addr = new System.Net.Mail.MailAddress(etEmail.Text);
            if (addr.Address != etEmail.Text)
            {
                Toast.MakeText(this.Activity, "Invalid Email Address", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateStreetAddress()
        {
            if (etStreetAddress.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Street Address", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateLocation()
        {
            double latitude = Convert.ToDouble(etLatitude.Text);
            double longitude = Convert.ToDouble(etLongitude.Text);

            if (latitude < -90 || latitude > 90)
            {
                Toast.MakeText(this.Activity, "Invalid Location: Latitude must be between -90 and 90 degrees inclusive", ToastLength.Short).Show();
                return false;
            }
            else if (longitude < -180 || longitude > 180)
            {
                Toast.MakeText(this.Activity, "Invalid Location: Longitude must be between -180 and 180 degrees inclusive", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        #endregion
    }
}