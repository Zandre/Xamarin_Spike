using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public class OfficeHour
    {
        public OfficeHour(DayOfWeek day, bool isOpen, DateTime openingHour, DateTime closingHour)
        {
            _dayOfWeek = day;
            _isOpen = isOpen;
            _openingHour = openingHour;
            _closingHour = closingHour;
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }

        private DateTime _openingHour;
        public DateTime OpeningHour
        {
            get { return _openingHour; }
            set { _openingHour = value; }
        }

        private DateTime _closingHour;
        public DateTime ClosingHour
        {
            get { return _closingHour; }
            set { _closingHour = value; }
        }

        public enum DayOfWeek : byte { Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7 }
        private DayOfWeek _dayOfWeek;
        public DayOfWeek DayOfWeekEnum
        {
            get { return _dayOfWeek; }
            set { _dayOfWeek = value; }
        }
    }
}
