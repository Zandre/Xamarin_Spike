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

namespace SuperFluity.Droid.Fragments
{
    public class Fragment_EditOfficeHours : Fragment, TimePickerDialog.IOnTimeSetListener
    {
        #region Controls
        Button btnMondayOpen;
        Button btnMondayClose;
        Button btnTuesdayOpen;
        Button btnTuesdayClose;
        Button btnWednesdayOpen;
        Button btnWednesdayClose;
        Button btnThursdayOpen;
        Button btnThursdayClose;
        Button btnFridayOpen;
        Button btnFridayClose;
        Button btnSaturdayOpen;
        Button btnSaturdayClose;
        Button btnSundayOpen;
        Button btnSundayClose;
                Button selectedButton;

        Button btnSave;

        CheckBox chkMonday;
        CheckBox chkTuesday;
        CheckBox chkWednesday;
        CheckBox chkThursday;
        CheckBox chkFriday;
        CheckBox chkSaturday;
        CheckBox chkSunday;

        DateTime dtMondayOpen;
        DateTime dtMondayClose;
        DateTime dtTuesdayOpen;
        DateTime dtTuesdayClose;
        DateTime dtWednesdayOpen;
        DateTime dtWednesdayClose;
        DateTime dtThursdayOpen;
        DateTime dtThursdayClose;
        DateTime dtFridayOpen;
        DateTime dtFridayClose;
        DateTime dtSaturdayOpen;
        DateTime dtSaturdayClose;
        DateTime dtSundayOpen;
        DateTime dtSundayClose;
                DateTime selectedDT;
        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Fragment_OfficeHours, container, false);

            btnMondayOpen = rootView.FindViewById<Button>(Resource.Id.btnMondayOpen);
            btnMondayOpen.Click += ShowTimePicker;
            btnMondayClose = rootView.FindViewById<Button>(Resource.Id.btnMondayClose);
            btnMondayClose.Click += ShowTimePicker;
            btnTuesdayOpen = rootView.FindViewById<Button>(Resource.Id.btnTeusdayOpen);
            btnTuesdayOpen.Click += ShowTimePicker;
            btnTuesdayClose = rootView.FindViewById<Button>(Resource.Id.btnTuesdayClose);
            btnTuesdayClose.Click += ShowTimePicker;
            btnWednesdayOpen = rootView.FindViewById<Button>(Resource.Id.btnWednesdayOpen);
            btnWednesdayOpen.Click += ShowTimePicker;
            btnWednesdayClose = rootView.FindViewById<Button>(Resource.Id.btnWednesdayClose);
            btnWednesdayClose.Click += ShowTimePicker;
            btnThursdayOpen = rootView.FindViewById<Button>(Resource.Id.btnThursdayOpen);
            btnThursdayOpen.Click += ShowTimePicker;
            btnThursdayClose = rootView.FindViewById<Button>(Resource.Id.btnThursdayClose);
            btnThursdayClose.Click += ShowTimePicker;
            btnFridayOpen = rootView.FindViewById<Button>(Resource.Id.btnFridayOpen);
            btnFridayOpen.Click += ShowTimePicker;
            btnFridayClose = rootView.FindViewById<Button>(Resource.Id.btnFridayClose);
            btnFridayClose.Click += ShowTimePicker;
            btnSaturdayOpen = rootView.FindViewById<Button>(Resource.Id.btnSaturdayOpen);
            btnSaturdayOpen.Click += ShowTimePicker;
            btnSaturdayClose = rootView.FindViewById<Button>(Resource.Id.btnSaturdayClose);
            btnSaturdayClose.Click += ShowTimePicker;
            btnSundayOpen = rootView.FindViewById<Button>(Resource.Id.btnSundayOpen);
            btnSundayOpen.Click += ShowTimePicker;
            btnSundayClose = rootView.FindViewById<Button>(Resource.Id.btnSundayClose);
            btnSundayClose.Click += ShowTimePicker;

            dtMondayOpen = CurrentSession.SelectedCompanyLocation.Monday.OpeningHour;
            dtTuesdayOpen = CurrentSession.SelectedCompanyLocation.Tuesday.OpeningHour;
            dtWednesdayOpen = CurrentSession.SelectedCompanyLocation.Wednesday.OpeningHour;
            dtThursdayOpen = CurrentSession.SelectedCompanyLocation.Thursday.OpeningHour; 
            dtFridayOpen = CurrentSession.SelectedCompanyLocation.Friday.OpeningHour;
            dtSaturdayOpen = CurrentSession.SelectedCompanyLocation.Saturday.OpeningHour;
            dtSundayOpen = CurrentSession.SelectedCompanyLocation.Sunday.OpeningHour;

