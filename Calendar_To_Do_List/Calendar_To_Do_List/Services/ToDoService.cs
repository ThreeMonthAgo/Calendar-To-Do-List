using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calendar_To_Do_List.Utilities.Interfaces;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.Services;

public class ToDoService : ITodoService
{
    public ObservableCollection<Todo> TodoCollection { get; set; } = [];

    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate)
    {
        Todo t = new()
        {
            Summary = summary,
            Description = description,
            Priority = priority,
            Due = dueDate
        };
        TodoCollection.Add(t);
        return t;
    }

    public void CompleteToDo(Todo todo, CalDateTime? completeDate = null)
    {
        if (!TodoCollection.Contains(todo)) return;
        todo.Completed = new(completeDate ?? CalDateTime.UtcNow);
    }

    public void DeleteToDo(Todo todo)
    {
        if (!TodoCollection.Contains(todo)) return;
        TodoCollection.Remove(todo);
    }

    public void ExportToIcs()
    {
        // TODO
    }

    public ObservableCollection<Todo> GetTodos()
    {
        throw new NotImplementedException();
    }
}
