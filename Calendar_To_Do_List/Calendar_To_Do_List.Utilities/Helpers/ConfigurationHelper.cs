using System.IO;

namespace Calendar_To_Do_List.Utilities.Helpers;

public static class ConfigurationHelper
{
    public static void SaveConfiguration(string config, string path)
    {
        using FileStream f = new(path, FileMode.Create, FileAccess.Write);
        using var w = new StreamWriter(f);
        w.Write(config);
    }

    public static string LoadConfiguration(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File {path} not found");
        }
        using var f = File.OpenText(path);
        return f.ReadToEnd();
    }
}
