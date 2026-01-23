using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Calender_To_Do_List.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calendar_To_Do_List.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ToDoTerm> TodoItems { get; set; } = [];

        [ObservableProperty] public string _newTaskContent = String.Empty;
        [ObservableProperty] public DateTimeOffset? _newTaskDate;

        public MainViewModel()
        {
            // 初始测试数据
            TodoItems.Add(new ToDoTerm { TaskContent = "完成项目开发", IsDone = false, DeadLine = DateTime.Now });
        }

        [RelayCommand]
        private void ExportIcs()
        {
            // TODO: Export
        }

        [RelayCommand]
        private void ExitApp() => IApp.ShutdownApp();

        [RelayCommand]
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskContent))
            {
                TodoItems.Add(new ToDoTerm
                {
                    TaskContent = NewTaskContent,
                    IsDone = false,
                    // 直接赋值，不再需要 .DateTime
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
            }
        }
    }
}