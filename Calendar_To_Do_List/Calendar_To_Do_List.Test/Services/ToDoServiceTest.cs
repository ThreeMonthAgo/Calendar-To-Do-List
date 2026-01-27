using System;
using System.Collections.Generic;
using System.Text;
using Calendar_To_Do_List.Services;
using Calendar_To_Do_List.Utilities.Interfaces;

namespace Calendar_To_Do_List.Test.Services
{
    [TestClass]
    public sealed class TodoServiceTest
    {
        private readonly ITodoService tds = new TodoService();

        [TestMethod]
        public void CompleteTest()
        {
            var todo = tds.CreateTodo("test", "description", 0);
            tds.CompleteTodo(todo);
            Assert.IsTrue(todo.IsCompleted);
        }
    }
}
