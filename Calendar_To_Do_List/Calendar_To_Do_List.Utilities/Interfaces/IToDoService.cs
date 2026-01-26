using System.Collections.ObjectModel;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.Utilities.Interfaces;

public interface ITodoService
{
    public ObservableCollection<Todo> TodoCollection { get; set; }

    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate);

    public void CompleteToDo(Todo todo, CalDateTime completedDate);

    public void DeleteToDo(Todo todo);

    public void ExportToIcs();

    ObservableCollection<Todo> GetTodos();





    // TodoService.cs
    public class TodoService : ITodoService
    {
        private readonly ObservableCollection<Todo> _todoCollection = new();

        public ObservableCollection<Todo> TodoCollection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObservableCollection<Todo> GetTodos()
        {
            return _todoCollection;
        }

        public Todo CreateTodo(string summary, string description, int priority, CalDateTime dueDate)
        {
            var newTodo = new Todo
            {
                Summary = summary,
                Description = description,
                Start = dueDate
            };
            _todoCollection.Add(newTodo);
            return newTodo;
        }

        public void DeleteTodo(Todo todo)
        {
            _todoCollection.Remove(todo);
        }

        // 实现其他方法...
        public void CompleteTodo(Todo todo, CalDateTime completedDate) { /* 逻辑 */ }
        public void ExportToIcs() { /* 逻辑 */ }

        public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate)
        {
            throw new NotImplementedException();
        }

        public void CompleteToDo(Todo todo, CalDateTime completedDate)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDo(Todo todo)
        {
            throw new NotImplementedException();
        }
    }


}
