using CommunityToolkit.Mvvm.ComponentModel;

namespace Calendar_To_Do_List.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
