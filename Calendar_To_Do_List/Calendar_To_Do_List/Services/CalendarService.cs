using System;
using System.Collections.Generic;
using System.Text;
using Calendar_To_Do_List.Utilities.Helpers;
using Calendar_To_Do_List.Utilities.Interfaces;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.Serialization;

namespace Calendar_To_Do_List.Services;

public class CalendarService(ITodoService todoService) : ICalendarService
{
    private readonly ITodoService _todoService = todoService;
    private readonly CalendarSerializer _calendarSerializer = new();

    public void ExportToIcs(string path)
    {
        var Calendar = new Calendar();
        foreach (var todoTerm in _todoService.TodoTermCollection) Calendar.Todos.Add(todoTerm);
        var s = _calendarSerializer.SerializeToString(Calendar);
        ConfigurationHelper.SaveConfiguration(s, path);
    }
}
