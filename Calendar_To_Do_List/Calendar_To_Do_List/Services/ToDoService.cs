using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using Calendar_To_Do_List.Utilities.DataType;
using Calendar_To_Do_List.Utilities.Helpers;
using Calendar_To_Do_List.Utilities.Interfaces;
using Newtonsoft.Json;

namespace Calendar_To_Do_List.Services;

public class TodoService : ITodoService
{
    private static string FilePath => Path.Combine(IApp.DataDir, "todos.ctdltd");
    public ObservableCollection<TodoTerm> TodoTermCollection { get; set; } = [];

    public TodoService()
    {
        LoadTodos();
        TodoTermCollection.CollectionChanged += SaveTodos;
    }

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

    private void SaveTodos(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var c = JsonConvert.SerializeObject(TodoTermCollection);
        ConfigurationHelper.SaveConfiguration(c, FilePath);
    }

    private void LoadTodos()
    {
        if (File.Exists(FilePath))
        {
            var s = ConfigurationHelper.LoadConfiguration(FilePath);
            var todos = JsonConvert.DeserializeObject<ObservableCollection<TodoTerm>>(s);
            if (todos is not null)
            {
                TodoTermCollection.Clear();
                foreach (var t in todos)
                {
                    TodoTermCollection.Add(t);
                }
            }
        }
    }
}
