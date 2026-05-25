using Practichna_1_Vysotska;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using YourProjectName;

public class StudentGroup
{
    private List<UniversityMember> _members = new List<UniversityMember>();
    private GradeRecord[] _gradeHistory;
    private Point[] _labLocations;

    public string GroupName { get; set; } = "К-321";
    public string Specialty { get; set; } = "Комп'ютерна інженерія";

    public int GroupSize => _members.Count;

    public event EventHandler<StudentEventArgs> StudentAdded;
    public event EventHandler<StudentEventArgs> StudentRemoved;
    public event EventHandler<GroupReportEventArgs> ReportGenerated;
    public event EventHandler<GradeChangedEventArgs> GradeChanged;

    public List<UniversityMember> Members
    {
        get => _members;
        set => _members = value;
    }

    public double AverageGroupGrade
    {
        get
        {
            var students = _members.OfType<Student>().ToList();
            return students.Count == 0 ? 0 : Math.Round(students.Average(s => s.AverageGrade), 2);
        }
    }

    public StudentGroup() { }

    public StudentGroup(string groupName)
    {
        GroupName = groupName;
    }

    public void AddMember(UniversityMember member)
    {
        _members.Add(member);
        if (member is Student student)
        {
            student.AverageGradeChanged += OnStudentGradeChanged;
            StudentAdded?.Invoke(this, new StudentEventArgs(student));
        }
    }

    public bool RemoveMember(string recordNumber)
    {
        var student = _members.OfType<Student>().FirstOrDefault(s => s.RecordBookNumber == recordNumber);
        if (student != null)
        {
            student.AverageGradeChanged -= OnStudentGradeChanged;
            _members.Remove(student);
            StudentRemoved?.Invoke(this, new StudentEventArgs(student));
            return true;
        }
        return false;
    }

    private void OnStudentGradeChanged(object sender, GradeChangedEventArgs e)
    {
        GradeChanged?.Invoke(sender, e);
    }

    public List<Student> FilterStudents(Predicate<Student> predicate)
    {
        return _members.OfType<Student>().Where(s => predicate(s)).ToList();
    }

    public void PerformOperationOnStudents(Func<Student, bool> predicate, Action<Student> action)
    {
        foreach (var student in _members.OfType<Student>().Where(predicate))
        {
            action(student);
        }
    }

    public double CalculateMetric(Student student, Func<Student, double> metricFunc)
    {
        return metricFunc(student);
    }

    public string GenerateReportWithFunc(Func<StudentGroup, string> reportFunc)
    {
        string reportContent = reportFunc(this);
        ReportGenerated?.Invoke(this, new GroupReportEventArgs(reportContent));
        return reportContent;
    }

    public void SortStudents(Comparison<Student> comparison)
    {
        var students = _members.OfType<Student>().ToList();
        students.Sort(comparison);
        _members = _members.Where(m => !(m is Student)).Cast<UniversityMember>().Concat(students).ToList();
    }

    public void SortStudentsWithLambda(Func<Student, object> keySelector, bool ascending = true)
    {
        var students = _members.OfType<Student>().ToList();
        students = ascending ? students.OrderBy(keySelector).ToList() : students.OrderByDescending(keySelector).ToList();
        _members = _members.Where(m => !(m is Student)).Cast<UniversityMember>().Concat(students).ToList();
    }

