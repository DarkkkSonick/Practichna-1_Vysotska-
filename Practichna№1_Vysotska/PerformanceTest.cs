using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace YourProjectName
{
    public class PerformanceTest
    {
        private const int Count = 100_000;

        public void Run()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memoryBeforeClasses = GC.GetTotalMemory(true);

            Stopwatch sw = Stopwatch.StartNew();
            Student[] classes = new Student[Count];
            for (int i = 0; i < Count; i++)
            {
                classes[i] = new Student($"Student {i}", DateTime.Now, "test@mail.com", i.ToString("D8"));
            }
            sw.Stop();
            long timeClassCreate = sw.ElapsedMilliseconds;
            long memoryAfterClasses = GC.GetTotalMemory(true) - memoryBeforeClasses;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memoryBeforeStructs = GC.GetTotalMemory(true);

            sw.Restart();
            StudentRecord[] structs = new StudentRecord[Count];
            for (int i = 0; i < Count; i++)
            {
                structs[i] = new StudentRecord(i, $"Student {i}");
            }
            sw.Stop();
            long timeStructCreate = sw.ElapsedMilliseconds;
            long memoryAfterStructs = GC.GetTotalMemory(true) - memoryBeforeStructs;

            sw.Restart();
            var sortedClasses = classes.OrderBy(s => s.FullName).ToArray();
            sw.Stop();
            long timeClassSort = sw.ElapsedMilliseconds;

            sw.Restart();
            var sortedStructs = structs.OrderBy(s => s.FullName).ToArray();
            sw.Stop();
            long timeStructSort = sw.ElapsedMilliseconds;

            PrintResults(timeClassCreate, timeStructCreate, timeClassSort, timeStructSort, memoryAfterClasses, memoryAfterStructs);
        }

        private void PrintResults(long tcc, long tsc, long tcs, long tss, long mc, long ms)
        {
            Console.WriteLine("\n--- Порівняння продуктивності (100,000 елементів) ---");
            Console.WriteLine("| Операція       | Class (мс) | Struct (мс) | Різниця |");
            Console.WriteLine("|----------------|------------|-------------|---------|");
            Console.WriteLine($"| Заповнення     | {tcc,10} | {tsc,11} | {(double)tcc / tsc:F2}x |");
            Console.WriteLine($"| Сортування     | {tcs,10} | {tss,11} | {(double)tcs / tss:F2}x |");
            Console.WriteLine("|----------------|------------|-------------|---------|");
            Console.WriteLine($"| Пам'ять (МБ)   | {mc / 1024.0 / 1024.0,10:F2} | {ms / 1024.0 / 1024.0,11:F2} | {(double)mc / ms:F2}x |");
        }
    }
}