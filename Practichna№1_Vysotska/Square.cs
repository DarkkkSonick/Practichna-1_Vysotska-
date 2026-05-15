public class Square : Rectangle
{
    public Square(string color, double side) : base(color, side, side)
    {
        Name = "Квадрат";
    }

    public override string GetDescription() => $"Це {Color} квадрат зі стороною {Width}";
}