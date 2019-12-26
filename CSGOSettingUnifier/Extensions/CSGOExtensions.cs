using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace CSGOSettingUnifier.Extensions
{
    public static class CSGOExtensions
    {
        private const string USERDATA = "userdata";
        private const string REG_KEY_STEAM = "Software\\Valve\\Steam";
        private const string STEAM_EXE = "SteamExe";

        public static string GetSteamPath()
        {
            var path = string.Empty;
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(REG_KEY_STEAM);
                var o = key?.GetValue(STEAM_EXE);
                path = (string) o ?? throw new CSGONotFoundException();
                path = GetUserDataFolder(path);
                ConsoleExtensions.WriteLineWithDate("Found Steam path - Set it in the config file");
                return path;
            }
            catch (CSGONotFoundException)
            {
                ConsoleExtensions.WriteLineWithDate("Unable to find CSGO, please open the config file and input information manually");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            catch (Exception)
            {
                ConsoleExtensions.WriteLineWithDate("Unable to find steam folder, please open the config file and input information manually");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

            return path;
        }

        private static string GetUserDataFolder(string path)
        {
            var di = new DirectoryInfo(path).Parent;
            foreach (var folder in di.GetDirectories())
            {
                if (folder.Name == USERDATA)
                {
                    return folder.FullName;
                }
            }

            throw new FileNotFoundException();
        }
    }
}