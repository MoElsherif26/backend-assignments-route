using System;

namespace Assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question 1
            // 1- Write a program that allows the user to enter a number then print it.
            Console.WriteLine("Q1: Enter a number:");
            string number = Console.ReadLine();
            Console.WriteLine("You entered: " + number);
            #endregion

            #region Question 2
            // 2- Convert string with non-numeric characters to int.
            Console.WriteLine("\nQ2: Convert string with letters to int:");
            string input = "123abc";
            try
            {
                int result = int.Parse(input);
                Console.WriteLine(result);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Explanation: You cannot convert non-numeric string to int directly.");
            }
            #endregion

            #region Question 3
            // 3- Perform arithmetic with floats
            float a = 5.5f;
            float b = 2.2f;
            float sum = a + b;
            Console.WriteLine($"\nQ3: {a} + {b} = {sum}");
            // Explanation: Floating point precision may cause small inaccuracies.
            #endregion

            #region Question 4
            // 4- Extract substring
            string text = "Hello, World!";
            string sub = text.Substring(7, 5);
            Console.WriteLine($"\nQ4: Substring = {sub}"); // Output: World
            #endregion

            #region Question 5
            // 5- Value types copy
            int x = 10;
            int y = x;
            y = 20;
            Console.WriteLine($"\nQ5: x = {x}, y = {y}");
            // Explanation: Value types are copied, so x remains unchanged.
            #endregion

            #region Question 6
            // 6- Reference types
            Person p1 = new Person { Name = "Ali" };
            Person p2 = p1;
            p2.Name = "Ahmed";
            Console.WriteLine($"\nQ6: p1.Name = {p1.Name}, p2.Name = {p2.Name}");
            // Explanation: Reference types point to the same object in memory.
            #endregion

            #region Question 7
            // 7- Concatenate two strings
            string firstName = "Mohamed";
            string lastName = "Elsherif";
            string fullName = firstName + " " + lastName;
            Console.WriteLine($"\nQ7: Full Name = {fullName}");
            #endregion

            #region Question 8
            // 8- Convert bool expression to int
            int d;
            d = Convert.ToInt32(!(30 < 20));
            Console.WriteLine($"\nQ8: d = {d}");
            // Explanation: 30 < 20 is false, !false is true, Convert.ToInt32(true) = 1
            // Answer: b) A value 1 will be assigned to d.
            #endregion

            #region Question 9
            // 9- Integer division and modulo
            Console.WriteLine("\nQ9: " + (13 / 2) + " " + (13 % 2));
            // Output: 6 1 — Because it's integer division.
            // Answer: d) 6 1
            #endregion

            #region Question 10
            int num = 1, z = 5;
            if (!(num <= 0))
                Console.WriteLine($"\nQ10: " + (++num + z++) + " " + (++z));
            else
                Console.WriteLine(--num + z-- + " " + --z);
            // Explanation: ++num = 2, z++ = 5 (z becomes 6), ++z = 7 → Output: 7 7
            // Answer: d) 7 7
            #endregion
        }

        class Person
        {
            public string Name;
        }
    }
}
