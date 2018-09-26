using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFluity.Office_Space_Classes
{
    public class OfficeItem
    {

        public OfficeItem(string title, string description, int flatRatePerHour)
        {
            _title = title;
            _description = description;
            _flatRatePerHour = flatRatePerHour;
            _parentType = ParentType.CompanyLocation;
        }

        public OfficeItem(string title, string description)
        {
            _title = title;
            _description = description;
            _flatRatePerHour = 0;
            _parentType = ParentType.OfficeSpace;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private int _flatRatePerHour;
        public int FlatRatePerHour
        {
            get { return _flatRatePerHour; }
            set { _flatRatePerHour = value; }
        }

        public enum ParentType : int
        {
            OfficeSpace = 0,
            CompanyLocation = 1
        }
        private ParentType _parentType;
        public ParentType ParentOfficeType
        {
            get { return _parentType; }
            set { _parentType = value; }
        }
    }
}
