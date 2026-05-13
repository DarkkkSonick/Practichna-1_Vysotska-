using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

public class Student
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    private string _fullName;
    private string _recordBookNumber;
    private double _averageGrade;
    private DateTime _dateOfBirth;
    private StudentStatus _status;
    private DateTime _enrollmentDate;
    private string _personalEmail;
    private string _notes = "";

    public int CourseProgress { get; set; }
    public List<GradePoint> GradeHistory { get; set; } = new List<GradePoint>();

    public required string PersonalEmail
    {
        get => _personalEmail;
        init
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Невірний email");
            _personalEmail = value;
        }
    }

    public DateTime EnrollmentDate { get; init; }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (value.Trim().Split(' ').Length < 3)
                throw new ArgumentException("Введіть ПІБ повністю");
            _fullName = value;
        }
    }

    public string RecordBookNumber
    {
        get => _recordBookNumber;
        set
        {
            if (!Regex.IsMatch(value, @"^\d{8}$"))
                throw new ArgumentException("Номер має містити 8 цифр");
            _recordBookNumber = value;
        }
    }

    public DateTime DateOfBirth { get; init; }
    public StudentStatus Status { get; set; }
    public string Notes { get; set; }
    public double AverageGrade
    {
        get => _averageGrade;
        private set => _averageGrade = Math.Round(value, 2);
    }

    public int Age => CalculateAge();

    private int CalculateAge()
    {
        var today = DateTime.Today;
        int age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 100) throw new ArgumentOutOfRangeException();
        AverageGrade = newGrade;
    }

    public static bool operator >(Student s1, Student s2)
    {
        if (s1.AverageGrade != s2.AverageGrade) return s1.AverageGrade > s2.AverageGrade;
        return s1.CourseProgress > s2.CourseProgress;
    }

    public static bool operator <(Student s1, Student s2) => s2 > s1;

    public static bool operator >=(Student s1, Student s2) => !(s1 < s2);

    public static bool operator <=(Student s1, Student s2) => !(s1 > s2);

    public static bool operator ==(Student s1, Student s2)
    {
        if (ReferenceEquals(s1, s2)) return true;
        if (s1 is null || s2 is null) return false;
        return s1.AverageGrade == s2.AverageGrade && s1.CourseProgress == s2.CourseProgress;
    }

    public static bool operator !=(Student s1, Student s2) => !(s1 == s2);

    public static string operator +(Student s1, Student s2) => $"Команда: {s1.FullName} та {s2.FullName}";

    public override bool Equals(object obj) => obj is Student s && this == s;

    public override int GetHashCode() => HashCode.Combine(AverageGrade, CourseProgress);

    public string GetFormattedInfo(bool detailed = false)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ПІБ: {FullName}");
        sb.AppendLine($"Заліковка: {RecordBookNumber}");
        sb.AppendLine($"Бал: {AverageGrade}");
        if (detailed)
        {
            sb.AppendLine($"Email: {PersonalEmail}");
            sb.AppendLine($"Вік: {Age}");
            sb.AppendLine($"Прогрес: {CourseProgress}%");
            sb.AppendLine($"Статус: {Status}");
        }
        return sb.ToString();
    }
}