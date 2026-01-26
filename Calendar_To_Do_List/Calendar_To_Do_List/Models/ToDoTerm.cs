using System;

namespace Calender_To_Do_List.Models
{
    public class ToDoTerm
    {
        public bool IsDone { get; set; } = false;
        public string TaskContent { get; set; } = string.Empty;
        public DateTimeOffset? DeadLine { get; set; }
    }
}
