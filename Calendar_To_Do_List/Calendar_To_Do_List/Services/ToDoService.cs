using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calendar_To_Do_List.Utilities.DataType;
using Calendar_To_Do_List.Utilities.Interfaces;

namespace Calendar_To_Do_List.Services;

public class TodoService : ITodoService
{
    public ObservableCollection<TodoTerm> TodoTermCollection { get; set; } = [];

    public TodoTerm CreateTodo(string? summary, string? description, int priority, DateTime? dueDate = null)
    {
        TodoTerm t = new()
        {
            Summary = summary ?? string.Empty,
            Description = description ?? string.Empty,
            Priority = priority,
            Due = dueDate
        };
        TodoTermCollection.Add(t);
        return t;
    }

    public void CompleteTodo(TodoTerm todo, DateTime? completedDate = null)
    {
        todo.IsCompleted = true;
        todo.CompletedTime = completedDate ?? DateTime.UtcNow;
    }

    public void DeleteTodo(TodoTerm todo)
    {
        TodoTermCollection.Remove(todo);
    }
}
