using System;

public abstract class Person
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; init; }
    public string PersonalEmail { get; init; }
    public string Notes { get; set; }

    protected Person(string fullName, DateTime dob, string email)
    {
        FullName = fullName;
        DateOfBirth = dob;
        PersonalEmail = email;
    }

    public virtual string GetInfo()
    {
        return $"ПІБ: {FullName} | ДН: {DateOfBirth:yyyy-MM-dd}";
    }
}