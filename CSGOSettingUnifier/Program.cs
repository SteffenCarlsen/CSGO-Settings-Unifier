using System;
using CSGOSettingUnifier.Extensions;
using CSGOSettingUnifier.Settings;

namespace CSGOSettingUnifier
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = OnLoad();
            ReplaceAllSettings(config);
            Console.ReadKey();
        }

        private static Config OnLoad()
        {
            Console.Title = "CSGO Settings Unifier";
            var settings = new Config(true);
            if (string.IsNullOrEmpty(settings.CSGOPath))
            {
                settings.CSGOPath = CSGOExtensions.GetSteamPath();
                settings.SaveSettings();
            }

            return settings;
        }

        private static void ReplaceAllSettings(Config config)
        {
            if (config.OverrideConfigSettings || config.OverrideVideoSettings)
            {
                var configReplacements = OverrideFiles.OverrideConfigs(config);
                Console.WriteLine($"Replaced configs for {configReplacements} users");
                return;
            }

            ConsoleExtensions.WriteLineWithDate("No settings to replace due to settings, please change them to replace the settings");
            Console.WriteLine("Press any key to exit application...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}