    public void Save(string filePath, StorageFormat format)
    {
        if (format == StorageFormat.Json)
        {
            FileManager.SaveToJson(this, filePath);
        }
        else if (format == StorageFormat.Txt)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GroupName);
            sb.AppendLine(Specialty);
            foreach (var student in _members.OfType<Student>())
            {
                sb.AppendLine($"{student.FullName};{student.DateOfBirth:yyyy-MM-dd};{student.PersonalEmail};{student.RecordBookNumber};{student.AverageGrade};{student.CourseProgress};{student.Status}");
            }
            FileManager.SaveToText(sb.ToString(), filePath);
        }
    }

    public static StudentGroup Load(string filePath, StorageFormat format)
    {
        if (format == StorageFormat.Json)
        {
            return FileManager.LoadFromJson<StudentGroup>(filePath);
        }
        else if (format == StorageFormat.Txt)
        {
            string content = FileManager.ReadFromText(filePath);
            string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2) return null;

            var group = new StudentGroup(lines[0])
            {
                Specialty = lines[1]
            };

            for (int i = 2; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts.Length >= 7)
                {
                    string fullName = parts[0];
                    DateTime dob = DateTime.Parse(parts[1]);
                    string email = parts[2];
                    string recordBook = parts[3];
                    double avgGrade = double.Parse(parts[4]);
                    int progress = int.Parse(parts[5]);
                    Student.StudentStatus status = (Student.StudentStatus)Enum.Parse(typeof(Student.StudentStatus), parts[6]);

                    var student = new Student(fullName, dob, email, recordBook)
                    {
                        CourseProgress = progress,
                        Status = status
                    };
                    student.UpdateAverageGrade(avgGrade);
                    group.AddMember(student);
                }
            }
            return group;
        }
        return null;
    }

    public void ExportGradesToCsv(string filePath)
    {
        FileManager.ExportToCsv(this, filePath);
    }

    public void OptimizeStorage()
    {
        var students = _members.OfType<Student>().ToList();
        _gradeHistory = new GradeRecord[students.Count];
        _labLocations = new Point[students.Count];

        for (int i = 0; i < students.Count; i++)
        {
            _labLocations[i] = new Point(i + 1, 1);
            _gradeHistory[i] = new GradeRecord("Програмування", (int)students[i].AverageGrade);
        }
    }

    public StudentRecord[] GetAllRecords()
    {
        var students = _members.OfType<Student>().ToList();
        StudentRecord[] records = new StudentRecord[students.Count];
        for (int i = 0; i < students.Count; i++)
        {
            records[i] = students[i].GetRecord();
        }
        return records;
    }

    public double GetTotalAreaOfAllShapes()
    {
        double totalArea = 0;
        foreach (var student in _members.OfType<Student>())
        {
            foreach (var shape in student.Shapes)
            {
                totalArea += shape.CalculateArea();
            }
        }
        return Math.Round(totalArea, 2);
    }

    public void DrawAllShapes()
    {
        foreach (var student in _members.OfType<Student>())
        {
            foreach (var shape in student.Shapes)
            {
                if (shape is IDrawable drawable)
                {
                    drawable.Draw();
                }
            }
        }
    }

    public void ResizeAllShapes(double factor)
    {
        foreach (var student in _members.OfType<Student>())
        {
            foreach (var shape in student.Shapes)
            {
                if (shape is IResizable resizable)
                {
                    resizable.Resize(factor);
                }
            }
        }
    }

    public Student FindStudent(string query, bool byNumber = false)
    {
        var students = _members.OfType<Student>();
        if (byNumber)
            return students.FirstOrDefault(s => s.RecordBookNumber == query);

        return students.FirstOrDefault(s => s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> GetAllStudents() => _members.OfType<Student>().ToList();

    public List<UniversityMember> GetAllMembers() => _members;

    public decimal GetTotalScholarship()
    {
        return _members.Sum(m => m.CalculateScholarship());
    }

    public List<T> GetMembersByType<T>() where T : UniversityMember
    {
        return _members.OfType<T>().ToList();
    }

    public string SearchByNameFragment(string fragment)
    {
        StringBuilder sb = new StringBuilder();
        var result = _members.OfType<Student>().Where(s =>
            s.FullName.Contains(fragment, StringComparison.OrdinalIgnoreCase));

        foreach (var s in result)
            sb.AppendLine(s.GetFormattedInfo());

        return sb.ToString();
    }

    public string ExportToCsv()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("ПІБ,Заліковка,Бал");
        foreach (var s in _members.OfType<Student>())
        {
            sb.AppendLine($"{s.FullName},{s.RecordBookNumber},{s.AverageGrade}");
        }
        return sb.ToString();
    }

    public void ImportStudentsFromText(string rawText)
    {
        string[] lines = rawText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            string[] data = line.Split(';');
            if (data.Length >= 4)
            {
                Student s = new Student(data[0], DateTime.Parse(data[3]), data[1], data[2]);
                AddMember(s);
            }
        }
    }

    public void SaveToFile(string file)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_members, options);
        File.WriteAllText(file, json);
    }

    public void LoadFromFile(string file)
    {
        if (File.Exists(file))
        {
            string json = File.ReadAllText(file);
            _members = JsonSerializer.Deserialize<List<UniversityMember>>(json) ?? new List<UniversityMember>();
        }
    }
}