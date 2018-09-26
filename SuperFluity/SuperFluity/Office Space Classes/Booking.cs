using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.Graphics;

namespace SuperFluity.Office_Space_Classes
{
    public class Booking
    {
        public Booking(SystemUser systemUser, OfficeSpace officeSpace, DateTime startTime, DateTime endTime, int price, BookingStatusEnum status)
        {
            _SystemUser = systemUser;
            _OfficeSpace = officeSpace;
            _StartTime = startTime;
            _EndTime = endTime;
            _price = price;
            _Status = status;
        }

        private SystemUser _SystemUser;
        public SystemUser SystemUser
        {
            get
            {
                return _SystemUser;
            }
            set
            {
                _SystemUser = value;
            }
        }

        private OfficeSpace _OfficeSpace;
        public OfficeSpace OfficeSpace
        {
            get
            {
                return _OfficeSpace;
            }
            set
            {
                _OfficeSpace = value;
            }
        }

        private DateTime _StartTime;
        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }

        public string FriendlyDate
        {
            get
            {
                string startDate = StartTime.ToString("dd MMMM yyyy");
                return startDate;
            }
        }

        public string FriendlyStartTime
        {
            get
            {
                string startTime = StartTime.ToString("HH:mm");
                return startTime;
            }
        }

        private DateTime _EndTime;
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }

        public string FriendlyEndTime
        {
            get
            {
                string endTime = EndTime.ToString("HH:mm");
                return endTime;
            }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public enum BookingStatusEnum : byte { AwaitingPayment = 1, Confirmed = 2, Cancelled = 3 }
        private BookingStatusEnum _Status;
        public BookingStatusEnum Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string FriendlyStatus
        {
            get
            {
                string status = "";
                switch (Status)
                {
                      case BookingStatusEnum.Confirmed:
                        status = "Booking Confirmed";
                        break;
                      case BookingStatusEnum.AwaitingPayment:
                        status = "Awaiting Payment";
                        break;
                      case BookingStatusEnum.Cancelled:
                        status = "Cancelled";
                        break;
                }
                return status;
            }
        }

        public Color StatusColor
        {
            get
            {
                switch (Status)
                {
                    case BookingStatusEnum.Confirmed:
                        return Color.Green;
                    case BookingStatusEnum.AwaitingPayment:
                        return Color.Orange;
                    case BookingStatusEnum.Cancelled:
                        return Color.Red;
                }
                return Color.White;
            }
        }
     }
}