            dtMondayClose = CurrentSession.SelectedCompanyLocation.Monday.ClosingHour;
            dtTuesdayClose = CurrentSession.SelectedCompanyLocation.Tuesday.ClosingHour;
            dtWednesdayClose = CurrentSession.SelectedCompanyLocation.Wednesday.ClosingHour;
            dtThursdayClose = CurrentSession.SelectedCompanyLocation.Thursday.ClosingHour;
            dtFridayClose = CurrentSession.SelectedCompanyLocation.Friday.ClosingHour;
            dtSaturdayClose = CurrentSession.SelectedCompanyLocation.Saturday.ClosingHour;
            dtSundayClose = CurrentSession.SelectedCompanyLocation.Sunday.ClosingHour;

            SetButtonText(btnMondayOpen, dtMondayOpen, "Opening Hour");
            SetButtonText(btnMondayClose, dtMondayClose, "Closing Hour");
            SetButtonText(btnTuesdayOpen, dtTuesdayOpen, "Opening Hour");
            SetButtonText(btnTuesdayClose, dtTuesdayClose, "Closing Hour");
            SetButtonText(btnWednesdayOpen, dtWednesdayOpen, "Opening Hour");
            SetButtonText(btnWednesdayClose, dtWednesdayClose, "Closing Hour");
            SetButtonText(btnThursdayOpen, dtThursdayOpen, "Opening Hour");
            SetButtonText(btnThursdayClose, dtThursdayClose, "Closing Hour");
            SetButtonText(btnFridayOpen, dtFridayOpen, "Opening Hour");
            SetButtonText(btnFridayClose, dtFridayClose, "Closing Hour");
            SetButtonText(btnSaturdayOpen, dtSaturdayOpen, "Opening Hour");
            SetButtonText(btnSaturdayClose, dtSaturdayClose, "Closing Hour");
            SetButtonText(btnSundayOpen, dtSundayOpen, "Opening Hour");
            SetButtonText(btnSundayClose, dtSundayClose, "Closing Hour");

            chkMonday = rootView.FindViewById<CheckBox>(Resource.Id.chkMonday);
            chkMonday.Checked = CurrentSession.SelectedCompanyLocation.Monday.IsOpen;
            chkMonday.CheckedChange += Day_CheckChanged;
            chkTuesday = rootView.FindViewById<CheckBox>(Resource.Id.chkTuesday);
            chkTuesday.Checked = CurrentSession.SelectedCompanyLocation.Tuesday.IsOpen;
            chkTuesday.CheckedChange += Day_CheckChanged;
            chkWednesday = rootView.FindViewById<CheckBox>(Resource.Id.chkWednesday);
            chkWednesday.Checked = CurrentSession.SelectedCompanyLocation.Wednesday.IsOpen;
            chkWednesday.CheckedChange += Day_CheckChanged;
            chkThursday = rootView.FindViewById<CheckBox>(Resource.Id.chkThursday);
            chkThursday.Checked = CurrentSession.SelectedCompanyLocation.Thursday.IsOpen;
            chkThursday.CheckedChange += Day_CheckChanged;
            chkFriday = rootView.FindViewById<CheckBox>(Resource.Id.chkFriday);
            chkFriday.Checked = CurrentSession.SelectedCompanyLocation.Friday.IsOpen;
            chkFriday.CheckedChange += Day_CheckChanged;
            chkSaturday = rootView.FindViewById<CheckBox>(Resource.Id.chkSaturday);
            chkSaturday.Checked = CurrentSession.SelectedCompanyLocation.Saturday.IsOpen;
            chkSaturday.CheckedChange += Day_CheckChanged;
            chkSunday = rootView.FindViewById<CheckBox>(Resource.Id.chkSunday);
            chkSunday.Checked = CurrentSession.SelectedCompanyLocation.Sunday.IsOpen;
            chkSunday.CheckedChange += Day_CheckChanged;

            btnSave = rootView.FindViewById<Button>(Resource.Id.btnSaveOfficeHours);
            btnSave.Click += BtnSave_Click;

