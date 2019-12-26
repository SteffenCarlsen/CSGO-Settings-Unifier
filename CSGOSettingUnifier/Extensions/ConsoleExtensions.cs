using System;

namespace CSGOSettingUnifier.Extensions
{
    public static class ConsoleExtensions
    {
        public static void WriteLineWithDate(string log)
        {
            var formattedString = string.Format($"{DateTime.Now.ToString()}: {log}");
            Console.WriteLine(formattedString);
        }
    }
}