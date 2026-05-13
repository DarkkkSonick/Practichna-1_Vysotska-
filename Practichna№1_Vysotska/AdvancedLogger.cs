using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class AdvancedLogger
{
    private StringBuilder logs = new StringBuilder();

    public void Log(string level, string message)
    {
        logs.AppendLine(
            $"[{DateTime.Now}] [{level}] {message}");
    }

    public void SaveToFile(string path)
    {
        File.WriteAllText(path, logs.ToString());
    }

    public string GetLogsByLevel(string level)
    {
        var lines = logs.ToString()
            .Split('\n')
            .Where(x => x.Contains(level));

        return string.Join("\n", lines);
    }

    public void Clear()
    {
        logs.Clear();
    }

    public string GetLast(int count)
    {
        var lines = logs.ToString()
            .Split('\n',
            StringSplitOptions.RemoveEmptyEntries);

        return string.Join("\n",
            lines.TakeLast(count));
    }

    public override string ToString()
    {
        return logs.ToString();
    }
}