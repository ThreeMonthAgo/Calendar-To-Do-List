using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Calendar_To_Do_List.Utilities.DataType
{
    public partial class TodoTerm : ObservableObject
    {
        [ObservableProperty]
        public string summary = string.Empty;

        [ObservableProperty]
        public string description = string.Empty;

        [ObservableProperty]
        public int priority = 0;

        /// <remarks>
        /// Ensure it's a UTC time. e.g.
        /// <code>
        /// Due = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc);
        /// </code>
        /// </remarks>
        [ObservableProperty]
        public DateTime? due;

        [ObservableProperty]
        public bool isCompleted = false;

        /// <remarks>
        /// Ensure it's a UTC time.
        /// <seealso cref="Due"/>
        /// </remarks>
        [ObservableProperty]
        public DateTime? completedTime;

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
