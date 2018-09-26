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
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid.Adapters
{
    class Listing_Adapter: BaseAdapter<Company>
    {

        private Activity _activity;
        private List<Company> _companies;


        public Listing_Adapter(Activity activity, List<Company> companies)
        {
            _activity = activity;
            _companies = companies;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Company this[int index]
        {
            get { return _companies[index]; }
        }

        public override int Count
        {
            get { return _companies.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

                var selectedItem = _companies[position];

                TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                text1.Text = selectedItem.Title;

                TextView text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
                int locations = selectedItem.CompanyLocations.Count();
                int offices = 0;
                foreach(CompanyLocation location in selectedItem.CompanyLocations)
                {
                    offices += location.OfficeSpaces.Count();
                }
                text2.Text = locations + " locations, " + offices + " office spaces";
            }
            return view;
        }
    }
}