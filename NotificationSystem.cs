using System;
using System.Collections.Generic;

public class NotificationSystem
{
    private readonly List<string> _logHistory = new List<string>();

    public event EventHandler<StudentEventArgs> StudentAdded;
    public event EventHandler<StudentEventArgs> StudentRemoved;
    public event EventHandler<GroupReportEventArgs> ReportGenerated;

    public IReadOnlyList<string> LogHistory => _logHistory;

    public void OnStudentAdded(Student student)
    {
        var args = new StudentEventArgs(student);
        string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] СИСТЕМА: До складу групи додано студента {student.FullName}";
        _logHistory.Add(message);
        Console.WriteLine(message);

        StudentAdded?.Invoke(this, args);
    }

    public void OnStudentRemoved(Student student)
    {
        var args = new StudentEventArgs(student);
        string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] СИСТЕМА: Зі складу групи виключено студента {student.FullName}";
        _logHistory.Add(message);
        Console.WriteLine(message);

        StudentRemoved?.Invoke(this, args);
    }

    public void OnReportGenerated(string reportContent)
    {
        var args = new GroupReportEventArgs(reportContent);
        string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] СИСТЕМА: Згенеровано новий аналітичний звіт групи";
        _logHistory.Add(message);
        Console.WriteLine(message);

        ReportGenerated?.Invoke(this, args);
    }

    public void OnAverageGradeChanged(object sender, GradeChangedEventArgs e)
    {
        if (sender is Student student)
        {
            string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ПОДІЯ: Студент {student.FullName} змінив бал з {e.OldGrade} на {e.NewGrade}";
            _logHistory.Add(message);
            Console.WriteLine(message);

            if (e.NewGrade < 60)
            {
                string alert = $"[УВАГА ВИКЛАДАЧУ] У студента {student.FullName} критично низький бал: {e.NewGrade}!";
                _logHistory.Add(alert);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(alert);
                Console.ResetColor();
            }
        }
    }

    public void LogAction(string logMessage, Action<string> logMethod)
    {
        logMethod?.Invoke(logMessage);
    }
}