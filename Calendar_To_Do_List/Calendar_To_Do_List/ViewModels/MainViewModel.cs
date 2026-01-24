using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Calendar_To_Do_List.Utilities.Interfaces;
using Calender_To_Do_List.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ical.Net.CalendarComponents;

namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;

        public ObservableCollection<Todo> TodoItems => _todoService.TodoCollection;

        [ObservableProperty] public string _newTaskContent = String.Empty;
        [ObservableProperty] public DateTime? _newTaskDate;

        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService;

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
            _todoService.ExportToIcs();
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
                TodoItems.RemoveAt(TodoItems.Count - 1);
            }
        }
    }
}