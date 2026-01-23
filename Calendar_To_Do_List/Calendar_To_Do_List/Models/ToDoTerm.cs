using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Calendar_To_Do_List.Utilities.Enums;

namespace Calendar_To_Do_List.Models
{
    public class ToDoTerm
    {
        public bool IsDone { get; set; } = false;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeadLine { get; set; } = null;
        public Importance Importance { get; set; } = Importance.Normal;
    }
}
