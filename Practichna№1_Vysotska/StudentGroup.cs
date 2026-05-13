using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;

public class StudentGroup
{
    private List<Student> _students = new List<Student>();

    public string GroupName { get; set; } = "К-321";
    public string Specialty { get; set; } = "Комп'ютерна інженерія";

    public int GroupSize => _students.Count;

    public double AverageGroupGrade =>
        _students.Count == 0 ? 0 :
        Math.Round(_students.Average(s => s.AverageGrade), 2);

    public Student this[string recordNumber]
    {
        get => _students.FirstOrDefault(s => s.RecordBookNumber == recordNumber);
    }

    public static StudentGroup operator +(StudentGroup g1, StudentGroup g2)
    {
        var merged = new StudentGroup
        {
            GroupName = $"{g1.GroupName}+{g2.GroupName}",
            Specialty = g1.Specialty
        };

        foreach (var s in g1.GetAllStudents()) merged.AddStudent(s);
        foreach (var s in g2.GetAllStudents()) merged.AddStudent(s);

        return merged;
    }

    public void AddStudent(Student s)
    {
        _students.Add(s);
    }

    public bool RemoveStudent(string record)
    {
        var st = _students.FirstOrDefault(s => s.RecordBookNumber == record);
        if (st != null)
        {
            _students.Remove(st);
            return true;
        }
        return false;
    }

    public Student FindStudent(string query, bool byNumber = false)
    {
        if (byNumber)
            return _students.FirstOrDefault(s => s.RecordBookNumber == query);

        return _students.FirstOrDefault(s =>
            s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> GetAllStudents() => _students;

    public List<Student> GetExcellentStudents()
    {
        return _students.Where(s => s.AverageGrade >= 90).ToList();
    }

    public List<Student> GetFailingStudents()
    {
        return _students.Where(s => s.AverageGrade < 60).ToList();
    }

    public Student BestStudent()
    {
        if (_students.Count == 0) return null;

        Student best = _students[0];
        foreach (var s in _students)
        {
            if (s > best) best = s;
        }
        return best;
    }

    public StudentGroup MergeGroups(StudentGroup other)
    {
        return this + other;
    }

    public string SearchByNameFragment(string fragment)
    {
        StringBuilder sb = new StringBuilder();
        var result = _students.Where(s =>
            s.FullName.Contains(fragment, StringComparison.OrdinalIgnoreCase));

        foreach (var s in result)
            sb.AppendLine(s.GetFormattedInfo());

        return sb.ToString();
    }

    public string ExportToCsv()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("ПІБ,Заліковка,Бал");
        foreach (var s in _students)
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
                Student s = new Student
                {
                    FullName = data[0],
                    PersonalEmail = data[1],
                    RecordBookNumber = data[2],
                    DateOfBirth = DateTime.Parse(data[3]),
                    EnrollmentDate = DateTime.Now,
                    Status = Student.StudentStatus.Active
                };
                AddStudent(s);
            }
        }
    }

    public void SaveToFile(string file)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_students, options);
        File.WriteAllText(file, json);
    }

    public void LoadFromFile(string file)
    {
        if (File.Exists(file))
        {
            string json = File.ReadAllText(file);
            _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
    }
}