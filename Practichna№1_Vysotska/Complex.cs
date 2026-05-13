using System;

public class Complex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex() { }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
    }

    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
    }

    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex(
            c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
            c1.Real * c2.Imaginary + c1.Imaginary * c2.Real
        );
    }

    public static Complex operator /(Complex c1, Complex c2)
    {
        double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
        if (denominator == 0) throw new DivideByZeroException("Ділення на нуль (модуль дільника дорівнює 0)");

        return new Complex(
            (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator,
            (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator
        );
    }

    public static bool operator ==(Complex c1, Complex c2)
    {
        if (ReferenceEquals(c1, c2)) return true;
        if (c1 is null || c2 is null) return false;
        return c1.Real == c2.Real && c1.Imaginary == c2.Imaginary;
    }

    public static bool operator !=(Complex c1, Complex c2) => !(c1 == c2);

    public override bool Equals(object obj) => obj is Complex c && this == c;

    public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

    public override string ToString()
    {
        if (Imaginary >= 0)
            return $"{Real} + {Imaginary}i";
        return $"{Real} - {Math.Abs(Imaginary)}i";
    }
}