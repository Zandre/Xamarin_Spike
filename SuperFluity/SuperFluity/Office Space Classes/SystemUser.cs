using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public class SystemUser
    { 
        public SystemUser(string email, string firstName, string lastName, string contactNumber, string password)
        {
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _contactNumber = contactNumber;
            _password = password;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _contactNumber;
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private List<Company> _userCompanies = new List<Company>();
        public List<Company> UserCompanies
        {
            get
            {
                return _userCompanies;
            }
            set
            {
                _userCompanies = value;
            }
        }

        public void AddUserCompany(Company newCompany)
        {
            _userCompanies.Add(newCompany);
        }

        private List<Booking> _userBookings = new List<Booking>();
        public List<Booking> UserBookings
        {
            get
            {
                List<Booking> sortedBookings = new List<Booking>(_userBookings.OrderBy(x => x.StartTime));
                return sortedBookings;
            }
            set
            {
                _userBookings = value;
            }
        }
        public void AddUserBooking(Booking newBooking)
        {
            _userBookings.Add(newBooking);        }
    }
}
