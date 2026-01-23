using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calendar_To_Do_List;

public interface IApp
{
    #region Fields

    /// <summary>The directory of the application</summary>
    public readonly static string DataDir = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData,
            Environment.SpecialFolderOption.Create),
        "Calendar To Do List");

    public readonly static string SettingsPath = Path.Combine(DataDir, "settings.json");

    #endregion

    #region Functions

    public static void ShutdownApp()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime app)
        {
            app.Shutdown();
        }
    }

    #endregion

    public static IHost Host;

    public static T GetService<T>()
    {
        if (Host is null) throw new Exception("Host is null");
        var s = Host.Services.GetService<T>();
        if (s != null) return s;
        else throw new ArgumentException($"Service {typeof(T)} is null!");
    }
}
