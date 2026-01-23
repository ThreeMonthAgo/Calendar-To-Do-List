using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CalenderToDoList.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CalenderToDoList.ViewModels
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        // 1. 任务数据集合 (模型内 DeadLine 是 DateTime?)
        public ObservableCollection<ToDoTerm> TodoItems { get; set; } = new();

        // 2. 输入框文字属性
        private string _newTaskContent = string.Empty;
        public string NewTaskContent
        {
            get => _newTaskContent;
            set { _newTaskContent = value; OnPropertyChanged(); }
        }

        // 3. 【重要】日期选择属性：必须使用 DateTimeOffset? 供 UI 控件使用
        private DateTimeOffset? _newTaskDate;
        public DateTimeOffset? NewTaskDate
        {
            get => _newTaskDate;
            set { _newTaskDate = value; OnPropertyChanged(); }
        }

        // 4. 指令定义
        public ICommand ExportIcsCommand { get; }
        public ICommand ExitAppCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteLastTaskCommand { get; }

        public MainViewModel()
        {
            // 初始化指令
            ExportIcsCommand = new RelayCommand(() => {
                System.Diagnostics.Debug.WriteLine("执行导出逻辑...");
            });

            ExitAppCommand = new RelayCommand(() => {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            });

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteLastTaskCommand = new RelayCommand(DeleteLastTask);

            // 初始测试数据
            TodoItems.Add(new ToDoTerm { TaskContent = "完成项目开发", IsDone = false, DeadLine = DateTime.Now });
        }

        // 添加任务逻辑
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


        // 删除逻辑
        private void DeleteLastTask()
        {
            if (TodoItems.Count > 0)
            {
                TodoItems.RemoveAt(TodoItems.Count - 1);
            }
        }

        // 属性更改通知
        public new event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // 指令辅助类
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) => _execute = execute;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged { add { } remove { } }
    }
}