using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Proxies;

namespace Calendar_To_Do_List.Utilities.Interfaces;

public interface ITodoService
{
    public IUniqueComponentList<Todo> Todos { get; }

    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate);

    public void CompleteToDo(Todo todo, CalDateTime completedDate);

    public void DeleteToDo(Todo todo);
}
