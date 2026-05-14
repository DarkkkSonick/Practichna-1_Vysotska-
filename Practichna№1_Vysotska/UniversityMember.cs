using System;

public abstract class UniversityMember : Person
{
    protected UniversityMember(string fullName, DateTime dob, string email)
        : base(fullName, dob, email) { }

    public abstract decimal CalculateScholarship();

    public virtual void Enroll()
    {
        Console.WriteLine($"{FullName} зараховано до списків.");
    }
}