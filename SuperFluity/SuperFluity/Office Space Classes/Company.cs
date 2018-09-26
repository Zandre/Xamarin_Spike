
using Android.Graphics;
using SuperFluity.Office_Space_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity
{
    public class Company
    {
        public Company(string title, Bitmap logo)
        {
            this._title = title;
            this._logo = logo;
        }

        public Company(string title)
        {
            this._title = title;
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private Bitmap _logo;
        public Bitmap Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        private List<Office_Space_Classes.CompanyLocation> _companyLocations = new List<Office_Space_Classes.CompanyLocation>();
        public List<Office_Space_Classes.CompanyLocation> CompanyLocations
        {
            get { return _companyLocations; }
            set { _companyLocations = value; }
        }

        public void AddCompanyLocation(Office_Space_Classes.CompanyLocation locationToAdd)
        {
            _companyLocations.Add(locationToAdd);
        }
    }
}
