using System;
using System.IO;
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

    public static IHost Host;
}
