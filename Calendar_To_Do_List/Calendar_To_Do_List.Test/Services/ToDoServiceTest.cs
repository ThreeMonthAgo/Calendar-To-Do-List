using System;
using System.Collections.Generic;
using System.Text;
using Calendar_To_Do_List.Services;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.Test.Services
{
    [TestClass]
    public sealed class ToDoServiceTest
    {
        ToDoService tds = new();

        [TestMethod]
        public void CompleteTest()
        {
            var todo = tds.CreateToDo("test", "description", 0, new(2026,1,1));
            tds.CompleteToDo(todo);
            Assert.IsTrue(todo.IsCompleted(new(2026, 1, 2)));
        }
    }
}
