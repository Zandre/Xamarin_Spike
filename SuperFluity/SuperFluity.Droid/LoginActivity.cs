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
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        Button login;
        Button cancel;
        Button register;
        EditText etEmail, etPassword;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            this.ActionBar.Hide();

            login = FindViewById<Button>(Resource.Id.btn_loginLogin);
            login.Click += Login_Click;
            register = FindViewById<Button>(Resource.Id.btn_loginRegister);
            register.Click += Register_Click;
            cancel = FindViewById<Button>(Resource.Id.btn_loginCancel);
            cancel.Click += Cancel_Click;

            etEmail = FindViewById<EditText>(Resource.Id.et_loginEmail);
            etPassword = FindViewById<EditText>(Resource.Id.et_loginPassword);

            if(CurrentSession.UserIsLoggedOn())
            {
                etEmail.Text = CurrentSession.LoggedOnUser.Email;
                etPassword.Text = CurrentSession.LoggedOnUser.Password;
                register.Text = "Update Details";
                login.Text = "Log out";
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (resultCode)
            {
                case Result.Canceled:
                    break;
                case Result.Ok:
                    SetResult(Result.Ok);
                    Finish();
                    break;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {            
            SetResult(Result.Canceled);
            Finish();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            StartActivityForResult(new Intent(this, typeof(RegisterActivity)), 0);
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if(CurrentSession.UserIsLoggedOn())
            {
                CurrentSession.LogOut();
                SetResult(Result.Ok);
                Finish();
            }
            else
            {
                if (Validate())
                {
                    SetResult(Result.Ok);
                    Finish();
                }
            }
        }

        #region Validate Login
        private bool Validate()
        {
            //cascade :-)
            return (ValidateEmail());
        }

        private bool ValidateEmail()
        {
            if (etEmail.Text != null && etEmail.Text.Length > 0)
            {
                if(CurrentSession.ValidUsers.Exists(user => user.Email == etEmail.Text))
                {
                    SystemUser selectedUser = CurrentSession.ValidUsers.FirstOrDefault(user => user.Email == etEmail.Text);
                    return ValidatePassword(selectedUser);
                }
                else
                {
                    Toast.MakeText(this, "Unrecognized Email Address", ToastLength.Short).Show();
                    return false;
                }
            }
            Toast.MakeText(this, "Unrecognized Email Address", ToastLength.Short).Show();
            return false;
        }

        private bool ValidatePassword(SystemUser selectedUser)
        {
            if (etPassword.Text != null && etPassword.Text.Length > 0)
            {
                if (selectedUser.Password == etPassword.Text)
                {
                    CurrentSession.LoggedOnUser = selectedUser;
                    return true;
                }
            }
            Toast.MakeText(this, "Incorrect Password", ToastLength.Short).Show();
            return false;
        }
        #endregion
    }
}