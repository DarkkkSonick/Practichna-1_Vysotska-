using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YourProjectName;

public class Student : UniversityMember
{
    public List<Shape> Shapes { get; set; } = new List<Shape>();
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    private string _fullName;
    private string _recordBookNumber;
    private double _averageGrade;
    private StudentStatus _status;
    private string _personalEmail;

    public int CourseProgress { get; set; }
    public List<GradePoint> GradeHistory { get; set; } = new List<GradePoint>();
    public GradeJournal GradeJournal { get; set; } = new GradeJournal();
    public StudentStatus Status { get; set; }
    public string Notes { get; set; }

    public event EventHandler<GradeChangedEventArgs> AverageGradeChanged;

    public double AverageGrade
    {
        get => _averageGrade;
        private set => _averageGrade = Math.Round(value, 2);
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

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public Student(string fullName, DateTime dob, string email, string recordBook)
        : base(fullName, dob, email)
    {
        RecordBookNumber = recordBook;
        Status = StudentStatus.Active;
    }

    public StudentRecord GetRecord()
    {
        int id = int.TryParse(RecordBookNumber, out int result) ? result : 0;
        return new StudentRecord(id, FullName);
    }

    public override decimal CalculateScholarship()
    {
        return AverageGrade >= 90 ? 2000m : 0m;
    }

    public override string GetInfo()
    {
        return GetFormattedInfo(true);
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 100) throw new ArgumentOutOfRangeException();

        double oldGrade = _averageGrade;
        AverageGrade = newGrade;

        if (oldGrade != _averageGrade)
        {
            AverageGradeChanged?.Invoke(this, new GradeChangedEventArgs(oldGrade, _averageGrade));
        }
    }

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

    public void ExecuteStudentOperation(StudentOperation operation)
    {
        operation?.Invoke(this);
    }

    public static bool operator >(Student s1, Student s2) => s1.AverageGrade > s2.AverageGrade;
    public static bool operator <(Student s1, Student s2) => s1.AverageGrade < s2.AverageGrade;
    public static bool operator ==(Student s1, Student s2) => s1?.AverageGrade == s2?.AverageGrade;
    public static bool operator !=(Student s1, Student s2) => !(s1 == s2);
}