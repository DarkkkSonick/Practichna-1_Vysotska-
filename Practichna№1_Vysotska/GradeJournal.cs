using System;
using System.Collections.Generic;
using System.Linq;

public class GradeJournal
{
    private Dictionary<string, double> _subjects = new Dictionary<string, double>();

    public void AddGrade(string subject, double grade)
    {
        if (grade < 0 || grade > 100) return;
        _subjects[subject] = grade;
    }

    public double CalculateAverage()
    {
        if (_subjects.Count == 0) return 0;
        return Math.Round(_subjects.Values.Average(), 2);
    }

    public Dictionary<string, double> GetAllGrades() => new Dictionary<string, double>(_subjects);
}