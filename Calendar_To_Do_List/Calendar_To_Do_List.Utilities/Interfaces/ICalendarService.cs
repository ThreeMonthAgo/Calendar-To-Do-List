using System;
using System.Collections.Generic;
using System.Text;
using Ical.Net;

namespace Calendar_To_Do_List.Utilities.Interfaces
{
    public interface ICalendarService
    {
        public Calendar Calendar { get; set; }

        /// <summary>
        /// path is the directory to export the calendar to, not including filename
        /// </summary>
        public void Export(string path);
    }
}
