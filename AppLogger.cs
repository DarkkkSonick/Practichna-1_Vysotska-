using System;
using System.IO;
using System.Text;

public static class AppLogger
{
    private static readonly string LogsFolder = "Logs";
    private static readonly string CurrentLogFile = Path.Combine(LogsFolder, "app_actions.log");
    private static readonly long MaxLogFileSize = 5120;

    static AppLogger()
    {
        Directory.CreateDirectory(LogsFolder);
    }

    public static void LogAction(string action)
    {
        try
        {
            CheckRotation();
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ДІЯ: {action}{Environment.NewLine}";
            File.AppendAllText(CurrentLogFile, logEntry, Encoding.UTF8);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка запису логу дії: {ex.Message}");
        }
    }

    private static void CheckRotation()
    {
        if (File.Exists(CurrentLogFile))
        {
            var fileInfo = new FileInfo(CurrentLogFile);
            if (fileInfo.Length >= MaxLogFileSize)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string rotatedFilePath = Path.Combine(LogsFolder, $"app_actions_{timestamp}.log");
                File.Move(CurrentLogFile, rotatedFilePath);
            }
        }
    }

    public static string ReadCurrentLogs()
    {
        if (!File.Exists(CurrentLogFile)) return "Логи дій порожні.";
        return File.ReadAllText(CurrentLogFile, Encoding.UTF8);
    }
}