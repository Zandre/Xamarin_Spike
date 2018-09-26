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
    class Fragment_EditOfficeItem : Fragment
    {
        Button btnSave;
        EditText etItemTitle;
        EditText etItemDescription;
        EditText etItemFlatRate;
        TextView tvItemFlatRate;

        OfficeItem.ParentType fragmentWasAccessedFrom;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_EditOfficeItem, container, false);

            bool accessedFromOfficeSpace = Arguments.GetBoolean("accessedFromOfficeSpace");
            if(accessedFromOfficeSpace)
            {
                fragmentWasAccessedFrom = OfficeItem.ParentType.OfficeSpace;
            }
            else
            {
                fragmentWasAccessedFrom = OfficeItem.ParentType.CompanyLocation;
            }

            btnSave = rootView.FindViewById<Button>(Resource.Id.btn_itemSave);
            btnSave.Click += BtnSave_Click;

            etItemTitle = rootView.FindViewById<EditText>(Resource.Id.et_itemTitle);
            etItemDescription = rootView.FindViewById<EditText>(Resource.Id.et_itemDescription);
            etItemFlatRate = rootView.FindViewById<EditText>(Resource.Id.et_itemFlatRate);
            tvItemFlatRate = rootView.FindViewById<TextView>(Resource.Id.tv_itemflatRate);

            if(CurrentSession.SelectedOfficeItem != null)
            {
                etItemTitle.Text = CurrentSession.SelectedOfficeItem.Title;
                etItemDescription.Text = CurrentSession.SelectedOfficeItem.Description;                
            }

            if (fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
            {
                if(CurrentSession.SelectedOfficeItem != null)
                {
                    etItemFlatRate.Text = CurrentSession.SelectedOfficeItem.FlatRatePerHour.ToString();
                }                
            }
            else
            {

                etItemFlatRate.Visibility = ViewStates.Gone;
                tvItemFlatRate.Visibility = ViewStates.Gone;
            }

            return rootView;
        }

        #region Validate Before Saving
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(Validate())
            {

                string title = etItemTitle.Text;
                string description = etItemDescription.Text;

                if(fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
                {
                    if(CurrentSession.SelectedOfficeItem == null)
                    {
                        int flatRate = Convert.ToInt32(etItemFlatRate.Text);
                        OfficeItem newItem = new OfficeItem(title, description, flatRate);
                        CurrentSession.SelectedCompanyLocation.AddCompanyLocationItem(newItem);
                    }
                    else
                    {
                        int flatRate = Convert.ToInt32(etItemFlatRate.Text);
                        OfficeItem newItem = new OfficeItem(title, description, flatRate);
                        //super dodgy, will sort this out when working with proper db-Id's
                        OfficeItem modifiedItem = CurrentSession.SelectedCompanyLocation.CompanyLocationItems.FirstOrDefault(officeItem => officeItem.Title.Contains(CurrentSession.SelectedOfficeItem.Title));
                        //please find easier way to update details of object
                        modifiedItem.Title = newItem.Title;
                        modifiedItem.Description = newItem.Description;
                    }
                }
                else
                {
                    if(CurrentSession.SelectedOfficeItem == null)
                    {
                        OfficeItem newItem = new OfficeItem(title, description);
                        CurrentSession.SelectedOfficeSpace.AddOfficeItem(newItem);
                    }
                    else
                    {
                        OfficeItem newItem = new OfficeItem(title, description);
                        //super dodgy, will sort this out when working with proper db-Id's
                        OfficeItem modifiedItem = CurrentSession.SelectedOfficeSpace.OfficeItems.FirstOrDefault(officeItem => officeItem.Title.Contains(CurrentSession.SelectedOfficeItem.Title));
                        //please find easier way to update details of object
                        modifiedItem.Title = newItem.Title;
                        modifiedItem.Description = newItem.Description;
                    }
                }

                Fragment_OfficeItems officeitemsFragment = new Fragment_OfficeItems();

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
                officeitemsFragment.Arguments = args;

                var ft = FragmentManager.BeginTransaction();
                ft.AddToBackStack(null);
                ft.Replace(Resource.Id.officespace_fragmentframe, officeitemsFragment);
                ft.Commit();
            }
        }

        private bool Validate()
        {
            return (ValidateTitle() && ValidateDescription() && ValidateItemFlatRate());
        }

        private bool ValidateTitle()
        {
            if (etItemTitle.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Item Title", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateDescription()
        {
            if (etItemDescription.Text.Trim().Length == 0)
            {
                Toast.MakeText(this.Activity, "Invalid Item Description", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateItemFlatRate()
        {
            if(fragmentWasAccessedFrom == OfficeItem.ParentType.CompanyLocation)
            {
                if (etItemFlatRate.Text.Trim().Length == 0 || Convert.ToInt32(etItemFlatRate.Text) == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Item FlatRate", ToastLength.Short).Show();
                    return false;
                }
                return true;
            }
            return true;
        }
        #endregion
    }
}