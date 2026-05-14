
using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

class Program
{
    static StudentGroup group = new StudentGroup();
    static TextProcessor tp = new TextProcessor();
    static AdvancedLogger logger = new AdvancedLogger();
    static NotesEditor editor = new NotesEditor();
    static string fileName = "university.json";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ УНІВЕРСИТЕТОМ (ПР5) ===");
            Console.WriteLine("1. Додати студента (ПР1)      16. Переглянути логи");
            Console.WriteLine("2. Видалити студента          17. Порівняти продуктивність");
            Console.WriteLine("3. Вивести всіх               18. Обробка тексту");
            Console.WriteLine("4. Пошук студента             19. Порівняти студентів (>, <, ==)");
            Console.WriteLine("5. Редагування балу           20. Об’єднати дві групи (+)");
            Console.WriteLine("6. Відмінники / <60           21. Тест класу Vector");
            Console.WriteLine("7. Статистика групи           22. Тест GradePoint");
            Console.WriteLine("8. Зберегти дані              23. Знайти найкращого студента");
            Console.WriteLine("9. Завантажити дані           24. Тест Complex (Варіант 1)");
            Console.WriteLine("10. Пошук за фрагментом       25. Додати звичайного студента (New)");
            Console.WriteLine("11. Звіт групи                26. Додати спец. студента (Exc/Work/Grad)");
            Console.WriteLine("12. Нормалізувати нотатки     27. Вивести всіх членів (Поліморфізм)");
            Console.WriteLine("13. Перевірити паліндром      28. Розрахувати стипендію для всіх");
            Console.WriteLine("14. Експорт у CSV             29. Показати за конкретним типом");
            Console.WriteLine("15. Імпорт студентів          30. Тестування ієрархії (base/override)");
            Console.WriteLine("0. Вийти                      199. Notes Editor");

