public class Rectangle : Shape, IResizable, IDrawable, IPrintable
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(string color, double width, double height) : base("Прямокутник", color)
    {
        Width = width;
        Height = height;
    }

    public override double CalculateArea() => Width * Height;
    public override double CalculatePerimeter() => 2 * (Width + Height);
    public override string GetDescription() => $"Це {Color} прямокутник {Width}x{Height}";

    public void Resize(double factor)
    {
        Width *= factor;
        Height *= factor;
    }

    public void Draw() => Console.WriteLine($"Малюю прямокутник: {Name}, Колір: {Color}");

    public string GetPrintInfo() => $"{GetDescription()}, Площа: {CalculateArea()}";
}