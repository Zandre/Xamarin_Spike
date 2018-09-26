using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SuperFluity.Office_Space_Classes;
using Object = Java.Lang.Object;

namespace SuperFluity.Droid.Adapters
{
    public class Booking_Adapter : BaseAdapter<Booking>
    {
        private Activity context;
        List<Booking> bookings;

        public Booking_Adapter(Activity _context, List<Booking> _bookings)
        {            
            context = _context;
            bookings = _bookings;
        }

        #region BaseAdapterMembers
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.listItem_Booking, null);
            }

            TextView officeTitle = view.FindViewById<TextView>(Resource.Id.tvBooking_Office);
            TextView bookingPrice = view.FindViewById<TextView>(Resource.Id.tvBooking_Price);
            TextView bookingRegion = view.FindViewById<TextView>(Resource.Id.tvBooking_Location);
            TextView bookingDate = view.FindViewById<TextView>(Resource.Id.tvBooking_Date);
            TextView bookingTime = view.FindViewById<TextView>(Resource.Id.tvBooking_Time);
            TextView bookingSatus = view.FindViewById<TextView>(Resource.Id.tvBooking_Status);
            LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.bookingbackground);

            Booking selectedBooking = bookings[position];
            officeTitle.Text = selectedBooking.OfficeSpace.Title;
            bookingPrice.Text = "R " + selectedBooking.Price.ToString();
            //remember the region of the office
            bookingDate.Text = selectedBooking.FriendlyDate;
            bookingTime.Text = selectedBooking.FriendlyStartTime + " - " + selectedBooking.FriendlyEndTime;
            bookingSatus.Text = selectedBooking.FriendlyStatus;
            bookingSatus.SetTextColor(selectedBooking.StatusColor);
        
            return view;
        }

        public override int Count
        {
            get
            {
                if (bookings != null)
                {
                    return bookings.Count;
                }
                return 0;
            }
        }

        public override Booking this[int position]
        {
            get
            {
                Booking selectedBooking = bookings[position];
                return selectedBooking;
            }
        }
        #endregion
    }
}