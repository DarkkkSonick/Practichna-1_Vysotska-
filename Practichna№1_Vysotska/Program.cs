using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("3. Вивести всіх студентів (пагінація)");
            Console.WriteLine("4. Пошук студента");
            Console.WriteLine("5. Редагування даних");
            Console.WriteLine("6. Відмінники / < 60 балів");
            Console.WriteLine("7. Статистика групи");
            Console.WriteLine("8. Зберегти дані");
            Console.WriteLine("9. Завантажити дані");
            Console.WriteLine("10. Відкрити порт");
            Console.WriteLine("11. Записати дані в порт");
            Console.WriteLine("12. Показати матрицю");
            Console.WriteLine("13. Лог портів");
            Console.WriteLine("0. Вийти");
            Console.Write("\nОберіть дію: ");

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
                case "10":OpenPort();break;
                case "11":WritePort();break;
                case "12":matrix.ScanMatrix();break;
                case "13":Console.WriteLine(logger.GetFullLog());break;
                case "0": return;
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }

    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ (мін. 5 симв.): ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Номер заліковки (8 цифр): ");
            string record = Console.ReadLine();
            Console.Write("Дата народження (рррр-мм-дд): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());

            var student = new Student
            {
                FullName = name,
                PersonalEmail = email,
                RecordBookNumber = record,
                DateOfBirth = dob,
                EnrollmentDate = DateTime.Now,
                Status = Student.StudentStatus.Active
            };

            Console.Write("Середній бал: ");
            student.UpdateAverageGrade(double.Parse(Console.ReadLine()));

            group.AddStudent(student);
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    static void ShowAllWithPagination()
    {
        var all = group.GetAllStudents();
        int pageSize = 10;
        for (int i = 0; i < all.Count; i += pageSize)
        {
            var page = all.Skip(i).Take(pageSize);
            foreach (var s in page) s.ShowDetailedInfo();

            if (i + pageSize < all.Count)
            {
                Console.WriteLine("Натисніть клавішу для наступної сторінки...");
                Console.ReadKey();
            }
        }
    }

    static void SearchStudent()
    {
        Console.Write("Введіть ПІБ або номер заліковки: ");
        string query = Console.ReadLine();
        var s = group.FindStudent(query) ?? group.FindStudent(query, true);
        if (s != null) s.ShowDetailedInfo();
        else Console.WriteLine("Студента не знайдено.");
    }

    static void EditStudent()
    {
        Console.Write("Введіть номер заліковки для редагування: ");
        string record = Console.ReadLine();
        var s = group.FindStudent(record, true);
        if (s != null)
        {
            Console.Write("Новий ПІБ (або порожньо): ");
            string n = Console.ReadLine();
            if (!string.IsNullOrEmpty(n)) s.FullName = n;

            Console.Write("Новий бал: ");
            s.UpdateAverageGrade(double.Parse(Console.ReadLine()));
        }
    }

    static void ShowByPerformance()
    {
        Console.WriteLine("\n--- Відмінники ---");
        foreach (var s in group.GetExcellentStudents()) s.ShowDetailedInfo();
        Console.WriteLine("\n--- Мають < 60 балів ---");
        foreach (var s in group.GetFailingStudents()) s.ShowDetailedInfo();
    }

    static void ShowStatistics()
    {
        int excellentCount = group.GetExcellentStudents().Count;
        double percent = group.GroupSize > 0 ? (double)excellentCount / group.GroupSize * 100 : 0;

        Console.WriteLine($"Кількість студентів: {group.GroupSize}");
        Console.WriteLine($"Середній бал групи: {group.AverageGroupGrade}");
        Console.WriteLine($"% відмінників: {percent:F2}%");
    }

    static void RemoveStudent()
    {
        Console.Write("Номер заліковки для видалення: ");
        string record = Console.ReadLine();
        if (group.RemoveStudent(record)) Console.WriteLine("Видалено.");
        else Console.WriteLine("Не знайдено.");
    }
        static void OpenPort()
        {
            Console.Write("Row: ");
            int r = int.Parse(Console.ReadLine());

            Console.Write("Col: ");
            int c = int.Parse(Console.ReadLine());

            matrix.OpenPort(r, c);

            logger.LogOperation("OPEN", r * 16 + c, "Порт відкрито");
        }

        static void WritePort()
        {
            Console.Write("Row: ");
            int r = int.Parse(Console.ReadLine());

            Console.Write("Col: ");
            int c = int.Parse(Console.ReadLine());

            byte[] data = { 1, 2, 3, 4 };

            matrix.WriteToPort(r, c, data);

            logger.LogOperation("WRITE", r * 16 + c, "Записані дані");
        }
}
// Фінальна перевірка ієрархії
