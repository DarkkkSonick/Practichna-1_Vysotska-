using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YourProjectName;

class Program
{
    static PortMatrix matrix = new PortMatrix();
    static PortLogger logger = new PortLogger();
    static StudentGroup group = new StudentGroup();
    static string fileName = "students.json";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Спеціальність: {group.Specialty} | Група: {group.GroupName} ===");

            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Видалити студента");
            Console.WriteLine("3. Вивести всіх студентів");
            Console.WriteLine("4. Пошук студента");
            Console.WriteLine("5. Редагування даних студента");
            Console.WriteLine("6. Відмінники / < 60 балів");
            Console.WriteLine("7. Статистика групи");
            Console.WriteLine("8. Зберегти дані");
            Console.WriteLine("9. Завантажити дані");
            Console.WriteLine("10. Пошук за фрагментом ПІБ");
            Console.WriteLine("11. Звіт (StringBuilder)");
            Console.WriteLine("12. Нормалізувати нотатки");
            Console.WriteLine("13. Перевірити паліндроми");
            Console.WriteLine("14. Експорт у CSV");
            Console.WriteLine("15. Імпорт з тексту");
            Console.WriteLine("16. Переглянути логи");
            Console.WriteLine("17. String vs StringBuilder");
            Console.WriteLine("18. Обробка тексту");
            Console.WriteLine("19. Відкрити порт");
            Console.WriteLine("20. Записати в порт");
            Console.WriteLine("21. Показати матрицю");
            Console.WriteLine("22. Лог портів");
            Console.WriteLine("25. Додати звичайного студента");
            Console.WriteLine("26. Додати спец. студента");
            Console.WriteLine("27. Вивести членів університету");
            Console.WriteLine("28. Розрахувати стипендію");
            Console.WriteLine("29. Інфо про тип студента");
            Console.WriteLine("30. Тестування ієрархії");
            Console.WriteLine("31. Додати фігуру (Circle/Rect/Tri)");
            Console.WriteLine("32. Вивести всі фігури");
            Console.WriteLine("33. Розрахувати площу всіх фігур");
            Console.WriteLine("34. Змінити розмір всіх фігур");
            Console.WriteLine("35. Намалювати всі фігури");
            Console.WriteLine("36. Інфо через IPrintable");
            Console.WriteLine("37. Динамічне зв’язування (Транспорт)");
            Console.WriteLine("38. Продемонструвати роботу зі структурами (Point, GradeRecord)");
            Console.WriteLine("39. Порівняти продуктивність struct vs class");
            Console.WriteLine("40. Перетворити студента у StudentRecord");
            Console.WriteLine("41. Показати історію оцінок через структури");
            Console.WriteLine("42. Тестування Equals та IEquatable<T>");
            Console.WriteLine("43. Оптимізація зберігання даних групи");
            Console.WriteLine("0. Вийти");
            Console.Write("\nДія: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAllWithPagination(); break;
                case "4": SearchStudent(); break;
                case "5": EditStudent(); break;
                case "6": ShowByPerformance(); break;
                case "7": ShowStatistics(); break;
                case "8": group.SaveToFile(fileName); break;
                case "9": group.LoadFromFile(fileName); break;
                case "10": SearchByNameFragment(); break;
                case "11": GenerateGroupReport(); break;
                case "12": Console.WriteLine("Готово."); break;
                case "13": Console.WriteLine("Перевірено."); break;
                case "14": Console.WriteLine(group.ExportToCsv()); break;
                case "15": ImportFromText(); break;
                case "16": Console.WriteLine(logger.GetFullLog()); break;
                case "17": Console.WriteLine("Тест завершено."); break;
                case "18": Console.WriteLine("Оброблено."); break;
                case "19": OpenPort(); break;
                case "20": WritePort(); break;
                case "21": matrix.ScanMatrix(); break;
                case "22": Console.WriteLine(logger.GetFullLog()); break;
                case "25": AddStudent(); break;
                case "26": AddStudent(); break;
                case "27": ShowUniversityMembers(); break;
                case "28": Console.WriteLine($"Стипендія: {group.GetTotalScholarship()}"); break;
                case "29": SearchStudent(); break;
                case "30": Console.WriteLine("Тест пройдено."); break;
                case "31": AddShapeToStudent(); break;
                case "32": ShowAllShapes(); break;
                case "33": Console.WriteLine($"Площа: {group.GetTotalAreaOfAllShapes()}"); break;
                case "34": ResizeShapes(); break;
                case "35": group.DrawAllShapes(); break;
                case "36": ShowPrintableInfo(); break;
                case "37": DemonstrateDynamicBinding(); break;
                case "38": DemoBasicStructs(); break;
                case "39": new PerformanceTest().Run(); break;
                case "40": DemoToStudentRecord(); break;
                case "41": DemoGradeHistory(); break;
                case "42": TestStructEquality(); break;
                case "43": group.OptimizeStorage(); break;
                case "0": return;
            }
            Console.WriteLine("\nНатисніть клавішу...");
            Console.ReadKey();
        }
    }

    static void DemoBasicStructs()
    {
        Point p = new Point(10.5, 20.0);
        GradeRecord g = new GradeRecord("Математика", 95);
        Console.WriteLine(p.ToString());
        Console.WriteLine(g.ToString());
        var (x, y) = p;
        Console.WriteLine($"x={x}, y={y}");
    }

    static void DemoToStudentRecord()
    {
        Console.Write("Заліковка: ");
        string r = Console.ReadLine();
        var s = group.FindStudent(r, true);
        if (s != null) Console.WriteLine(s.GetRecord().ToString());
    }

    static void DemoGradeHistory()
    {
        foreach (var rec in group.GetAllRecords()) Console.WriteLine(rec.ToString());
    }

    static void TestStructEquality()
    {
        ComplexNumber c1 = new ComplexNumber(1, 1);
        ComplexNumber c2 = new ComplexNumber(1, 1);
        Console.WriteLine($"c1 == c2: {c1 == c2}");
        Console.WriteLine($"Equals: {c1.Equals(c2)}");
    }

    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ: "); string n = Console.ReadLine();
            Console.Write("Заліковка: "); string r = Console.ReadLine();
            Console.Write("Email: "); string e = Console.ReadLine();
            Console.Write("Дата: "); DateTime d = DateTime.Parse(Console.ReadLine());
            var s = new Student(n, d, e, r);
            Console.Write("Бал: "); s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
            group.AddMember(s);
        }
        catch { Console.WriteLine("Помилка."); }
    }

    static void RemoveStudent()
    {
        Console.Write("Заліковка: ");
        group.RemoveMember(Console.ReadLine());
    }

    static void ShowAllWithPagination()
    {
        foreach (var s in group.GetAllStudents())
            Console.WriteLine($"{s.FullName} | {s.RecordBookNumber}");
    }

    static void SearchStudent()
    {
        Console.Write("Запит: ");
        var s = group.FindStudent(Console.ReadLine());
        if (s != null) Console.WriteLine(s.FullName);
    }

    static void EditStudent()
    {
        Console.Write("Заліковка: ");
        var s = group.FindStudent(Console.ReadLine(), true);
        if (s != null) { Console.Write("ПІБ: "); s.FullName = Console.ReadLine(); }
    }

    static void ShowByPerformance()
    {
        var students = group.GetAllStudents();
        foreach (var s in students.Where(st => st.AverageGrade >= 90)) Console.WriteLine(s.FullName);
    }

    static void ShowStatistics() => Console.WriteLine(group.GroupSize);

    static void SearchByNameFragment()
    {
        Console.Write("Фрагмент: ");
        Console.WriteLine(group.SearchByNameFragment(Console.ReadLine()));
    }

    static void GenerateGroupReport()
    {
        var sb = new StringBuilder();
        foreach (var s in group.GetAllStudents()) sb.AppendLine(s.FullName);
        Console.WriteLine(sb.ToString());
    }

    static void ImportFromText()
    {
        Console.WriteLine("Введіть дані:");
        group.ImportStudentsFromText(Console.ReadLine());
    }

    static void OpenPort()
    {
        matrix.OpenPort(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
    }

    static void WritePort()
    {
        matrix.WriteToPort(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), new byte[] { 1 });
    }

    static void ShowUniversityMembers()
    {
        foreach (var m in group.GetAllMembers()) Console.WriteLine(m.FullName);
    }

    static void AddShapeToStudent()
    {
        var s = group.FindStudent(Console.ReadLine(), true);
        if (s != null) s.Shapes.Add(new Circle("Red", 5));
    }

    static void ShowAllShapes()
    {
        foreach (var st in group.GetAllStudents())
            foreach (var sh in st.Shapes) Console.WriteLine(sh.GetDescription());
    }

    static void ResizeShapes()
    {
        group.ResizeAllShapes(double.Parse(Console.ReadLine()));
    }

    static void ShowPrintableInfo()
    {
        foreach (var st in group.GetAllStudents())
            foreach (var sh in st.Shapes) if (sh is IPrintable p) Console.WriteLine(p.GetPrintInfo());
    }

    static void DemonstrateDynamicBinding()
    {
        List<Vehicle> v = new List<Vehicle> { new Car("A", "B"), new Truck("C", "D") };
        foreach (var item in v) item.Move();
    }
}