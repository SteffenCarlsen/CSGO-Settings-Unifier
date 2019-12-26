using System.IO;
using CSGOSettingUnifier.Extensions;

namespace CSGOSettingUnifier.Settings
{
    public class Config
    {
        private const string CONFIG_FOLDER_NAME = "Config";
        private const string CONFIG_NAME = "Settings.json";

        public Config()
        {
        }

        public Config(bool onLoad)
        {
            if (!SettingsExist())
            {
                CreateConfigFolder();
            }

            if (!ConfigExists())
            {
                SaveSettings();
            }
            else
            {
                var settings = LoadSettings();
                CSGOPath = settings.CSGOPath;
                OverrideConfigSettings = settings.OverrideConfigSettings;
                OverrideVideoSettings = settings.OverrideVideoSettings;
            }
        }

        public string CSGOPath { get; set; }
        public bool OverrideVideoSettings { get; set; }
        public bool OverrideConfigSettings { get; set; }

        private bool SettingsExist()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            var di = new DirectoryInfo(currentFolder);
            var directories = di.GetDirectories();
            foreach (var directoryInfo in directories)
            {
                if (directoryInfo.Name == CONFIG_FOLDER_NAME)
                {
                    return true;
                }
            }

            return false;
        }

        private void CreateConfigFolder()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            var di = new DirectoryInfo(currentFolder);
            di.CreateSubdirectory(CONFIG_FOLDER_NAME);
            ConsoleExtensions.WriteLineWithDate("Created Config Folder");
        }

        private bool ConfigExists()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            var di = new DirectoryInfo(Path.Combine(currentFolder, CONFIG_FOLDER_NAME));
            foreach (var file in di.GetFiles("*.json"))
            {
                if (file.Name == CONFIG_NAME)
                {
                    return true;
                }
            }

            return false;
        }

        public void SaveSettings()
        {
            ConsoleExtensions.WriteLineWithDate("Saved Config");
            JsonObjectFileSaveLoad.WriteToJsonFile(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FOLDER_NAME), CONFIG_NAME), this);
        }

        public Config LoadSettings()
        {
            ConsoleExtensions.WriteLineWithDate("Loaded Config");
            return JsonObjectFileSaveLoad.ReadFromJsonFile<Config>(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FOLDER_NAME), CONFIG_NAME));
        }
    }
}