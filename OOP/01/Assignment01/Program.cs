using System;
using System.Globalization;

namespace OOP_Assignment01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1. WeekDays Enum
            Console.WriteLine("\n# WeekDays Enum:");
            foreach (var day in Enum.GetValues(typeof(WeekDays)))
                Console.WriteLine(day);
            #endregion

            #region 2. Person Struct Array
            Console.WriteLine("\n# Person Struct Array:");
            Person[] people = new Person[3];
            people[0] = new Person("Ahmed", 22);
            people[1] = new Person("Mona", 28);
            people[2] = new Person("Samy", 25);
            foreach (var person in people)
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
            #endregion

            #region 3. Season Enum with Months
            Console.Write("\nEnter season name: ");
            string seasonInput = Console.ReadLine();
            if (Enum.TryParse(seasonInput, true, out Season season))
            {
                switch (season)
                {
                    case Season.Spring: Console.WriteLine("March to May"); break;
                    case Season.Summer: Console.WriteLine("June to August"); break;
                    case Season.Autumn: Console.WriteLine("September to November"); break;
                    case Season.Winter: Console.WriteLine("December to February"); break;
                }
            }
            else Console.WriteLine("Invalid season");
            #endregion

            #region 4. Permissions Enum
            Console.WriteLine("\n# Permissions Enum:");
            Permissions userPermissions = Permissions.Read | Permissions.Write;
            Console.WriteLine($"Initial: {userPermissions}");
            userPermissions |= Permissions.Execute;
            Console.WriteLine($"After Add Execute: {userPermissions}");
            userPermissions &= ~Permissions.Write;
            Console.WriteLine($"After Remove Write: {userPermissions}");
            Console.WriteLine($"Has Delete? {userPermissions.HasFlag(Permissions.Delete)}");
            #endregion

            #region 5. Colors Enum
            Console.Write("\nEnter a color: ");
            string colorInput = Console.ReadLine();
            if (Enum.TryParse(colorInput, true, out Colors color))
            {
                Console.WriteLine("Primary color");
            }
            else Console.WriteLine("Not a primary color");
            #endregion

            #region 6. Point Struct and Distance
            Console.WriteLine("\n# Distance Between Two Points:");
            Point p1 = new Point(1, 1);
            Point p2 = new Point(4, 5);
            double distance = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            Console.WriteLine($"Distance: {distance:F2}");
            #endregion

            #region 7. Oldest Person
            Console.WriteLine("\n# Oldest Person:");
            Person[] persons = new Person[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Enter name of person {i + 1}: ");
                string name = Console.ReadLine();
                Console.Write($"Enter age of {name}: ");
                int age = int.Parse(Console.ReadLine());
                persons[i] = new Person(name, age);
            }
            Person oldest = persons[0];
            foreach (var p in persons)
                if (p.Age > oldest.Age) oldest = p;
            Console.WriteLine($"Oldest: {oldest.Name}, Age: {oldest.Age}");
            #endregion

            #region Part 02: Encapsulation
            Console.WriteLine("\n# Employee Info:");
            Employee[] EmpArr = new Employee[3];
            EmpArr[0] = new Employee(1, "Ahmed", Gender.M, SecurityLevel.DBA, 15000, new HireDate(1, 1, 2020));
            EmpArr[1] = new Employee(2, "Sara", Gender.F, SecurityLevel.Guest, 5000, new HireDate(5, 5, 2022));
            EmpArr[2] = new Employee(3, "Omar", Gender.M, SecurityLevel.Developer | SecurityLevel.DBA | SecurityLevel.Secretary | SecurityLevel.Guest, 20000, new HireDate(3, 3, 2018));

            foreach (var emp in EmpArr)
                Console.WriteLine(emp);
            #endregion
        }

        enum WeekDays { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }
        enum Season { Spring, Summer, Autumn, Winter }
        enum Permissions { Read = 1, Write = 2, Delete = 4, Execute = 8 }
        enum Colors { Red, Green, Blue }
        enum Gender { M, F }
        [Flags]
        enum SecurityLevel { Guest = 1, Developer = 2, Secretary = 4, DBA = 8 }

        struct Person
        {
            public string Name;
            public int Age;
            public Person(string name, int age) => (Name, Age) = (name, age);
        }

        struct Point
        {
            public double X, Y;
            public Point(double x, double y) => (X, Y) = (x, y);
        }

        struct HireDate
        {
            public int Day, Month, Year;
            public HireDate(int d, int m, int y) => (Day, Month, Year) = (d, m, y);
            public override string ToString() => $"{Day:D2}/{Month:D2}/{Year}";
        }

        class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Gender Gender { get; set; }
            public SecurityLevel SecurityLevel { get; set; }
            public decimal Salary { get; set; }
            public HireDate HireDate { get; set; }

            public Employee(int id, string name, Gender gender, SecurityLevel level, decimal salary, HireDate date)
            {
                ID = id;
                Name = name;
                Gender = gender;
                SecurityLevel = level;
                Salary = salary;
                HireDate = date;
            }

            public override string ToString() =>
                $"ID: {ID}, Name: {Name}, Gender: {Gender}, Security: {SecurityLevel}, Salary: {Salary.ToString("C", CultureInfo.CurrentCulture)}, HireDate: {HireDate}";
        }
    }
}
