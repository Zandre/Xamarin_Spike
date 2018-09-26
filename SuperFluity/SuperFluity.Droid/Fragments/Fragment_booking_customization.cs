using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SuperFluity.Droid.Adapters;
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid.Fragments
{
    public class Fragment_booking_customization : Fragment, TimePickerDialog.IOnTimeSetListener, DatePickerDialog.IOnDateSetListener
    {
        #region Control

        private Button btnDate;
        private Button btnStartTime;
        private Button btnEndTime;
        private Button btnConfirmBooking;
        private Button btnCancel;
        private LinearLayout llAvailableItemsLayout;
        private LinearLayout llIncludedItemsLayout;
        private LinearLayout llQuote;

        private LinearLayout llMainLayout;

        private int finalPrice;

        private DateTime dtBookingDate;
        private DateTime dtStartTime;
        private DateTime dtEndTime;

        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_booking_customization, container, false);

            btnDate = rootView.FindViewById<Button>(Resource.Id.btn_bookingcustomization_date);
            btnStartTime = rootView.FindViewById<Button>(Resource.Id.btn_bookingcustomization_startTime);
            btnEndTime = rootView.FindViewById<Button>(Resource.Id.btn_bookingcustomization_endTime);
            btnConfirmBooking = rootView.FindViewById<Button>(Resource.Id.btn_bookingcustomization_confirm);
            btnCancel = rootView.FindViewById<Button>(Resource.Id.btn_bookingcustomization_cancel);
            llAvailableItemsLayout = rootView.FindViewById<LinearLayout>(Resource.Id.ll_bookingcustomization_availableitems);
            llIncludedItemsLayout = rootView.FindViewById<LinearLayout>(Resource.Id.ll_bookingcustomization_includeditems);
            llQuote = rootView.FindViewById<LinearLayout>(Resource.Id.ll_bookingcustomization_quote);
            llMainLayout = rootView.FindViewById<LinearLayout>(Resource.Id.ll_bookingcustomization_main);

            btnDate.Click += ShowDatePicker;
            btnStartTime.Click += ShowTimePicker;
            btnEndTime.Click += ShowTimePicker;

            dtStartTime = DateTime.Now;
            dtEndTime = DateTime.Now.AddHours(1);
            dtBookingDate = DateTime.Now;

            #region Optional Additional Items
            foreach (OfficeItem item in CurrentSession.SelectedCompanyLocation.CompanyLocationItems)
            {
                CheckBox chkItem = new CheckBox(this.Activity);
                chkItem.Text = item.Title;
                chkItem.Tag = item.FlatRatePerHour;
                chkItem.CheckedChange += ChkItem_CheckedChange;
                llAvailableItemsLayout.AddView(chkItem);
            }
            #endregion

            #region Fixed Items in Office Space
            foreach (OfficeItem item in CurrentSession.SelectedOfficeSpace.OfficeItems)
            {
                TextView tvItem = new TextView(this.Activity);
                tvItem.Text = item.Title;
                llIncludedItemsLayout.AddView(tvItem);
            }
            #endregion

            TimeSpan tsHoursBooked = dtEndTime - dtStartTime;
            TextView tvHoursBooked = new TextView(this.Activity);
            tvHoursBooked.Text = "Hours Booked:\t" + tsHoursBooked.Hours + " hour(s)";
            llQuote.AddView(tvHoursBooked);

            TextView tvroomPrice = new TextView(this.Activity);            
            tvroomPrice.Text = "Office Rate:\tR" + CurrentSession.SelectedOfficeSpace.FlatRatePerHour + " p\\h";
            llQuote.AddView(tvroomPrice);

            TextView tvFinalQuote = new TextView(this.Activity);
            tvFinalQuote.SetTypeface(null, TypefaceStyle.Bold);
            tvFinalQuote.Text = "FINAL QUOTE:\tR" + CurrentSession.SelectedOfficeSpace.FlatRatePerHour;
            llQuote.AddView(tvFinalQuote);            

            return rootView;
        }

        private void ChkItem_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CalculateQuote();
        }

        private void CalculateQuote()
        {
            llQuote.RemoveAllViews();

            finalPrice = 0;

            TimeSpan tsHoursBooked = dtEndTime - dtStartTime;            
            TextView tvHoursBooked = new TextView(this.Activity);
            tvHoursBooked.Text = "Hours Booked:\t" + tsHoursBooked.Hours + " hour(s)";
            llQuote.AddView(tvHoursBooked);

            for (int i = 0; i < llAvailableItemsLayout.ChildCount; i++)
            {
                View v = llAvailableItemsLayout.GetChildAt(i);
                if (v is CheckBox)
                {
                    CheckBox chk = v as CheckBox;
                    if (chk.Checked)
                    {
                        TextView tvItemAdded = new TextView(this.Activity);
                        tvItemAdded.Text = chk.Text + ":\tR" + chk.Tag.ToString() + " p\\h";
                        int priceForThisItemMultipliedByHoursBooked = (Convert.ToInt32(chk.Tag) * tsHoursBooked.Hours);
                        finalPrice += priceForThisItemMultipliedByHoursBooked;
                        llQuote.AddView(tvItemAdded);
                    }
                }
            }

            TextView tvroomPrice = new TextView(this.Activity);
            tvroomPrice.Text = "Office Rate:\tR" + CurrentSession.SelectedOfficeSpace.FlatRatePerHour + " p\\h";
            int priceForThisRoomMultipliedByHoursBooked = (CurrentSession.SelectedOfficeSpace.FlatRatePerHour * tsHoursBooked.Hours);
            finalPrice += priceForThisRoomMultipliedByHoursBooked;
            llQuote.AddView(tvroomPrice);

            TextView tvFinalQuote = new TextView(this.Activity);
            tvFinalQuote.SetTypeface(null, TypefaceStyle.Bold);
            tvFinalQuote.Text = "FINAL QUOTE:\tR" + finalPrice;
            llQuote.AddView(tvFinalQuote);
        }

        #region Time Picker
        private void ShowTimePicker(object sender, EventArgs e)
        {
            TimePickerDialog tp1;
            switch (((Button) sender).Tag.ToString())
            {
                case "StartTime":
                    tp1 = new TimePickerDialog(this.Activity, this, dtStartTime.Hour, dtStartTime.Minute, true);
                    tp1.SetTitle("Booking - Start Time");
                    selectedTime = SelectedTime.StartTime;
                    tp1.Show();
                    break;
                case "EndTime":
                    tp1 = new TimePickerDialog(this.Activity, this, dtEndTime.Hour, dtEndTime.Minute, true);
                    tp1.SetTitle("Booking - End Time");
                    selectedTime = SelectedTime.EndTime;
                    tp1.Show();
                    break;
            }                                  
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            DateTime startTimeBeforeBeingSet = dtStartTime;
            DateTime endTimeBeforeBeingSet = dtEndTime;

            string _minute = "";
            if (minute < 10)
            {
                _minute = "0" + minute;
            }
            else
            {
                _minute = minute.ToString();
            }
            switch (selectedTime)
            {
                 case SelectedTime.StartTime:
                    dtStartTime = new DateTime(1900, 1, 1, hourOfDay, minute, 0);
                    btnStartTime.Text = "Start Time " + hourOfDay + ":" + _minute;
                    break;
                 case SelectedTime.EndTime:
                    dtEndTime = new DateTime(1900, 1, 1, hourOfDay, minute, 0);
                    btnEndTime.Text = "End Time " + hourOfDay + ":" + _minute;
                    break;
            }

            if (dtStartTime > dtEndTime)
            {
                Toast toast = Toast.MakeText(this.Activity, "End time of booking must be after Start time of booking", ToastLength.Short);                
                toast.Show();
                dtStartTime = startTimeBeforeBeingSet;
                dtEndTime = endTimeBeforeBeingSet;
                btnStartTime.Text = "Start Time " + dtStartTime.Hour + ":" + dtStartTime.Minute;
                btnEndTime.Text = "End Time " + dtEndTime.Hour + ":" + dtEndTime.Minute;
            }
            else
            {
                CalculateQuote();
            }
        }

        private SelectedTime selectedTime = SelectedTime.StartTime;
        public enum SelectedTime {StartTime, EndTime};
        #endregion

        #region DatePicker

        private void ShowDatePicker(object sender, EventArgs e)
        {
            DatePickerDialog dp = new DatePickerDialog(this.Activity, this, 
                                                                dtBookingDate.Year, 
                                                                dtBookingDate.Month - 1, 
                                                                dtBookingDate.DayOfYear);
            dp.SetTitle("Booking - Date");
            dp.Show();
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            dtBookingDate = new DateTime(year, monthOfYear+ 1, dayOfMonth);
            string Month = new DateTime(year, monthOfYear + 1, dayOfMonth).ToString("MMMM", CultureInfo.InvariantCulture);

            btnDate.Text = dayOfMonth + " "
                           + Month
                           + " "
                           + year;
        }
        #endregion
    }
}