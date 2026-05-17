using System;

namespace YourProjectName
{
    public readonly struct Point : IEquatable<Point>
    {
        public double X { get; init; }
        public double Y { get; init; }

        public Point(double x, double y) => (X, Y) = (x, y);

        public override bool Equals(object? obj) => obj is Point other && Equals(other);
        public bool Equals(Point other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Point left, Point right) => left.Equals(right);
        public static bool operator !=(Point left, Point right) => !left.Equals(right);

        public override string ToString() => $"Point({X}, {Y})";
        public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
    }

    public readonly struct GradeRecord : IEquatable<GradeRecord>
    {
        public string Subject { get; init; }
        public int Score { get; init; }

        public GradeRecord(string subject, int score)
        {
            Subject = subject;
            Score = score;
        }

        public override bool Equals(object? obj) => obj is GradeRecord other && Equals(other);
        public bool Equals(GradeRecord other) => Subject == other.Subject && Score == other.Score;
        public override int GetHashCode() => HashCode.Combine(Subject, Score);

        public static bool operator ==(GradeRecord left, GradeRecord right) => left.Equals(right);
        public static bool operator !=(GradeRecord left, GradeRecord right) => !left.Equals(right);

        public override string ToString() => $"[{Subject}]: {Score}";
        public void Deconstruct(out string subject, out int score) => (subject, score) = (Subject, Score);
    }

    public readonly struct StudentRecord : IEquatable<StudentRecord>
    {
        public int Id { get; init; }
        public string FullName { get; init; }

        public StudentRecord(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public override bool Equals(object? obj) => obj is StudentRecord other && Equals(other);
        public bool Equals(StudentRecord other) => Id == other.Id && FullName == other.FullName;
        public override int GetHashCode() => HashCode.Combine(Id, FullName);

        public static bool operator ==(StudentRecord left, StudentRecord right) => left.Equals(right);
        public static bool operator !=(StudentRecord left, StudentRecord right) => !left.Equals(right);

        public override string ToString() => $"ID: {Id}, Student: {FullName}";
        public void Deconstruct(out int id, out string name) => (id, name) = (Id, FullName);
    }

    public readonly struct ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Real { get; init; }
        public double Imaginary { get; init; }

        public ComplexNumber(double real, double imaginary) => (Real, Imaginary) = (real, imaginary);

        public override bool Equals(object? obj) => obj is ComplexNumber other && Equals(other);
        public bool Equals(ComplexNumber other) => Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

        public static bool operator ==(ComplexNumber left, ComplexNumber right) => left.Equals(right);
        public static bool operator !=(ComplexNumber left, ComplexNumber right) => !left.Equals(right);

        public override string ToString() => $"{Real} + {Imaginary}i";
        public void Deconstruct(out double real, out double imaginary) => (real, imaginary) = (Real, Imaginary);
    }
    public readonly struct DateRange : IEquatable<DateRange>
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        public DateRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                Start = end;
                End = start;
            }
            else
            {
                Start = start;
                End = end;
            }
        }

        public bool Equals(DateRange other) => Start == other.Start && End == other.End;

        public override bool Equals(object? obj) => obj is DateRange other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Start, End);

        public static bool operator ==(DateRange left, DateRange right) => left.Equals(right);

        public static bool operator !=(DateRange left, DateRange right) => !left.Equals(right);

        public static bool operator >(DateRange left, DateRange right) => (left.End - left.Start) > (right.End - right.Start);

        public static bool operator <(DateRange left, DateRange right) => (left.End - left.Start) < (right.End - right.Start);

        public override string ToString() => $"{Start:dd.MM.yyyy} - {End:dd.MM.yyyy}";

        public void Deconstruct(out DateTime start, out DateTime end)
        {
            start = Start;
            end = End;
        }
    }
}