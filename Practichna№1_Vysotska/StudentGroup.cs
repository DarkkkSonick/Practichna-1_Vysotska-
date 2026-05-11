using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class StudentGroup
{
    private List<Student> _students = new List<Student>();
    private PortMatrix portMatrix = new PortMatrix();
    public string GroupName { get; set; }
    public string Specialty { get; set; }
    public int Course { get; set; }

    public int GroupSize => _students.Count;
    public void AssignStudentToPort(Student s, int row, int col)
    {
        s.PortRow = row;
        s.PortCol = col;
    }

    public List<Student> GetStudentsByPortStatus(bool isOpen)
    {
        return _students.Where(s =>
        {
            if (s.PortRow == -1 || s.PortCol == -1)
                return false;

            return isOpen;
        }).ToList();
    }
    public double AverageGroupGrade
    {
        get
        {
            if (_students.Count == 0) return 0;
            return Math.Round(_students.Average(s => s.AverageGrade), 2);
        }
    }

    public void AddStudent(Student s)
    {
        if (s != null)
        {
            _students.Add(s);
        }
    }

    public bool RemoveStudent(string recordBookNumber)
    {
        var student = _students.FirstOrDefault(s => s.RecordBookNumber == recordBookNumber);
        if (student != null)
        {
            _students.Remove(student);
            return true;
        }
        return false;
    }

    public Student FindStudent(string nameOrNumber, bool searchByNumber = false)
    {
        if (searchByNumber)
            return _students.FirstOrDefault(s => s.RecordBookNumber == nameOrNumber);

        return _students.FirstOrDefault(s => s.FullName.Contains(nameOrNumber, StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> GetExcellentStudents()
    {
        return _students.Where(s => s.IsExcellent()).ToList();
    }

    public List<Student> GetStudentsByStatus(Student.StudentStatus status)
    {
        return _students.Where(s => s.Status == status).ToList();
    }

    public List<Student> GetFailingStudents()
    {
        return _students.Where(s => s.IsFailing()).ToList();
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public void SaveToFile(string fileName)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_students, options);
        File.WriteAllText(fileName, jsonString);
    }

    public void LoadFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string jsonString = File.ReadAllText(fileName);
            _students = JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
        }
    }
    
}