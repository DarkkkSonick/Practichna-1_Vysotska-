using System;

public abstract class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public Vehicle(string b, string m) { Brand = b; Model = m; }
    public abstract void Move();
}

public class Car : Vehicle
{
    public Car(string b, string m) : base(b, m) { }
    public override void Move() => Console.WriteLine($"Авто {Brand} {Model} їде.");
}

public class Truck : Vehicle
{
    public Truck(string b, string m) : base(b, m) { }
    public override void Move() => Console.WriteLine($"Вантажівка {Brand} {Model} везе вантаж.");
}

public class Bus : Vehicle
{
    public Bus(string b, string m) : base(b, m) { }
    public override void Move() => Console.WriteLine($"Автобус {Brand} {Model} везе людей.");
}

public class ElectricCar : Car
{
    public ElectricCar(string b, string m) : base(b, m) { }
    public override void Move() => Console.WriteLine($"Електрокар {Brand} {Model} їде тихо.");
}