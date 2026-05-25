using System;

public class Professor : Teacher
{
    public string AcademicTitle { get; set; }

    public Professor(string fullName, DateTime dob, string email, string department, decimal salary, string title)
        : base(fullName, dob, email, department, salary)
    {
        AcademicTitle = title;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $" | Вчене звання: {AcademicTitle}";
    }
}