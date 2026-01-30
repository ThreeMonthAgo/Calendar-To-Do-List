using System;
using System.Collections.ObjectModel;
using System.Linq;
using Calendar_To_Do_List.Utilities.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ical.Net;
using Ical.Net.DataTypes;
using Calendar_To_Do_List.Utilities.DataType;

namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;
        private readonly ICalendarService _calendarService;

        // UI 绑定的数据源
        public ObservableCollection<TodoTerm> TodoItems => _todoService.TodoTermCollection;

        [ObservableProperty] private string _newTaskContent = string.Empty;
        [ObservableProperty] private DateTime? _newTaskDate;

        public MainViewModel(ITodoService todoService, ICalendarService calendarService)
        {
            _todoService = todoService;
            _calendarService = calendarService;
        }

        [RelayCommand]
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskContent))
            {
                _todoService.CreateTodo(string.Empty, NewTaskContent, 0, NewTaskDate);

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
                if (_todoService.TodoTermCollection.Count > 0)
                {
                    var lastServiceItem = _todoService.TodoTermCollection.Last();
                    _todoService.DeleteTodo(lastServiceItem);
                }
            }
        }

        [RelayCommand] private void ExportIcs()
        {
            // TODO: Export
        }

        [RelayCommand] private void ExitApp() => IApp.ShutdownApp();
    }
}