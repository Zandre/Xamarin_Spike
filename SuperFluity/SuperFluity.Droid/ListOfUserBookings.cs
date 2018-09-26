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
using SuperFluity.Droid.Adapters;
using SuperFluity.Office_Space_Classes;


namespace SuperFluity.Droid
{
    [Activity(Label = "ListOfUserBookings")]
    public class ListOfUserBookings : Activity
    {
        ListView lvBookings;        
        bool showConfirmed, showAwaitingPayment, showCancelled, showPastBookings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListOfUserBookings);
            ActionBar.Title = "My Bookings";

            showConfirmed = true;
            showAwaitingPayment = true;            
            showCancelled = false;
            showPastBookings = false;

            lvBookings = FindViewById<ListView>(Resource.Id.lvUserBookings);

            //adding fake bookings
            Booking_Adapter adapter = new Booking_Adapter(this, CurrentSession.LoggedOnUser.UserBookings);
            lvBookings.Adapter = adapter;
            lvBookings.ItemClick += LvBookings_ItemClick;
        }

        #region Options Menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_booking, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.bookingShowConfirmed:
                    showConfirmed = !showConfirmed;
                    break;
                case Resource.Id.bookingShowAwaitingPayment:
                    showAwaitingPayment = !showAwaitingPayment;
                    break;
                case Resource.Id.bookingShowCancelled:
                    showCancelled = !showCancelled;
                    break;
                case Resource.Id.bookingShowPastBookings:
                    showPastBookings = !showPastBookings;
                    break;
            }
            InvalidateOptionsMenu();
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            IMenuItem confirmed = menu.FindItem(Resource.Id.bookingShowConfirmed);
            confirmed.SetChecked(showConfirmed);

            IMenuItem awaiting = menu.FindItem(Resource.Id.bookingShowAwaitingPayment);
            awaiting.SetChecked(showAwaitingPayment);

            IMenuItem cancelled = menu.FindItem(Resource.Id.bookingShowCancelled);
            cancelled.SetChecked(showCancelled);

            IMenuItem passed = menu.FindItem(Resource.Id.bookingShowPastBookings);
            passed.SetChecked(showPastBookings);

            return true;
        }
#endregion

        private void LvBookings_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Booking selectedBooking = CurrentSession.LoggedOnUser.UserBookings[e.Position];
            ShowPopupMenu(selectedBooking);
        }

        #region Popup Menu
        private Dialog dialog;
        private void ShowPopupMenu(Booking selectedBooking)
        {
            dialog = new Dialog(this);
            dialog.RequestWindowFeature((int)WindowFeatures.ContextMenu);
            dialog.SetTitle("Booking Options");
            dialog.SetContentView(Resource.Layout.bookings_menu);

            Button btnViewQuote = dialog.FindViewById<Button>(Resource.Id.btnBookingQuote);
            btnViewQuote.Click += BtnViewQuote_Click;
            Button btnMakePayment = dialog.FindViewById<Button>(Resource.Id.btnBookingPayment);
            btnMakePayment.Click += BtnMakePayment_Click;
            Button btnEditBooking = dialog.FindViewById<Button>(Resource.Id.btnBookingEdit);
            btnEditBooking.Click += BtnEditBooking_Click;
            Button btnCancelBooking = dialog.FindViewById<Button>(Resource.Id.btnBookingCancel);
            btnCancelBooking.Click += BtnCancelBooking_Click;
            Button btnShareBooking = dialog.FindViewById<Button>(Resource.Id.btnBookingShare);
            btnShareBooking.Click += BtnShareBooking_Click;
            Button btnViewMap = dialog.FindViewById<Button>(Resource.Id.btnBookingViewMap);
            btnViewMap.Click += BtnViewMap_Click;

            int result = DateTime.Compare(selectedBooking.EndTime, DateTime.Now);

            if (result >= 0)
            {
                switch (selectedBooking.Status)
                {
                    //also remember to check if booking is past already

                    case Booking.BookingStatusEnum.Confirmed:
                        btnViewQuote.Enabled = true;
                        btnMakePayment.Enabled = false;
                        btnEditBooking.Enabled = false;
                        btnCancelBooking.Enabled = false;
                        btnShareBooking.Enabled = true;
                        btnViewMap.Enabled = true;
                        break;

                    case Booking.BookingStatusEnum.AwaitingPayment:
                        btnViewQuote.Enabled = true;
                        btnMakePayment.Enabled = true;
                        btnEditBooking.Enabled = true;
                        btnCancelBooking.Enabled = true;
                        btnShareBooking.Enabled = true;
                        btnViewMap.Enabled = true;
                        break;

                    case Booking.BookingStatusEnum.Cancelled:
                        btnViewQuote.Enabled = true;
                        btnMakePayment.Enabled = false;
                        btnEditBooking.Enabled = false;
                        btnCancelBooking.Enabled = false;
                        btnShareBooking.Enabled = false;
                        btnViewMap.Enabled = true;
                        break;
                }
            }
            else
            {
                btnViewQuote.Enabled = true;
                btnMakePayment.Enabled = false;
                btnEditBooking.Enabled = false;
                btnCancelBooking.Enabled = false;
                btnShareBooking.Enabled = true;
                btnViewMap.Enabled = true;
            }


            dialog.Show();
        }
        #endregion

        #region Popup Menu Features
                private void BtnViewMap_Click(object sender, EventArgs e)
                {

                }

                private void BtnShareBooking_Click(object sender, EventArgs e)
                {

                }

                private void BtnCancelBooking_Click(object sender, EventArgs e)
                {

                }

                private void BtnEditBooking_Click(object sender, EventArgs e)
                {

                }

                private void BtnMakePayment_Click(object sender, EventArgs e)
                {

                }

                private void BtnViewQuote_Click(object sender, EventArgs e)
                {

                }
        #endregion

    }
}