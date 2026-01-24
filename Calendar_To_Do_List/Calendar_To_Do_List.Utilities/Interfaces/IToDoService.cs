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
}
