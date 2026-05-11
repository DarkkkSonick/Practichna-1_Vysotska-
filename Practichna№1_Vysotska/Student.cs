using System;
using System.Text;
using System.Text.RegularExpressions;

public class Student : ICloneable
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

    public byte[] LabGrades { get; set; } = new byte[10];

    public int PortRow { get; set; } = -1;
    public int PortCol { get; set; } = -1;

    public required string PersonalEmail
    {
        get => _personalEmail;
        init
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Неправильний email");
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
                throw new ArgumentException("ПІБ закороткий");
            _fullName = value;
        }
    }

    public string RecordBookNumber
    {
        get => _recordBookNumber;
        set
        {
            if (!Regex.IsMatch(value, @"^\d{8}$"))
                throw new ArgumentException("Заліковка має містити 8 цифр");
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

        if (_dateOfBirth.Date > today.AddYears(-age))
            age--;

        return age;
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 100)
            throw new ArgumentOutOfRangeException();

        AverageGrade = newGrade;
    }

    public void AddLabGrade(int labNumber, byte grade)
    {
        if (labNumber < 0 || labNumber >= 10)
            throw new IndexOutOfRangeException();

        LabGrades[labNumber] = grade;
    }

    public double GetAverageLabGrade()
    {
        return Math.Round(LabGrades.Average(x => x), 2);
    }

    public bool IsExcellent() => AverageGrade >= 90;

    public bool IsFailing() => AverageGrade < 60;

    public void ShowDetailedInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Name: {FullName}");
        sb.AppendLine($"Age: {Age}");
        sb.AppendLine($"Record Book: {RecordBookNumber}");
        sb.AppendLine($"Grade: {AverageGrade}");
        sb.AppendLine($"Average Lab Grade: {GetAverageLabGrade()}");
        sb.AppendLine($"Email: {PersonalEmail}");
        sb.AppendLine($"Status: {Status}");

        Console.WriteLine(sb.ToString());
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}