using System;
using System.Text.RegularExpressions;

public class Student
{
    public enum StudentStatus
    {
        Active,
        AcademicLeave,
        Expelled,
        Graduated
    }

    private string _fullName;
    private string _recordBookNumber;
    private double _averageGrade;
    private DateTime _dateOfBirth;
    private StudentStatus _status;
    private DateTime _enrollmentDate;
    private string _personalEmail;
    private string _notes;

    public required string PersonalEmail
    {
        get => _personalEmail;
        init
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException();
            _personalEmail = value;
        }
    }

    public DateTime EnrollmentDate
    {
        get => _enrollmentDate;
        init => _enrollmentDate = value;
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                throw new ArgumentException();
            _fullName = value;
        }
    }

    public string RecordBookNumber
    {
        get => _recordBookNumber;
        set
        {
            if (!Regex.IsMatch(value, @"^\d{8}$"))
                throw new ArgumentException();
            _recordBookNumber = value;
        }
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value;
    }

    public StudentStatus Status
    {
        get => _status;
        set => _status = value;
    }

    public string Notes
    {
        get => _notes;
        set => _notes = value;
    }

    public double AverageGrade
    {
        get => _averageGrade;
        private set => _averageGrade = Math.Round(value, 2);
    }

    public int Age => CalculateAge();

    public int CalculateAge()
    {
        var today = DateTime.Today;
        var age = today.Year - _dateOfBirth.Year;
        if (_dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 100)
            throw new ArgumentOutOfRangeException();
        AverageGrade = newGrade;
    }

    public bool IsExcellent() => AverageGrade >= 90;

    public bool IsFailing() => AverageGrade < 60;

    public int GetYearsToGraduation()
    {
        if (Status == StudentStatus.Graduated || Status == StudentStatus.Expelled) return 0;
        int yearsSpent = DateTime.Now.Year - EnrollmentDate.Year;
        int remaining = 4 - yearsSpent;
        return remaining > 0 ? remaining : 0;
    }

    public void ShowDetailedInfo()
    {
        Console.WriteLine($"Name: {FullName}");
        Console.WriteLine($"Age: {Age} (Born: {DateOfBirth:yyyy-MM-dd})");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Enrollment: {EnrollmentDate:yyyy-MM-dd}");
        Console.WriteLine($"Record Book: {RecordBookNumber}");
        Console.WriteLine($"Grade: {AverageGrade}");
        Console.WriteLine($"Email: {PersonalEmail}");
        Console.WriteLine($"Notes: {Notes}");
    }
}