public class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(string color, double a, double b, double c) : base("Трикутник", color)
    {
        SideA = a;
        SideB = b;
        SideC = c;
    }

    public override double CalculateArea()
    {
        double p = CalculatePerimeter() / 2;
        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }

    public override double CalculatePerimeter() => SideA + SideB + SideC;
    public override string GetDescription() => $"Це {Color} трикутник зі сторонами {SideA}, {SideB}, {SideC}";
}