            return rootView;
        }

        private void ShowTimePicker(object sender, EventArgs e)
        {
            TimePickerDialog tp1 = new TimePickerDialog(this.Activity, this, 8, 0, true);
            switch (((Button)sender).Tag.ToString())
            {
                case "MondayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtMondayOpen.Hour, dtMondayOpen.Minute, true);
                    tp1.SetTitle("Monday - Opening Time");
                    selectedButton = btnMondayOpen;
                    sdt = SelectedDateTime.MondayOpen;
                    break;
                case "MondayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtMondayClose.Hour, dtMondayClose.Minute, true);
                    tp1.SetTitle("Monday - Closing Time");
                    selectedButton = btnMondayClose;
                    sdt = SelectedDateTime.MondayClose;
                    break;

                case "TuesdayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtTuesdayOpen.Hour, dtTuesdayOpen.Minute, true);
                    tp1.SetTitle("Tuesday - Opening Time");
                    selectedButton = btnTuesdayOpen;
                    sdt = SelectedDateTime.TuesdayOpen;
                    break;
                case "TuesdayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtTuesdayClose.Hour, dtTuesdayClose.Minute, true);
                    tp1.SetTitle("Tuesday - Closing Time");
                    selectedButton = btnTuesdayClose;
                    sdt = SelectedDateTime.TuesdayClose;
                    break;

                case "WednesdayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtWednesdayOpen.Hour, dtWednesdayClose.Minute, true);
                    tp1.SetTitle("Wednesday - Opening Time");
                    selectedButton = btnWednesdayOpen;
                    sdt = SelectedDateTime.WednesdayOpen;
                    break;
                case "WednesdayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtWednesdayClose.Hour, dtWednesdayClose.Minute, true);
                    tp1.SetTitle("Wednesday - Closing Time");
                    selectedButton = btnWednesdayClose;
                    sdt = SelectedDateTime.WednesdayClose;
                    break;

                case "ThursdayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtThursdayOpen.Hour, dtThursdayOpen.Minute, true);
                    tp1.SetTitle("Thursday - Opening Time");
                    selectedButton = btnThursdayOpen;
                    sdt = SelectedDateTime.ThursdayOpen;
                    break;
                case "ThursdayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtThursdayClose.Hour, dtThursdayClose.Minute, true);
                    tp1.SetTitle("Thursday - Closing Time");
                    selectedButton = btnThursdayClose;
                    sdt = SelectedDateTime.ThursdayClose;
                    break;

                case "FridayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtFridayOpen.Hour, dtFridayOpen.Minute, true);
                    tp1.SetTitle("Friday - Opening Time");
                    selectedButton = btnFridayOpen;
                    sdt = SelectedDateTime.FridayOpen;
                    break;
                case "FridayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtFridayClose.Hour, dtFridayClose.Minute, true);
                    tp1.SetTitle("Friday - Closing Time");
                    selectedButton = btnFridayClose;
                    sdt = SelectedDateTime.FridayClose;
                    break;

                case "SaturdayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtSaturdayOpen.Hour, dtSaturdayOpen.Minute, true);
                    tp1.SetTitle("Saturday - Opening Time");
                    selectedButton = btnSaturdayOpen;
                    sdt = SelectedDateTime.SaturdayOpen;
                    break;
                case "SaturdayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtSaturdayClose.Hour, dtSaturdayClose.Minute, true);
                    tp1.SetTitle("Saturday - Closing Time");
                    selectedButton = btnSaturdayClose;
                    sdt = SelectedDateTime.SaturdayClose;
                    break;

                case "SundayOpen":
                    tp1 = new TimePickerDialog(this.Activity, this, dtSundayOpen.Hour, dtSundayOpen.Minute, true);
                    tp1.SetTitle("Sunday - Opening Time");
                    selectedButton = btnSundayOpen;
                    sdt = SelectedDateTime.SundayOpen;
                    break;
                case "SundayClose":
                    tp1 = new TimePickerDialog(this.Activity, this, dtSundayClose.Hour, dtSundayClose.Minute, true);
                    tp1.SetTitle("Sunday - Closing Time");
                    selectedButton = btnSundayClose;
                    sdt = SelectedDateTime.SundayClose;
                    break;
            }
            tp1.Show();
        }

        public void OnTimeSet(TimePicker view, int hour, int minute)
        {
            //selectedDT = new DateTime(1900, 1, 1, hour, minute, 0);

            switch (sdt)
            {
                case SelectedDateTime.MondayOpen:
                    dtMondayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.MondayClose:
                    dtMondayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.TuesdayOpen:
                    dtTuesdayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.TuesdayClose:
                    dtTuesdayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.WednesdayOpen:
                    dtWednesdayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.WednesdayClose:
                    dtWednesdayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.ThursdayOpen:
                    dtThursdayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.ThursdayClose:
                    dtThursdayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.FridayOpen:
                    dtFridayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.FridayClose:
                    dtFridayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.SaturdayOpen:
                    dtSaturdayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.SaturdayClose:
                    dtSaturdayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.SundayOpen:
                    dtSundayOpen = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
                case SelectedDateTime.SundayClose:
                    dtSundayClose = new DateTime(1900, 1, 1, hour, minute, 0);
                    break;
            }


            if (selectedButton.Tag.ToString().Contains("Open"))
            {
                SetButtonText(selectedButton, selectedDT, "Opening Hour");
            }
            else
            {
                SetButtonText(selectedButton, selectedDT, "Closing Hour");
            }
        }

        private void Day_CheckChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            switch (((CheckBox)sender).Tag.ToString())
            {
                case "Monday":
                    btnMondayOpen.Enabled = chk.Checked;
                    btnMondayClose.Enabled = chk.Checked;
                    break;

                case "Tuesday":
                    btnTuesdayOpen.Enabled = chk.Checked;
                    btnTuesdayClose.Enabled = chk.Checked;
                    break;

                case "Wednesday":
                    btnWednesdayOpen.Enabled = chk.Checked;
                    btnWednesdayClose.Enabled = chk.Checked;
                    break;

                case "Thursday":
                    btnThursdayOpen.Enabled = chk.Checked;
                    btnThursdayClose.Enabled = chk.Checked;
                    break;

                case "Friday":
                    btnFridayOpen.Enabled = chk.Checked;
                    btnFridayClose.Enabled = chk.Checked;
                    break;

                case "Saturday":
                    btnSaturdayOpen.Enabled = chk.Checked;
                    btnSaturdayClose.Enabled = chk.Checked;
                    break;

                case "Sunday":
                    btnSundayOpen.Enabled = chk.Checked;
                    btnSundayClose.Enabled = chk.Checked;
                    break;
            }
        }

        private void SetButtonText(Button btn, DateTime dt, string precedingText)
        {
            btn.Text = precedingText + " - " + dt.ToShortTimeString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                CurrentSession.SelectedCompanyLocation.Monday = (new OfficeHour(OfficeHour.DayOfWeek.Monday, chkMonday.Checked, dtMondayOpen, dtMondayClose));
                CurrentSession.SelectedCompanyLocation.Tuesday = (new OfficeHour(OfficeHour.DayOfWeek.Tuesday, chkTuesday.Checked, dtTuesdayOpen, dtTuesdayClose));
                CurrentSession.SelectedCompanyLocation.Wednesday = (new OfficeHour(OfficeHour.DayOfWeek.Wednesday, chkWednesday.Checked, dtWednesdayOpen, dtWednesdayClose));
                CurrentSession.SelectedCompanyLocation.Thursday = (new OfficeHour(OfficeHour.DayOfWeek.Thursday, chkThursday.Checked, dtThursdayClose, dtThursdayClose));
                CurrentSession.SelectedCompanyLocation.Friday = (new OfficeHour(OfficeHour.DayOfWeek.Friday, chkFriday.Checked, dtFridayOpen, dtFridayClose));
                CurrentSession.SelectedCompanyLocation.Saturday = (new OfficeHour(OfficeHour.DayOfWeek.Saturday, chkSaturday.Checked, dtSaturdayOpen, dtSaturdayClose));
                CurrentSession.SelectedCompanyLocation.Sunday = (new OfficeHour(OfficeHour.DayOfWeek.Sunday, chkSunday.Checked, dtSundayOpen, dtSundayClose));

                Toast.MakeText(this.Activity, "Office Hours Saved Successfully", ToastLength.Short).Show();

                Fragment_CompanyLocationDetail _companyDetails = new Fragment_CompanyLocationDetail();
                var ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.locationdetail_fragmentframe, _companyDetails);
                ft.Commit();
            }
        }

        private bool Validate()
        {
            if (chkMonday.Checked)
            {
                int result = DateTime.Compare(dtMondayOpen, dtMondayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Monday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkTuesday.Checked)
            {
                int result = DateTime.Compare(dtTuesdayOpen, dtTuesdayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Tuesday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkWednesday.Checked)
            {
                int result = DateTime.Compare(dtWednesdayOpen, dtWednesdayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Wednesday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkThursday.Checked)
            {
                int result = DateTime.Compare(dtThursdayOpen, dtThursdayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Thursday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkFriday.Checked)
            {
                int result = DateTime.Compare(dtFridayOpen, dtFridayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Friday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkSaturday.Checked)
            {
                int result = DateTime.Compare(dtSaturdayOpen, dtSaturdayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Saturday", ToastLength.Short).Show();
                    return false;
                }
            }

            if (chkSunday.Checked)
            {
                int result = DateTime.Compare(dtSundayOpen, dtSundayClose);
                if (result > 0 || result == 0)
                {
                    Toast.MakeText(this.Activity, "Invalid Office Hours - Sunday", ToastLength.Short).Show();
                    return false;
                }
            }

            return true;
        }

        private SelectedDateTime sdt = SelectedDateTime.MondayOpen;
        public enum SelectedDateTime { MondayOpen, MondayClose, TuesdayOpen, TuesdayClose, WednesdayOpen, WednesdayClose, ThursdayOpen, ThursdayClose, FridayOpen, FridayClose, SaturdayOpen, SaturdayClose, SundayOpen, SundayClose };
    }
}