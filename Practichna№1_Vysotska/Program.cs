

using System;

class Program
{
    static StudentGroup group = new StudentGroup();
    static TextProcessor tp = new TextProcessor();
    static AdvancedLogger logger = new AdvancedLogger();
    static NotesEditor editor = new NotesEditor();
    static string fileName = "students.json";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ГРУПОЮ (ПР4) ===");
            Console.WriteLine("1. Додати студента           13. Перевірити паліндром");
            Console.WriteLine("2. Видалити студента         14. Експорт у CSV");
            Console.WriteLine("3. Вивести всіх              15. Імпорт студентів");
            Console.WriteLine("4. Пошук студента            16. Переглянути логи");
            Console.WriteLine("5. Редагування балу          17. Порівняти продуктивність");
            Console.WriteLine("6. Відмінники / <60          18. Обробка тексту");
            Console.WriteLine("7. Статистика групи          19. Порівняти студентів (>, <, ==)");
            Console.WriteLine("8. Зберегти дані             20. Об’єднати дві групи (+)");
            Console.WriteLine("9. Завантажити дані          21. Тест класу Vector");
            Console.WriteLine("10. Пошук за фрагментом      22. Тест GradePoint");
            Console.WriteLine("11. Звіт групи               23. Знайти найкращого студента");
            Console.WriteLine("12. Нормалізувати нотатки    24. Тест Complex (Варіант 1)");
            Console.WriteLine("0. Вихід                     199. Notes Editor");

