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
using SuperFluity.Office_Space_Classes;
using SuperFluity.Droid.Fragments;

namespace SuperFluity.Droid
{
    public class Fragment_EditOfficeSpace : Fragment
    {
        public static readonly int PickImageId = 1000;
        EditText etOfficeTitle;
        EditText etRoomSize;
        EditText etMaxPeople;
        EditText etMinPeople;
        EditText etFlatRate;
        EditText etRoomDescriptiion;
        Spinner spnRoomType;
        ImageView ivOfficePic;
        Button btnOfficeItems;
        Button btnOfficeCalendar;
        Button btnSave;
    
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_EditOfficeSpace, container, false);

            etOfficeTitle = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceTitle);
            etRoomSize = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceRoomSize);
            etMaxPeople = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceMaxPeople);
            etMinPeople = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceMinimumPeople);
            etFlatRate = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceFlatRate);
            etRoomDescriptiion = rootView.FindViewById<EditText>(Resource.Id.et_officeSpaceDescription);
            spnRoomType = rootView.FindViewById <Spinner>(Resource.Id.spn_officeSpaceType);
            ivOfficePic = rootView.FindViewById<ImageView>(Resource.Id.ivOfficeSpacePic);
            ivOfficePic.Click += PickOfficePicture;
            btnSave = rootView.FindViewById<Button>(Resource.Id.btn_officeSpaceSave);
            btnSave.Click += BtnSave_Click;
            btnOfficeItems = rootView.FindViewById<Button>(Resource.Id.btn_officeSpaceOfficeItems);
            btnOfficeItems.Click += BtnOfficeItems_Click;
            btnOfficeCalendar = rootView.FindViewById<Button>(Resource.Id.btn_OfficeCalendar);
            btnOfficeCalendar.Click += BtnOfficeCalendar_Click;

            OfficeSpace selectedOffice = CurrentSession.SelectedOfficeSpace;

            etOfficeTitle.Text = selectedOffice.Title;

            if (selectedOffice.RoomSize != 0)
            {
                etRoomSize.Text = selectedOffice.RoomSize.ToString();
            }
            else
            {
                etRoomSize.Text = "";
            }

            if (selectedOffice.MaximumPeople != 0)
            {
                etMaxPeople.Text = selectedOffice.MaximumPeople.ToString();
            }
            else
            {
                etMaxPeople.Text = "";
            }

            if (selectedOffice.MinimumPeople != 0)
            {
                etMinPeople.Text = selectedOffice.MinimumPeople.ToString();
            }
            else
            {
                etMinPeople.Text = "";
            }

            if (selectedOffice.FlatRatePerHour != 0)
            {
                etFlatRate.Text = selectedOffice.FlatRatePerHour.ToString();
            }
            else
            {
                etFlatRate.Text = "";
            }

            etRoomDescriptiion.Text = selectedOffice.Description;

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, OfficeSpace.OfficeTypes);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnRoomType.Adapter = adapter;
            spnRoomType.SetSelection((int)selectedOffice.OfficeTypeEnum);

            return rootView;
        }

        #region Pick OfficePicture
        private void PickOfficePicture(object sender, EventArgs e)
        {
            this.Activity.Intent = new Intent();
            this.Activity.Intent.SetType("image/*");
            this.Activity.Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(this.Activity.Intent, "Select Picture"), PickImageId);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri imageUri = data.Data;
                ivOfficePic.SetImageURI(imageUri);
            }
        }
        #endregion

        #region Validate Before Saving
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                OfficeSpace newlySavedSpace = new OfficeSpace(etOfficeTitle.Text,
                                                              Convert.ToInt32(etRoomSize.Text),
                                                              Convert.ToInt32(etMaxPeople.Text),
                                                              Convert.ToInt32(etMinPeople.Text),
                                                              Convert.ToInt32(etFlatRate.Text),
                                                              etRoomDescriptiion.Text, 
                                                              (OfficeSpace.OfficeType)spnRoomType.SelectedItemPosition);

                CurrentSession.SelectedOfficeSpace = newlySavedSpace;

                Toast.MakeText(this.Activity, "Save Success :-)", ToastLength.Short).Show();

                CurrentSession.SelectedCompanyLocation.OfficeSpaces[this.Activity.ActionBar.SelectedTab.Position] = CurrentSession.SelectedOfficeSpace;
                this.Activity.ActionBar.SelectedTab.SetText(CurrentSession.SelectedOfficeSpace.Title);
            }
        }

        private bool Validate()
        {
            return (ValidateTitle() && ValidateRoomSize() && ValidateMaxMinPeople() && ValidateFlatRate() && ValidateDescription());
            //remember to validate picture
            //we're not validating the office type for now, as those are pre-set
        }

        private bool ValidateTitle()
        {
            if(etOfficeTitle.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Office Title", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateRoomSize()
        {
            if(etRoomSize.Text.Trim().Length == 0 ||  Convert.ToInt32(etRoomSize.Text) == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Office Room Size", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateMaxMinPeople()
        {
            int max;
            int min;

            if(etMaxPeople.Text.Trim().Length == 0 || Convert.ToInt32(etMaxPeople.Text) == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Maximum Amount Of People", ToastLength.Short).Show();
                return false;
            }
            else
            {
                max = Convert.ToInt32(etMaxPeople.Text);
            }

            if (etMinPeople.Text.Trim().Length == 0 || Convert.ToInt32(etMinPeople.Text) == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Minimum Amount Of People", ToastLength.Short).Show();
                return false;
            }
            else
            {
                min = Convert.ToInt32(etMinPeople.Text);
            }

            if(min > max || min == max)
            {
                Toast.MakeText(this.Activity, "Maximum amount of people must be larger than minimum amount", ToastLength.Short).Show();
                return false;
            }

            return true;
        }

        private bool ValidateFlatRate()
        {
            if (etFlatRate.Text.Trim().Length == 0 || Convert.ToInt32(etFlatRate.Text) == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Flat Rate", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        
        private bool ValidateDescription()
        {
            if (etRoomDescriptiion.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Office Description", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        #endregion

        #region OfficeItems
        private void BtnOfficeItems_Click(object sender, EventArgs e)
        {
            Fragment_OfficeItems officeitemsFragment = new Fragment_OfficeItems();            

            Bundle args = new Bundle();
            args.PutBoolean("accessedFromOfficeSpace", true);
            officeitemsFragment.Arguments = args;
            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(Resource.Id.officespace_fragmentframe, officeitemsFragment);
            ft.Commit();
        }
        #endregion

        #region Calendar
        private void BtnOfficeCalendar_Click(object sender, EventArgs e)
        {
            Fragment_OfficeCalendar officecalendarFragment = new Fragment_OfficeCalendar();
            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(Resource.Id.officespace_fragmentframe, officecalendarFragment);
            ft.Commit();
        }
        #endregion

    }
}