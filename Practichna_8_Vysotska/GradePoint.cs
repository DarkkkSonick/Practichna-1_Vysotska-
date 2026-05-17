public class GradePoint
{
    public double Value { get; set; }

    public static bool operator true(GradePoint g) => g.Value >= 8;
    public static bool operator false(GradePoint g) => g.Value < 8;

    public static implicit operator double(GradePoint g) => g.Value;
    public static implicit operator GradePoint(double d) => new GradePoint { Value = d };

    public static GradePoint operator +(GradePoint g1, GradePoint g2) => new GradePoint { Value = g1.Value + g2.Value };
    public static GradePoint operator ++(GradePoint g) => new GradePoint { Value = g.Value + 1 };
    public static GradePoint operator --(GradePoint g) => new GradePoint { Value = g.Value - 1 };

    public static bool operator >(GradePoint g1, GradePoint g2) => g1.Value > g2.Value;
    public static bool operator <(GradePoint g1, GradePoint g2) => g1.Value < g2.Value;
    public static bool operator >=(GradePoint g1, GradePoint g2) => g1.Value >= g2.Value;
    public static bool operator <=(GradePoint g1, GradePoint g2) => g1.Value <= g2.Value;
}