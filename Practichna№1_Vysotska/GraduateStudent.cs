using System;

public sealed class GraduateStudent : Student
{
    public string ThesisTopic { get; set; }

    public GraduateStudent(string fullName, DateTime dob, string email, string recordBook)
        : base(fullName, dob, email, recordBook) { }

    public override string GetInfo()
    {
        return base.GetInfo() + $" | Дипломна: {ThesisTopic}";
    }
}