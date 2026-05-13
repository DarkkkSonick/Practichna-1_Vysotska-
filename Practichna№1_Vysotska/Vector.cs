public class Vector
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public static Vector operator +(Vector v1, Vector v2) => new Vector { X = v1.X + v2.X, Y = v1.Y + v2.Y, Z = v1.Z + v2.Z };
    public static Vector operator -(Vector v1, Vector v2) => new Vector { X = v1.X - v2.X, Y = v1.Y - v2.Y, Z = v1.Z - v2.Z };
    public static double operator *(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static bool operator ==(Vector v1, Vector v2) => v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
    public static bool operator !=(Vector v1, Vector v2) => !(v1 == v2);

    public static bool operator >(Vector v1, Vector v2) => (double)v1 > (double)v2;
    public static bool operator <(Vector v1, Vector v2) => (double)v1 < (double)v2;

    public static Vector operator ++(Vector v) => new Vector { X = v.X + 1, Y = v.Y + 1, Z = v.Z + 1 };
    public static Vector operator --(Vector v) => new Vector { X = v.X - 1, Y = v.Y - 1, Z = v.Z - 1 };

    public static explicit operator double(Vector v) => Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);

    public override bool Equals(object obj) => obj is Vector v && this == v;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}