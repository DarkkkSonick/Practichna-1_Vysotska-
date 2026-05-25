using System;
using System.IO;
using System.Text;
using System.Text.Json;

public static class FileManager
{
    private static readonly string BackupsFolder = "Backups";
    private static readonly string ReportsFolder = "Reports";
    private static readonly string LogsFolder = "Logs";

    static FileManager()
    {
        Directory.CreateDirectory(BackupsFolder);
        Directory.CreateDirectory(ReportsFolder);
        Directory.CreateDirectory(LogsFolder);
    }

    public static void SaveToJson<T>(T data, string filePath)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        }
        catch (JsonException ex)
        {
            throw new InvalidFileFormatException("Помилка серіалізації в JSON", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка вводу-виводу при збереженні JSON: {ex.Message}", ex);
        }
    }

    public static T LoadFromJson<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл JSON не знайдено", filePath);
            }
            string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (JsonException ex)
        {
            throw new InvalidFileFormatException("Некоректний формат файлу JSON", ex);
        }
    }

    public static void SaveToText(string content, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.Write(content);
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка запису у текстовий файл: {ex.Message}", ex);
        }
    }

    public static string ReadFromText(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Текстовий файл не знайдено", filePath);
            }
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка читання текстового файлу: {ex.Message}", ex);
        }
    }

    public static void ExportToCsv(StudentGroup group, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Повне ім'я,Номер заліковки,Середній бал,Статус");
                foreach (var student in group.GetAllStudents())
                {
                    writer.WriteLine($"\"{student.FullName}\",\"{student.RecordBookNumber}\",\"{student.AverageGrade}\",\"{student.Status}\"");
                }
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка експорту в CSV: {ex.Message}", ex);
        }
    }

    public static void CreateBackup(string sourcePath)
    {
        try
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException("Початковий файл для бекапу не знайдено", sourcePath);
            }

            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string extension = Path.GetExtension(sourcePath);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFileName = $"{fileName}_{timestamp}{extension}";
            string destPath = Path.Combine(BackupsFolder, backupFileName);

            File.Copy(sourcePath, destPath, true);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"Немає доступу для створення резервної копії: {ex.Message}", ex);
        }
    }

    public static void CleanOldBackups(int daysOld)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(BackupsFolder);
            var files = directoryInfo.GetFiles();
            var threshold = DateTime.Now.AddDays(-daysOld);

            foreach (var file in files)
            {
                if (file.CreationTime < threshold)
                {
                    file.Delete();
                }
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Помилка під час очищення старих бекапів: {ex.Message}", ex);
        }
    }

    public static string[] GetBackupFiles()
    {
        return Directory.GetFiles(BackupsFolder);
    }
}