using Avalonia.Styling;
using Calendar_To_Do_List.Utilities.DataType;
using Calendar_To_Do_List.Utilities.Interfaces;
using Calender_To_Do_List.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ical.Net;
using Ical.Net.DataTypes;


using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;




namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;

       
        

        // UI 绑定的数据源
        public ObservableCollection<ToDoTerm> TodoItems { get; } = new();

        [ObservableProperty] private string _newTaskContent = string.Empty;
        [ObservableProperty] private DateTime? _newTaskDate;
        [ObservableProperty] private TimeSpan? _newTaskTime;
        [ObservableProperty] private bool _isSidebarCollapsed; public double SidebarWidth => IsSidebarCollapsed ? 50 : 250;


        [RelayCommand]
        private void ToggleSidebar()
        {
            IsSidebarCollapsed = !IsSidebarCollapsed;
            OnPropertyChanged(nameof(SidebarWidth));
        }


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


                DateTime? finalDate = null;


                if (NewTaskDate.HasValue)
                {
                    // 获取日期部分 (Year/Month/Day)
                    // 如果 NewTaskDate 是 DateTimeOffset，用 .DateTime.Date；如果是 DateTime，直接 .Date
                    finalDate = NewTaskDate.Value.Date;

                    // 如果用户选择了时间，则累加时间偏移
                    if (NewTaskTime.HasValue)
                    {
                        finalDate = finalDate.Value.Add(NewTaskTime.Value);
                    }
                }
                // ------------------------------

                // 调用 Service，传入合并后的日期时间对象
                _todoService.CreateTodo(string.Empty, NewTaskContent, 0, finalDate);

                // 重置所有输入项
                NewTaskContent = string.Empty;
                NewTaskDate = null;
                NewTaskTime = null; // 别忘了重置时间选择器
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

        [RelayCommand]
        private void ToggleTheme()
        {
            // 获取当前应用实例
            var app = Avalonia.Application.Current;
            if (app != null)
            {
                // 如果当前是深色，就切到浅色，反之亦然
                if (app.RequestedThemeVariant == ThemeVariant.Dark)
                {
                    app.RequestedThemeVariant = ThemeVariant.Light;
                }
                else
                {
                    app.RequestedThemeVariant = ThemeVariant.Dark;
                }
            }
        }



        [RelayCommand] private void ExportIcs()
        {
            // TODO: Export
        }

>>>>>>> Stashed changes
        [RelayCommand] private void ExitApp() => IApp.ShutdownApp();
    }
}