using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExcellentStudent : Student
{
    public string RewardNotes { get; set; }

    public ExcellentStudent(string fullName, DateTime dob, string email, string recordBook)
        : base(fullName, dob, email, recordBook) { }

    public override decimal CalculateScholarship() => 2500m;

    public override string GetInfo() => base.GetInfo() + " [ВІДМІННИК]";
}


