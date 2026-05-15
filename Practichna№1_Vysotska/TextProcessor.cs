using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class TextProcessor
{
    public string Reverse(string input)
    {
        char[] arr = input.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public int CountWords(string text)
    {
        return text.Split(' ',
            StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public int CountCharacters(string text, bool ignoreWhitespace = true)
    {
        if (ignoreWhitespace)
            text = text.Replace(" ", "");

        return text.Length;
    }

    public string Normalize(string text)
    {
        return Regex.Replace(text.Trim(), @"\s+", " ");
    }

    public bool IsPalindrome(string text,
        bool ignoreCase = true,
        bool ignoreSpaces = true)
    {
        if (ignoreCase)
            text = text.ToLower();

        if (ignoreSpaces)
            text = text.Replace(" ", "");

        string reversed = Reverse(text);

        return text == reversed;
    }

    public string ReplaceMultiple(string text,
        Dictionary<string, string> replacements)
    {
        foreach (var item in replacements)
        {
            text = text.Replace(item.Key, item.Value);
        }

        return text;
    }

    public string[] SplitIntoSentences(string text)
    {
        return text.Split(new char[] { '.', '!', '?' },
            StringSplitOptions.RemoveEmptyEntries);
    }

    public string BuildGroupReport(StudentGroup group)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("=== ЗВІТ ГРУПИ ===");
        sb.AppendLine($"Кількість: {group.GroupSize}");
        sb.AppendLine($"Середній бал: {group.AverageGroupGrade}");

        foreach (var s in group.GetAllStudents())
        {
            sb.AppendLine(s.GetFormattedInfo());
        }

        return sb.ToString();
    }

    public string ComparePerformance(int iterations)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        string text = "";

        for (int i = 0; i < iterations; i++)
        {
            text += i;
        }

        sw.Stop();

        long stringTime = sw.ElapsedMilliseconds;

        sw.Restart();

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            sb.Append(i);
        }

        sw.Stop();

        long sbTime = sw.ElapsedMilliseconds;

        return $"String: {stringTime} ms\nStringBuilder: {sbTime} ms";
    }
}