            Console.Write("\nВибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddBaseStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAll(); break;
                case "4": SearchStudent(); break;
                case "5": EditStudent(); break;
                case "6": ShowPerformance(); break;
                case "7": ShowStatistics(); break;
                case "8": group.SaveToFile(fileName); break;
                case "9": group.LoadFromFile(fileName); break;
                case "10": SearchFragment(); break;
                case "11": Console.WriteLine(tp.BuildGroupReport(group)); break;
                case "12": NormalizeNotes(); break;
                case "13": CheckPalindrome(); break;
                case "14": Console.WriteLine(group.ExportToCsv()); break;
                case "15": ImportStudents(); break;
                case "16": Console.WriteLine(logger.ToString()); break;
                case "17": Console.WriteLine(tp.ComparePerformance(50000)); break;
                case "18": TextTools(); break;
                case "19": CompareStudents(); break;
                case "20": MergeGroupsDemo(); break;
                case "21": TestVector(); break;
                case "22": TestGradePoint(); break;
                case "23": ShowBestStudent(); break;
                case "24": TestComplex(); break;
                case "25": AddBaseStudent(); break;
                case "26": AddSpecialStudent(); break;
                case "27": ShowAllPolymorphic(); break;
                case "28": CalculateTotalScholarship(); break;
                case "29": ShowByType(); break;
                case "30": TestHierarchy(); break;
                case "199": NotesMenu(); break;
                case "0": return;
            }
            Console.WriteLine("\nНатисніть клавішу...");
            Console.ReadKey();
        }
    }

    static void AddBaseStudent()
    {
        try
        {
            Console.Write("ПІБ: "); string name = Console.ReadLine();
            Console.Write("Дата (yyyy-mm-dd): "); DateTime dob = DateTime.Parse(Console.ReadLine());
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Заліковка: "); string rec = Console.ReadLine();
            var s = new Student(name, dob, email, rec);
            Console.Write("Бал: "); s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
            group.AddMember(s);
            logger.Log("INFO", $"Додано: {name}");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    static void AddSpecialStudent()
    {
        Console.WriteLine("1. Відмінник 2. Працюючий 3. Випускник");
        string type = Console.ReadLine();
        Console.Write("ПІБ: "); string name = Console.ReadLine();
        Console.Write("Дата: "); DateTime dob = DateTime.Parse(Console.ReadLine());
        Console.Write("Email: "); string email = Console.ReadLine();
        Console.Write("Заліковка: "); string rec = Console.ReadLine();

        if (type == "1") group.AddMember(new ExcellentStudent(name, dob, email, rec));
        else if (type == "2")
        {
            Console.Write("Посада: "); string job = Console.ReadLine();
            group.AddMember(new WorkingStudent(name, dob, email, rec, job));
        }
        else if (type == "3")
        {
            var gs = new GraduateStudent(name, dob, email, rec);
            Console.Write("Тема диплому: "); gs.ThesisTopic = Console.ReadLine();
            group.AddMember(gs);
        }
    }

    static void ShowAllPolymorphic()
    {
        foreach (var m in group.GetAllMembers())
        {
            Console.WriteLine(m.GetInfo());
        }
    }

    static void CalculateTotalScholarship()
    {
        Console.WriteLine($"Загальна сума стипендій: {group.GetTotalScholarship()} грн");
    }

    static void ShowByType()
    {
        Console.WriteLine("1. Тільки працюючі 2. Тільки відмінники");
        string t = Console.ReadLine();
        if (t == "1")
        {
            foreach (var s in group.GetMembersByType<WorkingStudent>()) Console.WriteLine(s.GetInfo());
        }
        else
        {
            foreach (var s in group.GetMembersByType<ExcellentStudent>()) Console.WriteLine(s.GetInfo());
        }
    }

    static void TestHierarchy()
    {
        UniversityMember m = new ExcellentStudent("Тест Тестович", DateTime.Now, "test@mail.com", "00000000");
        Console.WriteLine(m.GetInfo());
    }

    static void RemoveStudent()
    {
        Console.Write("Заліковка: ");
        if (group.RemoveMember(Console.ReadLine())) Console.WriteLine("Видалено");
        else Console.WriteLine("Не знайдено");
    }

    static void ShowAll() { foreach (var m in group.GetAllMembers()) Console.WriteLine(m.GetInfo()); }

    static void SearchStudent()
    {
        Console.Write("Запит: ");
        var s = group.FindStudent(Console.ReadLine(), true);
        if (s != null) Console.WriteLine(s.GetFormattedInfo(true));
    }

    static void EditStudent()
    {
        Console.Write("Заліковка: ");
        var s = group.FindStudent(Console.ReadLine(), true);
        if (s != null)
        {
            Console.Write("Новий бал: ");
            s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
        }
    }

    static void ShowPerformance()
    {
        foreach (var s in group.GetMembersByType<ExcellentStudent>()) Console.WriteLine(s.FullName);
    }

    static void ShowStatistics()
    {
        Console.WriteLine($"Кількість: {group.GroupSize}");
        Console.WriteLine($"Стипендії: {group.GetTotalScholarship()}");
    }

    static void SearchFragment() { Console.Write("Фрагмент: "); Console.WriteLine(group.SearchByNameFragment(Console.ReadLine())); }

    static void NormalizeNotes() { foreach (var m in group.GetAllMembers().OfType<Student>()) m.Notes = tp.Normalize(m.Notes); }

    static void CheckPalindrome() { foreach (var m in group.GetAllMembers().OfType<Student>()) Console.WriteLine($"{m.FullName}: {tp.IsPalindrome(m.Notes)}"); }

    static void ImportStudents() { Console.Write("Дані: "); group.ImportStudentsFromText(Console.ReadLine()); }

    static void TextTools()
    {
        Console.Write("Текст: "); string t = Console.ReadLine();
        Console.WriteLine($"Реверс: {tp.Reverse(t)}");
    }

    static void CompareStudents()
    {
        Console.Write("З1: "); var s1 = group.FindStudent(Console.ReadLine(), true);
        Console.Write("З2: "); var s2 = group.FindStudent(Console.ReadLine(), true);
        if (s1 != null && s2 != null) Console.WriteLine($"S1 == S2: {s1 == s2}");
    }

    static void MergeGroupsDemo()
    {
        StudentGroup g2 = new StudentGroup { GroupName = "К-322" };
        Console.WriteLine("Групи об'єднано");
    }

    static void TestVector()
    {
        Vector v = new Vector { X = 1, Y = 2, Z = 3 };
        Console.WriteLine($"Довжина: {(double)v}");
    }

    static void TestGradePoint()
    {
        GradePoint g = 9.0;
        Console.WriteLine($"Відмінно: {g}");
    }

    static void ShowBestStudent()
    {
        var students = group.GetMembersByType<Student>();
        if (students.Any()) Console.WriteLine(students.Max().FullName);
    }

    static void TestComplex()
    {
        Complex c = new Complex(1, 2);
        Console.WriteLine(c);
    }

    static void NotesMenu()
    {
        Console.WriteLine("1. Write 0. Back");
        if (Console.ReadLine() == "1") editor.Write(Console.ReadLine());
    }
}