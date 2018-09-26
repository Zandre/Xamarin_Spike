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

namespace SuperFluity.Droid.Fragments
{
    public class Fragment_BookOfficeSpace : Fragment
    {
        private TextView tvOfficeSize;
        private TextView tvPrice;
        private TextView tvMaxPeople;
        private TextView tvMinPeople;
        private TextView tvOfficeType;
        private TextView tvOfficeDescription;
        private Spinner spnIncludedItems;
        private Spinner spnAvailableEXtras;
        private Button btnMakeBooking;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_BookOfficeSpace, container, false);

            OfficeSpace selectedOfficeSpace = CurrentSession.SelectedOfficeSpace;

            tvOfficeSize = rootView.FindViewById<TextView>(Resource.Id.tvBookOffice_RoomSize);
            tvOfficeSize.Text = selectedOfficeSpace.RoomSize + " square meters";
            tvPrice = rootView.FindViewById<TextView>(Resource.Id.tvBookOffice_Price);
            tvPrice.Text = "R " + selectedOfficeSpace.FlatRatePerHour;
            tvMaxPeople = rootView.FindViewById<TextView>(Resource.Id.tvBookOffice_MaximumPeople);
            tvMaxPeople.Text = selectedOfficeSpace.MaximumPeople.ToString();
            tvMinPeople = rootView.FindViewById<TextView>(Resource.Id.tvBookOffice_MinimumPeople);
            tvMinPeople.Text = selectedOfficeSpace.MinimumPeople.ToString();
            tvOfficeType = rootView.FindViewById<TextView>(Resource.Id.tvBookOffice_OfficeType);
            tvOfficeType.Text = selectedOfficeSpace.FriendlyOfficeTypeEnum;
            tvOfficeDescription = rootView.FindViewById<TextView>(Resource.Id.tvOfficeBooking_Description);
            tvOfficeDescription.Text = selectedOfficeSpace.Description;

            spnIncludedItems = rootView.FindViewById<Spinner>(Resource.Id.spnOfficeBooking_IncludedItems);
            List<string> includedItems = new List<string>();
            foreach (OfficeItem item in selectedOfficeSpace.OfficeItems)
            {
                includedItems.Add(item.Title);
            }
            ArrayAdapter<string> includedItemsAdapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, includedItems);
            includedItemsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnIncludedItems.Adapter = includedItemsAdapter;



            spnAvailableEXtras = rootView.FindViewById<Spinner>(Resource.Id.spnOfficeBooking_AvailableExtras);
            List<string> availableExtras = new List<string>();
            foreach (OfficeItem item in CurrentSession.SelectedCompanyLocation.CompanyLocationItems)
            {
                availableExtras.Add(item.Title);
            }
            ArrayAdapter<string> availableExtrasAdapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, availableExtras);
            availableExtrasAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnAvailableEXtras.Adapter = availableExtrasAdapter;


            btnMakeBooking = rootView.FindViewById<Button>(Resource.Id.btn_BookOfficeSpaceBook);
            btnMakeBooking.Click += BtnMakeBooking_Click;

            return rootView;
        }
        
        private void BtnMakeBooking_Click(object sender, EventArgs e)
        {
            Fragment_booking_customization bookingCustomization = new Fragment_booking_customization();
            var bookingManager = FragmentManager.BeginTransaction();
            bookingManager.Replace(Resource.Id.officespace_fragmentframe, bookingCustomization);
            bookingManager.Commit();
        }
    }
}