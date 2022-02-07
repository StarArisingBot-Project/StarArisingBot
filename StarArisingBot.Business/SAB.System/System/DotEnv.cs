using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarArisingBot.Business.System
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

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
