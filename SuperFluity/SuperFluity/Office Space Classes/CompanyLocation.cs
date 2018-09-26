using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public class CompanyLocation
    {
        public CompanyLocation(string title,
                                double longitude,
                                double latitude,
                                string streetAddress,
                                string contactNumber,
                                string email,                                
                                Status status,
                                int rating)
        {
            this._title = title;
            this._longitude = longitude;
            this._latitude = latitude;
            this._streetAddress = streetAddress;
            this._contactNumber = contactNumber;
            this._email = email;
            this._status = status;
            this._rating = rating;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        #region GEO MAP STUFF
        //sub lat and long with proper Geo coordinate class
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        #endregion

        private string _streetAddress;
        public string StreetAddress
        {
            get { return _streetAddress; }
            set { _streetAddress = value; }
        }

        private string _contactNumber;
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private int _rating;

        public int Rating
        {
            get
            {
                return _rating;
            }
            set { _rating = value; }
        }

        public enum Status : byte { Active = 1, NotActive = 2 }
        private Status _status;
        public Status StatusEnum
        {
            get { return _status; }
            set { _status = value; }
        }

        private List<OfficeSpace> _officeSpaces = new List<OfficeSpace>();
        public List<OfficeSpace> OfficeSpaces
        {
            get { return _officeSpaces; }
            set { _officeSpaces = value; }
        }
        public void AddOfficeSpace(OfficeSpace SpaceToAdd)
        {
            _officeSpaces.Add(SpaceToAdd);
        }

        private List<OfficeItem> _companyLocationItems = new List<OfficeItem>();
        public List<OfficeItem> CompanyLocationItems
        {
            get { return _companyLocationItems; }
            set { _companyLocationItems = value; }
        }
        public void AddCompanyLocationItem(OfficeItem officeItem)
        {
            _companyLocationItems.Add(officeItem);
        }

        #region OfficeHours
        private OfficeHour _Monday = new OfficeHour(OfficeHour.DayOfWeek.Monday, true, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 17, 0, 0));
        private OfficeHour _Tuesday = new OfficeHour(OfficeHour.DayOfWeek.Tuesday, true, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 17, 0, 0));
        private OfficeHour _Wednesday = new OfficeHour(OfficeHour.DayOfWeek.Wednesday, true, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 17, 0, 0));
        private OfficeHour _Thursday = new OfficeHour(OfficeHour.DayOfWeek.Thursday, true, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 17, 0, 0));
        private OfficeHour _Friday = new OfficeHour(OfficeHour.DayOfWeek.Friday, true, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 16, 30, 0));
        private OfficeHour _Saturday = new OfficeHour(OfficeHour.DayOfWeek.Saturday, false, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 8, 0, 0));
        private OfficeHour _Sunday = new OfficeHour(OfficeHour.DayOfWeek.Sunday, false, new DateTime(1900, 01, 01, 8, 0, 0), new DateTime(1900, 01, 01, 8, 0, 0));

        public OfficeHour Monday
        {
            get{ return _Monday; }
            set { _Monday = value; }
        }

        public OfficeHour Tuesday
        {
            get { return _Tuesday; }
            set { _Tuesday = value; }
        }

        public OfficeHour Wednesday
        {
            get { return _Wednesday; }
            set { _Wednesday = value; }
        }

        public OfficeHour Thursday
        {
            get { return _Thursday; }
            set { _Thursday = value; }
        }

        public OfficeHour Friday
        {
            get { return _Friday; }
            set { _Friday = value; }
        }

        public OfficeHour Saturday
        {
            get { return _Saturday; }
            set { _Saturday = value; }
        }

        public OfficeHour Sunday
        {
            get { return _Sunday; }
            set { _Sunday = value; }
        }

        #endregion
    }
}
