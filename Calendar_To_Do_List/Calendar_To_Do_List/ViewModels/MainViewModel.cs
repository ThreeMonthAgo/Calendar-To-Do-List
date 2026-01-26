using System;
using System.Collections.ObjectModel;
using System.Linq;
using Calendar_To_Do_List.Utilities.Interfaces;
using Calender_To_Do_List.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ical.Net;
using Ical.Net.DataTypes;

namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;

        // UI 绑定的数据源
        public ObservableCollection<ToDoTerm> TodoItems { get; } = new();

        [ObservableProperty] private string _newTaskContent = string.Empty;
        [ObservableProperty] private DateTime? _newTaskDate;

        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService;

            // 同步 Service 里的初始数据到 UI
            foreach (var item in _todoService.TodoCollection)
            {
                TodoItems.Add(new ToDoTerm
                {
                    TaskContent = item.Summary,
                    IsDone = item.Status == TodoStatus.Completed,
                    DeadLine = item.Start?.Value
                });
            }
        }

        [RelayCommand]
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskContent))
            {
                var dueDate = NewTaskDate.HasValue ? new CalDateTime(NewTaskDate.Value) : new CalDateTime(DateTime.UtcNow);

                // 1. 同步到逻辑层 (Ical.Net)
                _todoService.CreateToDo(NewTaskContent, string.Empty, 0, dueDate);

                // 2. 同步到 UI 层
                TodoItems.Add(new ToDoTerm
                {
                    TaskContent = NewTaskContent,
                    IsDone = false,
                    DeadLine = NewTaskDate
                });

                NewTaskContent = string.Empty;
                NewTaskDate = null;
            }
        }

        [RelayCommand]
        private void DeleteLastTask()
        {
            if (TodoItems.Count > 0)
            {
                TodoItems.RemoveAt(TodoItems.Count - 1);
                if (_todoService.TodoCollection.Count > 0)
                {
                    var lastServiceItem = _todoService.TodoCollection.Last();
                    _todoService.DeleteToDo(lastServiceItem);
                }
            }
        }

        [RelayCommand] private void ExportIcs() => _todoService.ExportToIcs();
        [RelayCommand] private void ExitApp() => IApp.ShutdownApp();
    }
}