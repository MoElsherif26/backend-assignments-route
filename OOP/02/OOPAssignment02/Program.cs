using System;
using System.Collections.Generic;
using System.Linq;

#region First Project - Point3D
class Point3D : IComparable<Point3D>, ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point3D() : this(0, 0, 0) { }
    public Point3D(int x, int y) : this(x, y, 0) { }
    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"Point Coordinates: ({X}, {Y}, {Z})";
    }

    public static bool operator ==(Point3D p1, Point3D p2)
    {
        return p1.Equals(p2);
    }
    public static bool operator !=(Point3D p1, Point3D p2)
    {
        return !p1.Equals(p2);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point3D p)
            return X == p.X && Y == p.Y && Z == p.Z;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public int CompareTo(Point3D other)
    {
        if (X != other.X) return X.CompareTo(other.X);
        return Y.CompareTo(other.Y);
    }

    public object Clone()
    {
        return new Point3D(X, Y, Z);
    }
}
#endregion

#region Second Project - Static Maths
static class Maths
{
    public static int Add(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
    public static double Divide(int a, int b) => b == 0 ? throw new DivideByZeroException() : (double)a / b;
}
#endregion

#region Third Project - Duration
class Duration
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    public Duration(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        Normalize();
    }

    public Duration(int totalSeconds)
    {
        Hours = totalSeconds / 3600;
        totalSeconds %= 3600;
        Minutes = totalSeconds / 60;
        Seconds = totalSeconds % 60;
    }

    private void Normalize()
    {
        int total = ToSeconds();
        Hours = total / 3600;
        total %= 3600;
        Minutes = total / 60;
        Seconds = total % 60;
    }

    public int ToSeconds() => Hours * 3600 + Minutes * 60 + Seconds;

    public override string ToString()
    {
        return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Duration d)
            return this.ToSeconds() == d.ToSeconds();
        return false;
    }

    public override int GetHashCode() => ToSeconds();

    public static Duration operator +(Duration d1, Duration d2)
        => new Duration(d1.ToSeconds() + d2.ToSeconds());

    public static Duration operator +(Duration d1, int seconds)
        => new Duration(d1.ToSeconds() + seconds);

    public static Duration operator +(int seconds, Duration d1)
        => d1 + seconds;

    public static Duration operator -(Duration d1, Duration d2)
        => new Duration(d1.ToSeconds() - d2.ToSeconds());

    public static Duration operator ++(Duration d)
    {
        d.Minutes++;
        d.Normalize();
        return d;
    }

    public static Duration operator --(Duration d)
    {
        d.Minutes--;
        d.Normalize();
        return d;
    }

    public static bool operator >(Duration d1, Duration d2)
        => d1.ToSeconds() > d2.ToSeconds();

    public static bool operator <(Duration d1, Duration d2)
        => d1.ToSeconds() < d2.ToSeconds();

    public static bool operator >=(Duration d1, Duration d2)
        => d1.ToSeconds() >= d2.ToSeconds();

    public static bool operator <=(Duration d1, Duration d2)
        => d1.ToSeconds() <= d2.ToSeconds();

    public static explicit operator DateTime(Duration d)
    {
        return new DateTime().AddSeconds(d.ToSeconds());
    }
}
#endregion

#region Main Entry
class Program
{
    static void Main()
    {
        // First Project: Point3D
        var points = new List<Point3D>();
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Enter X,Y,Z for point {i + 1}: ");
            var input = Console.ReadLine().Split(',');
            if (input.Length == 3 && int.TryParse(input[0], out int x)
                && int.TryParse(input[1], out int y) && int.TryParse(input[2], out int z))
                points.Add(new Point3D(x, y, z));
        }
        Console.WriteLine("Points:");
        foreach (var p in points)
            Console.WriteLine(p);

        Console.WriteLine(points[0] == points[1] ? "Equal" : "Not Equal");
        points.Sort();
        Console.WriteLine("Sorted:");
        points.ForEach(Console.WriteLine);
        var clone = (Point3D)points[0].Clone();
        Console.WriteLine("Cloned: " + clone);

        // Second Project: Maths Static
        Console.WriteLine("Math: Add = " + Maths.Add(5, 3));
        Console.WriteLine("Math: Sub = " + Maths.Subtract(5, 3));
        Console.WriteLine("Math: Mul = " + Maths.Multiply(5, 3));
        Console.WriteLine("Math: Div = " + Maths.Divide(10, 2));

        // Third Project: Duration
        var d1 = new Duration(1, 10, 15);
        var d2 = new Duration(7800);
        var d3 = new Duration(666);
        Console.WriteLine(d1);
        Console.WriteLine(d2);
        Console.WriteLine(d3);

        d3 = d1 + d2;
        Console.WriteLine("D1 + D2 = " + d3);
        d3 = d1 + 7800;
        Console.WriteLine("D1 + 7800 = " + d3);
        d3 = 666 + d3;
        Console.WriteLine("666 + D3 = " + d3);
        d3 = ++d1;
        Console.WriteLine("++D1 = " + d3);
        d3 = --d2;
        Console.WriteLine("--D2 = " + d3);
        d1 = d1 - d2;
        Console.WriteLine("D1 - D2 = " + d1);

        if (d1 > d2) Console.WriteLine("D1 > D2");
        if (d1 <= d2) Console.WriteLine("D1 <= D2");

        DateTime dateTime = (DateTime)d1;
        Console.WriteLine("DateTime from D1 = " + dateTime.ToLongTimeString());
    }
}
#endregion
