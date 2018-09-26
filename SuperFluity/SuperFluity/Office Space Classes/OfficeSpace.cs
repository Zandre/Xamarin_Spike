using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public class OfficeSpace
    {
        public OfficeSpace(string title, int roomSize, int maxPeople, int minPeople, int flatRate, string description, OfficeType type)
        {
            _title = title;
            _roomSize = roomSize;
            _maxPeople = maxPeople;
            _minPeople = minPeople;
            _flatRate = flatRate;
            _description = description;
            _type = type;
        }

        public OfficeSpace()
        {

        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private int _roomSize;
        public int RoomSize
        {
            get { return _roomSize; }
            set { _roomSize = value; }
        }

        private int _maxPeople;
        public int MaximumPeople
        {
            get { return _maxPeople; }
            set { _maxPeople = value; }
        }

        private int _minPeople;
        public int MinimumPeople
        {
            get { return _minPeople; }
            set { _minPeople = value; }
        }

        private int _flatRate;
        public int FlatRatePerHour
        {
            get { return _flatRate; }
            set { _flatRate = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public enum OfficeType : int
        {   OpenOffice = 0,
            Cubicle = 1,
            PrivateOffice = 2,
            SharedOffice = 3,
            TeamRoom = 4,
            StudyBooth = 5,
            WorkLounge = 6,
            SmallMeetingRoom = 7,
            LargeMeetingRoom = 8,
            LargeMeetingSpace = 9,
            Auditorium = 10,
            BrainstormRoom = 11
        }
        private OfficeType _type;
        public OfficeType OfficeTypeEnum
        {
            get { return _type; }
            set { _type = value; }
        }

        public string FriendlyOfficeTypeEnum
        {
            get
            {
                string type = "";
                switch (OfficeTypeEnum)
                {
                    case OfficeType.OpenOffice:
                        type = "Open Office";
                        break;

                    case OfficeType.Cubicle:
                        type = "Cubicle";
                        break;

                    case OfficeType.PrivateOffice:
                        type = "Private Office";
                        break;

                    case OfficeType.SharedOffice:
                        type = "Shared Office";
                        break;

                    case OfficeType.TeamRoom:
                        type = "Team Room";
                        break;

                    case OfficeType.StudyBooth:
                        type = "Study Booth";
                        break;

                    case OfficeType.WorkLounge:
                        type = "Work Lounge";
                        break;

                    case OfficeType.SmallMeetingRoom:
                        type = "Small Meeting Room";
                        break;

                    case OfficeType.LargeMeetingRoom:
                        type = "Large Meeting Room";
                        break;

                    case OfficeType.LargeMeetingSpace:
                        type = "Large Meeting Space";
                        break;

                    case OfficeType.Auditorium:
                        type = "Auditorium";
                        break;

                    case OfficeType.BrainstormRoom:
                        type = "Brainstorm Room";
                        break;                        
                }
                return type;
            }
        }

        public static List<string> OfficeTypes
        {
            get
            {
                List<string> officeTypes = new List<string>();
                officeTypes.Add("Open Office");
                officeTypes.Add("Cubicle");
                officeTypes.Add("Private Office");
                officeTypes.Add("Shared Office");
                officeTypes.Add("Team Room");
                officeTypes.Add("Study Booth");
                officeTypes.Add("Work Lounge");
                officeTypes.Add("Small Meeting Room");
                officeTypes.Add("Large Meeting Room");
                officeTypes.Add("Large Meeting Space");
                officeTypes.Add("Auditorium");
                officeTypes.Add("Brainstorm Room");
                return officeTypes;
            }
        }

        private List<OfficeItem> _officeItems = new List<OfficeItem>();
        public List<OfficeItem> OfficeItems
        {
            get { return _officeItems; }
            set { _officeItems = value; }
        }
        public void AddOfficeItem(OfficeItem officeItem)
        {
            _officeItems.Add(officeItem);
        }


    }
}
