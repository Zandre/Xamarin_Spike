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

namespace SuperFluity.Droid.Fragments
{
    public class Fragment_OfficeCalendar : Fragment
    {
        CalendarView cv;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_OfficeCalendar, container, false);

            cv = rootView.FindViewById<CalendarView>(Resource.Id.cv_officeCalendar);
            cv.FirstDayOfWeek = 2;
            return rootView;
        }
    }
}