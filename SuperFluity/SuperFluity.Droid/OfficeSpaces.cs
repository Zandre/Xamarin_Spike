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
using SuperFluity.Droid.Fragments;
using SuperFluity.Office_Space_Classes;
using static Android.App.ActionBar;

namespace SuperFluity.Droid
{
    [Activity(Label = "OfficeSpaces", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class OfficeSpaces : Activity
    {        
        private Fragment_EditOfficeSpace editOfficeSpace;
        private Fragment_BookOfficeSpace bookOfficeSpace;

        private AccessFunction accessFunction;
        public enum AccessFunction
        {
            EditOfficeSpace = 0,
            MakeBooking = 1
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
  
            this.ActionBar.Title = CurrentSession.SelectedCompanyLocation.Title;
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.OfficeSpace);

            int aF = Intent.GetIntExtra("accessFunction", 1);
            accessFunction = (AccessFunction)aF;

            foreach(OfficeSpace office in CurrentSession.SelectedCompanyLocation.OfficeSpaces)
            {
                AddTab(office.Title);
            }
        }

        private void AddTab(string title)
        {
            Tab tab = ActionBar.NewTab();
            tab.SetText(title);
            tab.TabSelected += (sender, args) =>
            {
                CurrentSession.SelectedOfficeSpace = CurrentSession.SelectedCompanyLocation.OfficeSpaces[tab.Position];

                switch (accessFunction)
                {
                        case AccessFunction.EditOfficeSpace:
                            editOfficeSpace = new Fragment_EditOfficeSpace();
                            var editManager = FragmentManager.BeginTransaction();
                            editManager.Replace(Resource.Id.officespace_fragmentframe, editOfficeSpace);
                            editManager.Commit();
                        break;

                        case AccessFunction.MakeBooking:
                            bookOfficeSpace = new Fragment_BookOfficeSpace();
                            var bookingManager = FragmentManager.BeginTransaction();
                            bookingManager.Replace(Resource.Id.officespace_fragmentframe, bookOfficeSpace);
                            bookingManager.Commit();
                        break;
                }

            };
            ActionBar.AddTab(tab);
        }

        #region Options Menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_officespace, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.addnewofficespace:
                    AddNewOfficeSpace();
                    break;
                case Resource.Id.removeofficespace:
                    AskBeforeDelete();
                    break;
                case Resource.Id.addgenericofficeitem:
                    ActionBar.NavigationMode = ActionBarNavigationMode.Standard;
                    ActionBar.Hide();
                    Fragment_OfficeItems officeitemsFragment = new Fragment_OfficeItems();
                    Bundle args = new Bundle();
                    args.PutBoolean("accessedFromOfficeSpace", false);
                    officeitemsFragment.Arguments = args;
                    var ft = FragmentManager.BeginTransaction();
                    ft.AddToBackStack(null);
                    ft.Replace(Resource.Id.officespace_fragmentframe, officeitemsFragment);
                    ft.Commit();
                    break;
                case Resource.Id.editcompanylocation:
                    StartActivityForResult(new Intent(this, typeof(CompanyLocationDetail)), 0);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        #endregion

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //for now this is only used when edit the company location from this activity
            this.ActionBar.Title = CurrentSession.SelectedCompanyLocation.Title;
        }

        #region Adding / Removing Office Spaces
        private void AddNewOfficeSpace()
        {
            //OfficeSpace defaultSpace = new OfficeSpace("New Office Space", 25, 7, 3, 300, "A new office space", OfficeSpace.OfficeType.SmallMeetingRoom);
            OfficeSpace emptyOffice = new OfficeSpace();
            CurrentSession.SelectedCompanyLocation.OfficeSpaces.Add(emptyOffice);
            AddTab("New Office Space");
        }

        private void AskBeforeDelete()
        {
            if(CurrentSession.SelectedCompanyLocation.OfficeSpaces.Count > 0)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle(CurrentSession.SelectedCompanyLocation.OfficeSpaces[ActionBar.SelectedTab.Position].Title);

                if (CurrentSession.SelectedCompanyLocation.OfficeSpaces.Count > 1)
                {
                    alert.SetMessage("Are you sure you want to delete this office space?");
                }
                else
                {
                    alert.SetMessage("You are about to delete the last remaining office space in " + CurrentSession.SelectedCompanyLocation.Title + ". Are you sure you want to do this?");
                }


                alert.SetPositiveButton("Yes", (senderAlert, args) =>
                {
                    RemoveOfficeSpace();
                });
                alert.SetNegativeButton("No", (senderAlert, args) =>
                {

                });

                RunOnUiThread(() =>
                {
                    alert.Show();
                });
            }
        }

        private void RemoveOfficeSpace()
        {
            CurrentSession.SelectedCompanyLocation.OfficeSpaces.RemoveAt(ActionBar.SelectedTab.Position);
            ActionBar.RemoveTab(ActionBar.SelectedTab);
            //also do cascade delete in db :-)
        }
        #endregion
    }
}