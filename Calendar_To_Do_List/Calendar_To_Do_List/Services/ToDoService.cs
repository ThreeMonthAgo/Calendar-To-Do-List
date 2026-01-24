using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calendar_To_Do_List.Utilities.Interfaces;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Proxies;
using Ical.Net.Serialization;

namespace Calendar_To_Do_List.Services;

public class TodoService(ICalendarService calendarService) : ITodoService
{
    private readonly ICalendarService _calendarService = calendarService;

    public Calendar Calendar => _calendarService.Calendar;

    public IUniqueComponentList<Todo> Todos
    {
        get => Calendar.Todos;
    }

    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate)
    {
        Todo t = new()
        {
            Summary = summary,
            Description = description,
            Priority = priority,
            Due = dueDate
        };
        Calendar.Todos.Add(t);
        return t;
    }

    public void CompleteToDo(Todo todo, CalDateTime? completeDate = null)
    {
        if (!Calendar.Todos.Contains(todo)) return;
        todo.Status = TodoStatus.Completed;
        todo.Completed = new(completeDate ?? CalDateTime.UtcNow);
    }

    public void DeleteToDo(Todo todo)
    {
        if (!Calendar.Todos.Contains(todo)) return;
        Calendar.Todos.Remove(todo);
    }
}
