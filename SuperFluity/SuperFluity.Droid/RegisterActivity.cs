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
using System.Text.RegularExpressions;
using SuperFluity.Office_Space_Classes;

namespace SuperFluity.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        Button registerUser;
        EditText etFirstName, etLastName, etEmail, etContactNumber, etPassword;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            this.ActionBar.Hide();

            registerUser = FindViewById<Button>(Resource.Id.btn_RegisterUser);
            registerUser.Click += RegisterUser_Click;

            etFirstName = FindViewById<EditText>(Resource.Id.et_RegisterFirstName);
            etLastName = FindViewById<EditText>(Resource.Id.et_RegisterLastName);
            etEmail = FindViewById<EditText>(Resource.Id.et_RegisterEmail);
            etContactNumber = FindViewById<EditText>(Resource.Id.et_RegisterContactNumber);
            etPassword = FindViewById<EditText>(Resource.Id.et_RegisterPassword);

            if(CurrentSession.UserIsLoggedOn())
            {
                etFirstName.Text = CurrentSession.LoggedOnUser.FirstName;
                etLastName.Text = CurrentSession.LoggedOnUser.LastName;
                etEmail.Text = CurrentSession.LoggedOnUser.Email;
                etContactNumber.Text = CurrentSession.LoggedOnUser.ContactNumber;
                etPassword.Text = CurrentSession.LoggedOnUser.Password;

                registerUser.Text = "Update";
            }
        }

        private void RegisterUser_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                //register user to online database goes here
                if (!CurrentSession.UserIsLoggedOn())
                {
                    SystemUser newUser = new SystemUser(etEmail.Text, etFirstName.Text, etLastName.Text, etContactNumber.Text, etPassword.Text);
                    CurrentSession.AddValidUser(newUser);
                }
                else
                {
                    SystemUser updatedUser = new SystemUser(etEmail.Text, etFirstName.Text, etLastName.Text, etContactNumber.Text, etPassword.Text);
                    CurrentSession.UpdateLoggedOnUser(updatedUser);
                }
                SetResult(Result.Ok);
                Finish();
            }
        }

        #region Validate
        private bool Validate()
        {
            return (ValidateFirstName() && ValidateLastName() && ValidateEmail() && ValidateContactNumber() && ValidatePassword());
        }

        private bool ValidateFirstName()
        {
            if (etFirstName.Text != null && etFirstName.Text.Length > 0)
            {
                return true;
            }
            Toast.MakeText(this, "nvalid First Name", ToastLength.Short).Show();
            return false;
        }

        private bool ValidateLastName()
        {
            if (etLastName.Text != null && etLastName.Text.Length > 0)
            {
                return true;
            }
            Toast.MakeText(this, "nvalid Last Name", ToastLength.Short).Show();
            return false;
        }

        private bool ValidateEmail()
        {
            //http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address

            var addr = new System.Net.Mail.MailAddress(etEmail.Text);
            if (addr.Address != etEmail.Text)
            {
                Toast.MakeText(this, "Invalid Email Address", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidateContactNumber()
        {
            if (!Regex.Match(etContactNumber.Text, @"^(\d{10})$").Success)
            {
                Toast.MakeText(this, "Invalid Contact Number", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool ValidatePassword()
        {
            if (etPassword.Text != null && etPassword.Text.Length > 0)
            {
                return true;
            }
            Toast.MakeText(this, "nvalid Password", ToastLength.Short).Show();
            return false;
        }
        #endregion
    }
}