using System;

namespace Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1 - Check if divisible by 3 and 4
            Console.WriteLine("Q1: Enter a number:");
            int num1 = int.Parse(Console.ReadLine());

            if (num1 % 3 == 0 && num1 % 4 == 0)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
            #endregion

            Console.WriteLine("-----------------------------");

            #region Q2 - Positive or Negative
            Console.WriteLine("Q2: Enter an integer:");
            int num2 = int.Parse(Console.ReadLine());

            if (num2 < 0)
                Console.WriteLine("Negative");
            else
                Console.WriteLine("Positive");
            #endregion

            Console.WriteLine("-----------------------------");

            #region Q3 - Max and Min of 3 numbers
            Console.WriteLine("Q3: Enter 3 integers separated by space:");
            string[] inputs = Console.ReadLine().Split();
            int a = int.Parse(inputs[0]);
            int b = int.Parse(inputs[1]);
            int c = int.Parse(inputs[2]);

            int max = Math.Max(a, Math.Max(b, c));
            int min = Math.Min(a, Math.Min(b, c));

            Console.WriteLine($"Max element = {max}");
            Console.WriteLine($"Min element = {min}");
            #endregion

            Console.WriteLine("-----------------------------");

            #region Q4 - Even or Odd
            Console.WriteLine("Q4: Enter an integer:");
            int num4 = int.Parse(Console.ReadLine());

            if (num4 % 2 == 0)
                Console.WriteLine("Even");
            else
                Console.WriteLine("Odd");
            #endregion

            Console.WriteLine("-----------------------------");

            #region Q5 - Vowel or Consonant
            Console.WriteLine("Q5: Enter a character:");
            char ch = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if ("aeiou".Contains(ch))
                Console.WriteLine("Vowel");
            else
                Console.WriteLine("Consonant");
            #endregion

            Console.WriteLine("-----------------------------");
        }
    }
}
