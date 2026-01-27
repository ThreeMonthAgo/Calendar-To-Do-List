using System.Collections.ObjectModel;
using Calendar_To_Do_List.Utilities.DataType;

namespace Calendar_To_Do_List.Utilities.Interfaces;

public interface ITodoService
{
    public ObservableCollection<TodoTerm> TodoTermCollection { get; set; }

    public TodoTerm CreateTodo(string? summary, string description, int priority, DateTime? dueDate = null);

    public void CompleteTodo(TodoTerm todo, DateTime? completedDate = null);

    public void DeleteTodo(TodoTerm todo);
}
