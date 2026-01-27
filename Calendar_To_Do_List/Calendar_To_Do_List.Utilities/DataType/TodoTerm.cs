using System;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Calendar_To_Do_List.Utilities.DataType
{
    public class TodoTerm
    {
        public string? Summary { get; set; } = string.Empty;

        /// <remarks>Description is necessary</remarks>
        public required string Description { get; set; } = string.Empty;

        public int Priority { get; set; } = 0;

        /// <remarks>
        /// Ensure it's a UTC time. e.g.
        /// <code>
        /// Due = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc);
        /// </code>
        /// </remarks>
        public DateTime? Due { get; set; }

        public bool IsCompleted { get; set; } = false;

        /// <remarks>
        /// Ensure it's a UTC time.
        /// <seealso cref="Due"/>
        /// </remarks>
        public DateTime? CompletedTime { get; set; }

        public static implicit operator Todo(TodoTerm v)
        {
            var t = new Todo
            {
                Description = v.Description,
                Priority = v.Priority
            };
            if (v.Summary != null) t.Summary = v.Summary;
            if (v.Due != null)
            {
                var d = (DateTime)v.Due;
                if (d.Kind == DateTimeKind.Utc)
                {
                    t.Due = new(d);
                }
            }
            if (v.IsCompleted)
            {
                t.Status = TodoStatus.Completed;
                if (v.CompletedTime != null)
                {
                    t.Completed = new((DateTime)v.CompletedTime);
                }
            }
            return t;
        }
    }
}
