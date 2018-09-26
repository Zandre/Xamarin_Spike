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

namespace SuperFluity.Droid
{
    public class Fragment_OfficeItems : Fragment
    {
        TextView tvDescription;
        Button btnSave;
        Button btnAddItem;
        ListView lvExistingItems;

        OfficeItem.ParentType fragmentWasAccessedFrom;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_OfficeItems, container, false);

            bool accessedFromOfficeSpace = Arguments.GetBoolean("accessedFromOfficeSpace");
            if (accessedFromOfficeSpace)
            {
                fragmentWasAccessedFrom = OfficeItem.ParentType.OfficeSpace;
            }
            else
            {
                fragmentWasAccessedFrom = OfficeItem.ParentType.CompanyLocation;
            }

            tvDescription = rootView.FindViewById<TextView>(Resource.Id.tvOfficeItemsDescription);
            if(fragmentWasAccessedFrom == OfficeItem.ParentType.OfficeSpace)
            {
                tvDescription.Text = "This section allows you to add/remove office items which is specific to a office space (ex: a fixed projector, or soundbar). These items will not incur additional charges to clients using a office space.";
            }
            else
            {
                tvDescription.Text = "This section allows you to add/remove generic office items to a company location (ex: a laptop, or loose standing projector). These items will incur additional charges to clients when used";
            }
            
            btnSave = rootView.FindViewById<Button>(Resource.Id.btnSaveOfficeItems);
            btnSave.Click += BtnSave_Click;
            btnAddItem = rootView.FindViewById<Button>(Resource.Id.btnAddOfficeItems);
            btnAddItem.Click += BtnAddOfficeItem_Click;
            lvExistingItems = rootView.FindViewById<ListView>(Resource.Id.lvOfficeItems);
            
            if (fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
            {
                var adapter = new OfficeItem_Adapter(this.Activity, CurrentSession.SelectedCompanyLocation.CompanyLocationItems, fragmentWasAccessedFrom);
                lvExistingItems.Adapter = adapter;
            }
            else
            {
                var adapter = new OfficeItem_Adapter(this.Activity, CurrentSession.SelectedOfficeSpace.OfficeItems, fragmentWasAccessedFrom);
                lvExistingItems.Adapter = adapter;
            }
                           
            lvExistingItems.ItemClick += OnListItemClick;
            return rootView;
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if(fragmentWasAccessedFrom == OfficeItem.ParentType.OfficeSpace)
            {
                CurrentSession.SelectedOfficeItem = CurrentSession.SelectedOfficeSpace.OfficeItems[e.Position];
            }
            else
            {
                CurrentSession.SelectedOfficeItem = CurrentSession.SelectedCompanyLocation.CompanyLocationItems[e.Position];
            }     
            AddOfficeItem();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
            {
                //re-add tabs & Action Bar
                this.Activity.ActionBar.Show();
                this.Activity.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            }
            else
            {
                Fragment_EditOfficeSpace officespaceFragment = new Fragment_EditOfficeSpace();
                var ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.officespace_fragmentframe, officespaceFragment);
                ft.Commit();
            }
        }

        private void BtnAddOfficeItem_Click(object sender, EventArgs e)
        {
            CurrentSession.SelectedOfficeItem = null;
            AddOfficeItem();
        }

        private void AddOfficeItem()
        {
            Fragment_EditOfficeItem edititemsFragment = new Fragment_EditOfficeItem();

            //parameters
            Bundle args = new Bundle();
            if (fragmentWasAccessedFrom == OfficeItem.ParentType.OfficeSpace)
            {
                args.PutBoolean("accessedFromOfficeSpace", true);
            }
            else
            {
                args.PutBoolean("accessedFromOfficeSpace", false);
            }
            edititemsFragment.Arguments = args;

            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(Resource.Id.officespace_fragmentframe, edititemsFragment);
            ft.Commit();
        }

        public override void OnDestroy()
        {            
            if (fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
            {
                //re-add tabs & Action Bar
                this.Activity.ActionBar.Show();
                this.Activity.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            }
            base.OnDestroy();
        }
    }
}