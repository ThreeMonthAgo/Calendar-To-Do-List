using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.Utilities.Interfaces;

public interface IToDoService
{
    public Todo CreateToDo(string summary, string description, int priority, CalDateTime dueDate);

    public void CompleteToDo(string Uid, CalDateTime completedDate);

    public void CompleteToDo(Todo todo, CalDateTime completedDate);
}
