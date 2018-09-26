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

namespace SuperFluity.Droid
{
    public class OfficeItem_Adapter: BaseAdapter<OfficeItem>
    {
        private Activity _activity;
        private List<OfficeItem> _items;
        private OfficeItem.ParentType _type;

        public OfficeItem_Adapter(Activity activity, List<OfficeItem> items, OfficeItem.ParentType type)
        {
            _activity = activity;
            _items = items;
            _type = type;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override OfficeItem this[int index]
        {
            get { return _items[index]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                if(_type == OfficeItem.ParentType.CompanyLocation)
                {
                    view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

                    var selectedItem = _items[position];

                    TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                    text1.Text = selectedItem.Title;

                    TextView text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
                    text2.Text = "R" + selectedItem.FlatRatePerHour.ToString() + " p/h";
                }
                else
                {
                    view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

                    var selectedItem = _items[position];

                    TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                    text1.Text = selectedItem.Title;
                }
                
            }



            return view;
        }

    }
}