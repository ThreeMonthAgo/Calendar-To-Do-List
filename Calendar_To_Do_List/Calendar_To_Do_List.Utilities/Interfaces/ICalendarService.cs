using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar_To_Do_List.Utilities.Interfaces;

public interface ICalendarService
{
    public void ExportToIcs(string path);
}
