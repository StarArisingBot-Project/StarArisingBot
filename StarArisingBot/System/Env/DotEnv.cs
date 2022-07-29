using System;
using System.IO;

namespace StarArisingBot.System
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (string? line in File.ReadAllLines(filePath))
            {
                string[]? parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                string variable = parts[0];
                string value = parts[1];

                if (parts.Length < 2)
                    continue;

                if (parts.Length > 3)
                {
                    value = line.Replace($"{variable}=", "");
                }
                Environment.SetEnvironmentVariable(variable, value);
            }
        }
    }
}