using System.IO;
using Newtonsoft.Json;

namespace arca.Settings;

public class ArcaSettings
{
    public string Foo { get; set; } = "Bar";
}

public class SettingsManager
{
    private static readonly string SettingsFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Arca", "settings.json");
    public static ArcaSettings Settings { get; private set; }

    static SettingsManager()
    {
        LoadSettings();
    }

    private static void LoadSettings()
    {
        if (File.Exists(SettingsFilePath))
        {
            string json = File.ReadAllText(SettingsFilePath);
            Settings = JsonConvert.DeserializeObject<ArcaSettings>(json);
        }
        else
        {
            Settings = new ArcaSettings();
        }
    }

    public static void SaveSettings()
    {
        string dir = Path.GetDirectoryName(SettingsFilePath);
        Directory.CreateDirectory(dir);
        Console.WriteLine(dir);

        string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
        File.WriteAllText(SettingsFilePath, json);
    }
}