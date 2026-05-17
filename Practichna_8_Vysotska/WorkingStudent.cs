using System;

public class WorkingStudent : Student
{
    public string JobTitle { get; set; }

    public WorkingStudent(string fullName, DateTime dob, string email, string recordBook, string job)
        : base(fullName, dob, email, recordBook)
    {
        JobTitle = job;
    }

    public override decimal CalculateScholarship()
    {
        return base.CalculateScholarship() * 0.5m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $" | Робота: {JobTitle}";
    }
}