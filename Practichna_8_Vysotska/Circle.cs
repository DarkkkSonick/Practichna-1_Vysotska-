public class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(string color, double radius) : base("Коло", color)
    {
        Radius = radius;
    }
    public override double CalculateArea() => Math.PI * Math.Pow(Radius, 2);
    public override double CalculatePerimeter() => 2 * Math.PI * Radius;
    public override string GetDescription() => $"Це {Color} коло з радіусом {Radius}";
}