            Console.Write("\nВибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAll(); break;
                case "4": SearchStudent(); break;
                case "5": EditStudent(); break;
                case "6": ShowPerformance(); break;
                case "7": ShowStatistics(); break;
                case "8": group.SaveToFile(fileName); Console.WriteLine("Збережено"); break;
                case "9": group.LoadFromFile(fileName); Console.WriteLine("Завантажено"); break;
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
                case "199": NotesMenu(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір"); break;
            }
            Console.WriteLine("\nНатисніть клавішу...");
            Console.ReadKey();
        }
    }

    // --- Методи порівняння та операторів (Нові) ---

    static void CompareStudents()
    {
        Console.Write("Заліковка 1: ");
        var s1 = group[Console.ReadLine()];
        Console.Write("Заліковка 2: ");
        var s2 = group[Console.ReadLine()];

        if (s1 != null && s2 != null)
        {
            Console.WriteLine($"Результат == : {s1 == s2}");
            Console.WriteLine($"Результат >  : {s1 > s2}");
            Console.WriteLine($"Результат <  : {s1 < s2}");
        }
        else Console.WriteLine("Студентів не знайдено");
    }

    static void MergeGroupsDemo()
    {
        StudentGroup group2 = new StudentGroup { GroupName = "К-322" };
        group2.AddStudent(new Student { FullName = "Іванов Іван Іванович", PersonalEmail = "vanya@mail.com", RecordBookNumber = "11112222", DateOfBirth = new DateTime(2005, 1, 1) });

        StudentGroup merged = group + group2;
        Console.WriteLine($"Нова група: {merged.GroupName}, Кількість: {merged.GroupSize}");
    }

    static void TestVector()
    {
        Vector v1 = new Vector { X = 1, Y = 2, Z = 3 };
        Vector v2 = new Vector { X = 4, Y = 5, Z = 6 };
        Console.WriteLine($"v1 + v2 = {(v1 + v2).X}, {(v1 + v2).Y}, {(v1 + v2).Z}");
        Console.WriteLine($"Скалярний добуток *: {v1 * v2}");
        Console.WriteLine($"Довжина v1 (explicit double): {(double)v1}");
    }

    static void TestGradePoint()
    {
        GradePoint g1 = 9.5;
        GradePoint g2 = 7.0;
        Console.WriteLine($"g1 (9.5) >= 8 (true/false): {(g1 ? "Відмінно" : "Нижче 8")}");
        Console.WriteLine($"g2 (7.0) >= 8 (true/false): {(g2 ? "Відмінно" : "Нижче 8")}");
        double val = g1;
        Console.WriteLine($"Неявне приведення в double: {val}");
    }

    static void ShowBestStudent()
    {
        var best = group.BestStudent();
        if (best != null) Console.WriteLine($"Найкращий студент за балом та прогресом:\n{best.GetFormattedInfo(true)}");
        else Console.WriteLine("Група порожня");
    }

    static void TestComplex()
    {
        Complex c1 = new Complex { Real = 1, Imaginary = 2 };
        Complex c2 = new Complex { Real = 3, Imaginary = 4 };
        Console.WriteLine($"Сума комплексних: {c1 + c2}");
        Console.WriteLine($"Множення комплексних: {c1 * c2}");
    }

    // --- Твої існуючі методи (ПР1-3) ---

    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ: "); string name = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Заліковка: "); string rec = Console.ReadLine();
            Console.Write("Дата (yyyy-mm-dd): "); DateTime dob = DateTime.Parse(Console.ReadLine());

            var s = new Student { FullName = name, PersonalEmail = email, RecordBookNumber = rec, DateOfBirth = dob, EnrollmentDate = DateTime.Now };
            Console.Write("Бал: "); s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
            Console.Write("Прогрес: "); s.CourseProgress = int.Parse(Console.ReadLine());

            group.AddStudent(s);
            logger.Log("INFO", $"Додано: {name}");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    static void RemoveStudent()
    {
        Console.Write("Заліковка: ");
        if (group.RemoveStudent(Console.ReadLine())) Console.WriteLine("Видалено");
        else Console.WriteLine("Не знайдено");
    }

    static void ShowAll() { foreach (var s in group.GetAllStudents()) Console.WriteLine(s.GetFormattedInfo(true)); }

    static void SearchStudent()
    {
        Console.Write("Запит: ");
        string q = Console.ReadLine();
        var s = group.FindStudent(q) ?? group.FindStudent(q, true);
        if (s != null) Console.WriteLine(s.GetFormattedInfo(true));
        else Console.WriteLine("Не знайдено");
    }

    static void EditStudent()
    {
        Console.Write("Заліковка: ");
        var s = group[Console.ReadLine()];
        if (s != null)
        {
            Console.Write("Новий бал: "); s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
            Console.WriteLine("Оновлено");
        }
    }

    static void ShowPerformance()
    {
        Console.WriteLine("\nВідмінники:");
        foreach (var s in group.GetExcellentStudents()) Console.WriteLine(s.FullName);
        Console.WriteLine("\n<60:");
        foreach (var s in group.GetFailingStudents()) Console.WriteLine(s.FullName);
    }

    static void ShowStatistics()
    {
        Console.WriteLine($"Кількість: {group.GroupSize}");
        Console.WriteLine($"Сер. бал: {group.AverageGroupGrade}");
    }

    static void SearchFragment() { Console.WriteLine(group.SearchByNameFragment(Console.ReadLine())); }

    static void NormalizeNotes() { foreach (var s in group.GetAllStudents()) s.Notes = tp.Normalize(s.Notes); }

    static void CheckPalindrome() { foreach (var s in group.GetAllStudents()) Console.WriteLine($"{s.FullName}: {tp.IsPalindrome(s.Notes)}"); }

    static void ImportStudents() { group.ImportStudentsFromText(Console.ReadLine()); }

    static void TextTools()
    {
        string text = Console.ReadLine();
        Console.WriteLine($"Реверс: {tp.Reverse(text)}");
        Console.WriteLine($"Слів: {tp.CountWords(text)}");
    }

    static void NotesMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Записати 2. Undo 0. Назад");
            string c = Console.ReadLine();
            if (c == "1") editor.Write(Console.ReadLine());
            else if (c == "2") editor.Undo();
            else if (c == "0") return;
        }
    }
}