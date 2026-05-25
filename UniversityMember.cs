using System;

public abstract class UniversityMember : Person
{
    protected UniversityMember(string fullName, DateTime dob, string email)
        : base(fullName, dob, email) { }

    public abstract decimal CalculateScholarship();
}