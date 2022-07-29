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

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line == null || string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    continue;

                foreach (string addVarCommand in line.Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] tokens = addVarCommand.Split('=', StringSplitOptions.RemoveEmptyEntries);

                    string varName = tokens[0];
                    string varValue = tokens[1];

                    if (string.IsNullOrWhiteSpace(varName) || string.IsNullOrWhiteSpace(varValue))
                    {
                        throw new FormatException();
                    }
                    if ((!varName.StartsWith("[") || !varName.EndsWith("]")) && (!varValue.StartsWith("\"") || !varValue.EndsWith("\"")))
                    {
                        throw new FormatException();
                    }

                    varName = varName.Substring(1, varName.Length - 2);
                    varValue = varValue.Substring(1, varValue.Length - 2);

                    Environment.SetEnvironmentVariable(varName, varValue);
                }
            }
        }
    }
}