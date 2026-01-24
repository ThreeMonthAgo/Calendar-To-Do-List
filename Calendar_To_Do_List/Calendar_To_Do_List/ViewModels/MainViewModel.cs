using System;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Calendar_To_Do_List.Utilities.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ical.Net.CalendarComponents;
using Ical.Net.Proxies;

namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;
        private readonly ICalendarService _calendarService;

        public IUniqueComponentList<Todo> TodoItems => _todoService.Todos;

        [ObservableProperty] public string _newTaskContent = String.Empty;
        [ObservableProperty] public DateTime? _newTaskDate;

        public MainViewModel(ITodoService todoService, ICalendarService calendarService)
        {
            _todoService = todoService;
            _calendarService = calendarService;

            // 初始测试数据
            _todoService.CreateToDo(
                    string.Empty,
                    "Test",
                    0,
                    new Ical.Net.DataTypes.CalDateTime(DateTime.UtcNow)
                );
        }

        [RelayCommand]
        private void ExportIcs()
        {
            // TODO
            // _calendarService.Export("path/to/export");
        }

        [RelayCommand]
        private void ExitApp() => IApp.ShutdownApp();

        [RelayCommand]
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskContent))
            {
                _todoService.CreateToDo(
                        string.Empty,
                        NewTaskContent,
                        0,
                        new Ical.Net.DataTypes.CalDateTime(NewTaskDate ?? DateTime.UtcNow)
                    );
                NewTaskContent = string.Empty;
                NewTaskDate = null;
            }
        }

        [RelayCommand]
        private void DeleteLastTask()
        {
            if (TodoItems.Count > 0)
            {
                TodoItems.Remove(TodoItems.Last());
            }
        }
    }
}