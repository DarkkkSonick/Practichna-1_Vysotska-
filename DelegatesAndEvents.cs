using System;

public class StudentEventArgs : EventArgs
{
    public Student Student { get; }

    public StudentEventArgs(Student student)
    {
        Student = student;
    }
}

public class GroupReportEventArgs : EventArgs
{
    public string ReportContent { get; }

    public GroupReportEventArgs(string reportContent)
    {
        ReportContent = reportContent;
    }
}

public class GradeChangedEventArgs : EventArgs
{
    public double OldGrade { get; }
    public double NewGrade { get; }

    public GradeChangedEventArgs(double oldGrade, double newGrade)
    {
        OldGrade = oldGrade;
        NewGrade = newGrade;
    }
}

public delegate void StudentOperation(Student student);
public delegate void GroupOperation(StudentGroup group);