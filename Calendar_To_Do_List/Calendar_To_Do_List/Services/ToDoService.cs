using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using Calendar_To_Do_List.Utilities.Interfaces;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.Services;

public class ToDoService : IToDoService
{
    //                Uid     Todo
    public Dictionary<string, Todo> TodoDictionary = [];

    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate)
    {
        Todo t = new()
        {
            Summary = summary,
            Description = description,
            Priority = priority,
            Due = dueDate
        };
        TodoDictionary.Add(t.Uid, t);
        return t;
    }

    public void CompleteToDo(string Uid, CalDateTime? deadLine = null)
    {
        if (!TodoDictionary.TryGetValue(Uid, out Todo t)) return;
        CompleteToDo(t, deadLine);
    }

    public void CompleteToDo(Todo todo, CalDateTime? deadLine = null)
    {
        todo.Completed = new(deadLine ?? CalDateTime.Now);
    }
}
