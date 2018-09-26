using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public static class CurrentSession
    {
        public static Company SelectedCompany
        {
            get;
            set;
        }

        public static CompanyLocation SelectedCompanyLocation
        {
            get;
            set;
        }

        public static OfficeSpace SelectedOfficeSpace
        {
            get;
            set;
        }

        public static OfficeItem SelectedOfficeItem
        {
            get;
            set;
        }

        public static SystemUser LoggedOnUser
        {
            get;
            set;
        }

        public static void LogOut()
        {
            LoggedOnUser = null;
        }

        public static bool UserIsLoggedOn()
        {
            if(LoggedOnUser != null)
            {
                return true;
            }
            return false;
        }

        //HARDCODE - remove this when db goes online
        private static List<SystemUser> _validUsers = new List<SystemUser>();
        public  static List<SystemUser> ValidUsers
        {
            get
            {
                if(_validUsers.Count == 0)
                {
                    SystemUser adminGuy = new SystemUser("admin@sf.co.za", "Admin", "User", "011", "admin");
                    Company randomBank = new Company("ABSA Bank");
                    CompanyLocation cl = new CompanyLocation("Sandton Square Branch", 28.054163, -26.106754, "Somewhere Super Close to Sandton", "0114445698", "someperson@bank.com", CompanyLocation.Status.Active, 2);

                    OfficeItem laptop1 = new OfficeItem("Acer Laptop", "i3, 2gb RAM, 250gb HD", 100);
                    OfficeItem laptop2 = new OfficeItem("Dell Laptop", "i5, 4gb RAM, 500 HD", 250);
                    OfficeItem laptop3 = new OfficeItem("Lenovo Laptop", "i7, 8gb RAM, 1tb HD", 500);

                    cl.AddCompanyLocationItem(laptop1);
                    cl.AddCompanyLocationItem(laptop2);
                    cl.AddCompanyLocationItem(laptop3);

                    OfficeSpace bigRoom = new OfficeSpace("Big Room", 50, 10, 1, 200, "A very nice, super big room. Almost like an auditorium", OfficeSpace.OfficeType.Auditorium);

                    OfficeItem fixedProjector = new OfficeItem("Fixed Projector", "A projector fixed to the ceiling");
                    OfficeItem computer = new OfficeItem("Computer", "A personal computer with internet access");
                    OfficeItem printer = new OfficeItem("Printer", "A colour printer");
                    OfficeItem soundbar = new OfficeItem("Soundbar", "A soundbar fixed to the ceiling");
                    bigRoom.AddOfficeItem(fixedProjector);
                    bigRoom.AddOfficeItem(computer);
                    bigRoom.AddOfficeItem(printer);
                    bigRoom.AddOfficeItem(soundbar);


                    OfficeSpace mediumRoom = new OfficeSpace("Medium Room", 30, 7, 1, 150, "A very nice, medium sized room", OfficeSpace.OfficeType.TeamRoom);
                    mediumRoom.AddOfficeItem(fixedProjector);
                    mediumRoom.AddOfficeItem(soundbar);


                    OfficeSpace smallRoom = new OfficeSpace("Small Room", 3, 10, 1, 70, "Just super small", OfficeSpace.OfficeType.StudyBooth);
                    smallRoom.AddOfficeItem(printer);

                    Booking awaitingPayment = new Booking(adminGuy, bigRoom, new DateTime(2014, 02, 15, 14, 0, 0), new DateTime(2017, 02, 15, 16, 0, 0), 1500, Booking.BookingStatusEnum.AwaitingPayment);
                    Booking confirmed = new Booking(adminGuy, mediumRoom, new DateTime(2017, 03, 19, 10, 0, 0), new DateTime(2017, 03, 19, 13, 0, 0), 600, Booking.BookingStatusEnum.Confirmed);
                    Booking confirmed1 = new Booking(adminGuy, bigRoom, new DateTime(2018, 08, 19, 10, 0, 0), new DateTime(2017, 03, 19, 13, 0, 0), 600, Booking.BookingStatusEnum.Confirmed);
                    Booking canceled = new Booking(adminGuy, smallRoom, new DateTime(2017, 02, 15, 14, 0, 0), new DateTime(2017, 02, 15, 14, 30, 0), 200, Booking.BookingStatusEnum.Cancelled);
                    Booking confirmed2 = new Booking(adminGuy, bigRoom, new DateTime(2019, 03, 19, 10, 0, 0), new DateTime(2017, 03, 19, 13, 0, 0), 600, Booking.BookingStatusEnum.Confirmed);
                    Booking awaitingPayment2 = new Booking(adminGuy, smallRoom, new DateTime(2017, 02, 15, 14, 0, 0), new DateTime(2017, 02, 15, 16, 0, 0), 1500, Booking.BookingStatusEnum.AwaitingPayment);
                    Booking canceled2 = new Booking(adminGuy, mediumRoom, new DateTime(2017, 09, 15, 14, 0, 0), new DateTime(2017, 02, 15, 14, 30, 0), 200, Booking.BookingStatusEnum.Cancelled);
                    adminGuy.AddUserBooking(awaitingPayment);                    
                    adminGuy.AddUserBooking(confirmed);
                    adminGuy.AddUserBooking(confirmed1);
                    adminGuy.AddUserBooking(awaitingPayment2);
                    adminGuy.AddUserBooking(canceled);
                    adminGuy.AddUserBooking(confirmed2);                    
                    adminGuy.AddUserBooking(canceled2);

                    cl.AddOfficeSpace(bigRoom);
                    cl.AddOfficeSpace(mediumRoom);
                    cl.AddOfficeSpace(smallRoom);
                    randomBank.AddCompanyLocation(cl);
                    randomBank.AddCompanyLocation(new CompanyLocation("Melrose Arch Branch", 28.067721, -26.132759, "Somewhere Super Close to Melrose", "0114445698", "someperson2@bank.com", CompanyLocation.Status.NotActive, 4));
                    randomBank.AddCompanyLocation(new CompanyLocation("Cape Town Branch", 18.477384, -33.947713, "Somewhere Super Close to Cape Town", "0114445698", "someperson2@bank.com", CompanyLocation.Status.Active, 1));
                    adminGuy.UserCompanies.Add(randomBank);
                    adminGuy.UserCompanies.Add(new Company("Fancy Law Firm"));
                    adminGuy.UserCompanies.Add(new Company("Some House Inc"));
                    _validUsers.Add(adminGuy);
                    SystemUser nonAdminGuy = new SystemUser("guy@sf.co.za", "Random", "User", "01", "random");
                    _validUsers.Add(nonAdminGuy);
                }                
                return _validUsers;
            }
        }
        
        public static void AddValidUser(SystemUser newUser)
        {
            _validUsers.Add(newUser);
            LoggedOnUser = newUser;
        }

        public static void UpdateLoggedOnUser(SystemUser updatedUser)
        {
            SystemUser selectedUser = _validUsers.FirstOrDefault(users => users.Email == LoggedOnUser.Email);
            if(selectedUser != null)
            {
                LoggedOnUser = updatedUser;

                //eventaully move this into a smarter method
                selectedUser.FirstName = updatedUser.FirstName;
                selectedUser.LastName = updatedUser.LastName;
                selectedUser.Email = updatedUser.Email;
                selectedUser.ContactNumber = updatedUser.ContactNumber;
                selectedUser.Password = updatedUser.Password;
            }
        }
    }
}
