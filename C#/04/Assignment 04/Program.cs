using System;

namespace Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q6 - Print numbers from 1 to n
            Console.WriteLine("Q6: Enter a number:");
            int q6 = int.Parse(Console.ReadLine());
            for (int i = 1; i <= q6; i++)
                Console.Write(i + " ");
            Console.WriteLine();
            #endregion

            #region Q7 - Multiplication Table to 12
            Console.WriteLine("Q7: Enter a number:");
            int q7 = int.Parse(Console.ReadLine());
            for (int i = 1; i <= 12; i++)
                Console.Write(q7 * i + " ");
            Console.WriteLine();
            #endregion

            #region Q8 - Even numbers between 1 and n
            Console.WriteLine("Q8: Enter a number:");
            int q8 = int.Parse(Console.ReadLine());
            for (int i = 2; i <= q8; i += 2)
                Console.Write(i + " ");
            Console.WriteLine();
            #endregion

            #region Q9 - Power of a number
            Console.WriteLine("Q9: Enter base and exponent:");
            int b = int.Parse(Console.ReadLine());
            int exp = int.Parse(Console.ReadLine());
            int result = 1;
            for (int i = 0; i < exp; i++)
                result *= b;
            Console.WriteLine("Result: " + result);
            #endregion

            #region Q10 - Marks calculation
            Console.WriteLine("Q10: Enter 5 subject marks:");
            string[] marksInput = Console.ReadLine().Split();
            int total = 0;
            foreach (var m in marksInput)
                total += int.Parse(m);
            double avg = total / 5.0;
            double percent = avg;
            Console.WriteLine("Total marks = " + total);
            Console.WriteLine("Average Marks = " + avg);
            Console.WriteLine("Percentage = " + percent);
            #endregion

            #region Q11 - Days in a month
            Console.WriteLine("Q11: Enter month number (1-12):");
            int month = int.Parse(Console.ReadLine());
            int days = month switch
            {
                1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
                4 or 6 or 9 or 11 => 30,
                2 => 28,
                _ => -1
            };
            Console.WriteLine(days == -1 ? "Invalid month" : $"Days in Month: {days}");
            #endregion

            #region Q12 - Simple Calculator
            Console.WriteLine("Q12: Enter two numbers and operation (+ - * /):");
            double num1 = double.Parse(Console.ReadLine());
            double num2 = double.Parse(Console.ReadLine());
            char op = Console.ReadLine()[0];
            double calcResult = op switch
            {
                '+' => num1 + num2,
                '-' => num1 - num2,
                '*' => num1 * num2,
                '/' => num2 != 0 ? num1 / num2 : double.NaN,
                _ => double.NaN
            };
            Console.WriteLine("Result: " + calcResult);
            #endregion

            #region Q13 - Reverse a string
            Console.WriteLine("Q13: Enter a string:");
            string inputStr = Console.ReadLine();
            char[] arr = inputStr.ToCharArray();
            Array.Reverse(arr);
            Console.WriteLine("Reversed: " + new string(arr));
            #endregion

            #region Q14 - Reverse an integer
            Console.WriteLine("Q14: Enter an integer:");
            int number = int.Parse(Console.ReadLine());
            int reversed = 0;
            while (number != 0)
            {
                reversed = reversed * 10 + number % 10;
                number /= 10;
            }
            Console.WriteLine("Reversed number: " + reversed);
            #endregion

            #region Q15 - Prime numbers in range
            Console.WriteLine("Q15: Enter start and end of range:");
            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());
            Console.WriteLine("Prime numbers:");
            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
            Console.WriteLine();
            #endregion

            #region Q17 - Check if 3 points are on a straight line
            Console.WriteLine("Q17: Enter (x1 y1 x2 y2 x3 y3):");
            var p = Console.ReadLine().Split();
            double x1 = double.Parse(p[0]), y1 = double.Parse(p[1]);
            double x2 = double.Parse(p[2]), y2 = double.Parse(p[3]);
            double x3 = double.Parse(p[4]), y3 = double.Parse(p[5]);
            bool collinear = (y2 - y1) * (x3 - x2) == (y3 - y2) * (x2 - x1);
            Console.WriteLine(collinear ? "Points are on a straight line" : "Points are not on a straight line");
            #endregion

            #region Q18 - Worker Efficiency
            Console.WriteLine("Q18: Enter time taken by worker in hours:");
            double time = double.Parse(Console.ReadLine());
            if (time >= 2 && time <= 3)
                Console.WriteLine("Highly efficient");
            else if (time > 3 && time <= 4)
                Console.WriteLine("Increase speed");
            else if (time > 4 && time <= 5)
                Console.WriteLine("Training required");
            else if (time > 5)
                Console.WriteLine("Leave the company");
            else
                Console.WriteLine("Invalid input");
            #endregion
        }

        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0)
                    return false;
            return true;
        }
    }
}
