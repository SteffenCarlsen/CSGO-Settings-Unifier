using System;
using System.IO;
using System.Linq;
using CSGOSettingUnifier.Extensions;
using CSGOSettingUnifier.Settings;

namespace CSGOSettingUnifier
{
    public static class OverrideFiles
    {
        private const string CSGO_FOLDER_NAME = "730";
        private const string LOCAL = "local";
        private const string CFG = "cfg";
        private const string CONFIG = "Config";

        private const string LINE_SEPARATOR = "_____________________________________________________________";

        // TODO: Handle no config or txt files for moving
        public static int OverrideConfigs(Config settings)
        {
            Console.WriteLine("Replacing files...");
            Console.WriteLine(LINE_SEPARATOR);
            var di = new DirectoryInfo(settings.CSGOPath);
            var folders = di.GetDirectories();
            foreach (var userFolder in folders)
            {
                var subDirectories = userFolder.GetDirectories();
                var csgoFolder = subDirectories.SingleOrDefault(dir => dir.Name == CSGO_FOLDER_NAME);
                if (csgoFolder != null)
                {
                    ReplaceFiles(settings, csgoFolder, userFolder);
                }
                else
                {
                    csgoFolder = CreateFolder(userFolder, CSGO_FOLDER_NAME);
                    ReplaceFiles(settings, csgoFolder, userFolder);
                }

                Console.WriteLine(LINE_SEPARATOR);
            }

            return folders.Length;
        }

        private static DirectoryInfo CreateFolder(DirectoryInfo userFolder, string name)
        {
            Console.WriteLine($"Created folder: {name} inside {userFolder.Name}");
            return userFolder.CreateSubdirectory(name);
        }

        private static void ReplaceFiles(Config settings, DirectoryInfo subdir, DirectoryInfo userFolder)
        {
            var configFolder = Path.Combine(Path.Combine(subdir.FullName, LOCAL), CFG);
            var di = new DirectoryInfo(configFolder);
            if (!di.Exists)
            {
                di.Create();
            }

            var configFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, CONFIG), "*.cfg");
            var videoFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, CONFIG), "*.txt");
            if (settings.OverrideConfigSettings)
            {
                foreach (var configFile in configFiles)
                {
                    var f = new FileInfo(configFile);
                    f.CopyTo(Path.Combine(configFolder, f.Name), true);
                    ConsoleExtensions.WriteLineWithDate($"Moved {f.Name} to {userFolder.Name}");
                }
            }

            if (settings.OverrideVideoSettings)
            {
                foreach (var videoFile in videoFiles)
                {
                    var f = new FileInfo(videoFile);
                    f.CopyTo(Path.Combine(configFolder, f.Name), true);
                    ConsoleExtensions.WriteLineWithDate($"Moved {f.Name} to {userFolder.Name}");
                }
            }
        }
    }
}