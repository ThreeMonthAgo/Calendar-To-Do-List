using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Calendar_To_Do_List.Utilities.Helpers;
using Calendar_To_Do_List.Utilities.Interfaces;
using Ical.Net;
using Ical.Net.Serialization;

namespace Calendar_To_Do_List.Services
{
    public class CalendarService : ICalendarService
    {
        public Calendar Calendar { get; set; } = new();

        public void Export(string path)
        {
            var serializer = new CalendarSerializer();
            var s = serializer.SerializeToString(Calendar);
            if (s is null) throw new SerializationException();
            else ConfigurationHelper.SaveConfiguration(s, path);
        }
    }
}
