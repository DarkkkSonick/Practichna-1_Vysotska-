using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

public class StudentGroup
{
    private List<UniversityMember> _members = new List<UniversityMember>();

    public string GroupName { get; set; } = "К-321";
    public string Specialty { get; set; } = "Комп'ютерна інженерія";

    public int GroupSize => _members.Count;

    public double AverageGroupGrade
    {
        get
        {
            var students = _members.OfType<Student>().ToList();
            return students.Count == 0 ? 0 : Math.Round(students.Average(s => s.AverageGrade), 2);
        }
    }

    public void AddMember(UniversityMember member)
    {
        _members.Add(member);
    }

    public bool RemoveMember(string recordNumber)
    {
        var member = _members.OfType<Student>().FirstOrDefault(s => s.RecordBookNumber == recordNumber);
        if (member != null)
        {
            _members.Remove(member);
            return true;
        }
        return false;
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