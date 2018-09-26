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
    class AddItemToBooking_Adapter : BaseAdapter<OfficeItem>
    {

        private Activity context;
        List<OfficeItem> items;

        public AddItemToBooking_Adapter(Activity _context, List<OfficeItem> _items)
        {
            context = _context;
            items = _items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItemMultipleChoice, null);
            }

            var selectedItem = items[position];

            TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = selectedItem.Title + " - R" + selectedItem.FlatRatePerHour + " p/h";

            return view;
        }

        public override int Count { get; }

        public override OfficeItem this[int position]
        {
            get
            {
                OfficeItem selecItem = items[position];
                return selecItem;
            }
        }
    }
}