using System;

public class Teacher : UniversityMember
{
    public string Department { get; set; }
    public decimal BaseSalary { get; set; }

    public Teacher(string fullName, DateTime dob, string email, string department, decimal salary)
        : base(fullName, dob, email)
    {
        Department = department;
        BaseSalary = salary;
    }

    public override decimal CalculateScholarship()
    {
        return 0;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $" | Кафедра: {Department} | Зарплата: {BaseSalary}";
    }
}