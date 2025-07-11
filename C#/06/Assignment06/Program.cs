using System;

namespace Assignment06_Functions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1 - Value Type: by value vs by reference
            Console.WriteLine("Q1 - Value Type:");
            int a = 5, b = 10;
            Console.WriteLine($"Before => a = {a}, b = {b}");
            PassByValue(a, b);
            Console.WriteLine($"After PassByValue => a = {a}, b = {b}");
            PassByRef(ref a, ref b);
            Console.WriteLine($"After PassByRef => a = {a}, b = {b}");

            void PassByValue(int x, int y)
            {
                x += 5;
                y += 5;
            }

            void PassByRef(ref int x, ref int y)
            {
                x += 5;
                y += 5;
            }
            #endregion

            #region Q2 - Reference Type: by value vs by reference
            Console.WriteLine("\nQ2 - Reference Type:");
            int[] numbers = { 1, 2 };
            Console.WriteLine("Before => " + string.Join(",", numbers));
            ChangeByValue(numbers);
            Console.WriteLine("After ChangeByValue => " + string.Join(",", numbers));
            ChangeByRef(ref numbers);
            Console.WriteLine("After ChangeByRef => " + string.Join(",", numbers));

            void ChangeByValue(int[] arr)
            {
                arr[0] = 10;
            }

            void ChangeByRef(ref int[] arr)
            {
                arr = new int[] { 99, 100 };
            }
            #endregion

            #region Q3 - Sum and Subtract Function
            Console.WriteLine("\nQ3 - Enter 4 numbers:");
            int x1 = int.Parse(Console.ReadLine());
            int x2 = int.Parse(Console.ReadLine());
            int x3 = int.Parse(Console.ReadLine());
            int x4 = int.Parse(Console.ReadLine());

            (int sum, int diff) = SumAndSubtract(x1, x2, x3, x4);
            Console.WriteLine($"Sum = {sum}, Subtract = {diff}");

            (int, int) SumAndSubtract(int a, int b, int c, int d)
            {
                return (a + b, c - d);
            }
            #endregion

            #region Q4 - Sum of Digits
            Console.WriteLine("\nQ4 - Enter a number:");
            int digitNum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Sum of digits = {SumDigits(digitNum)}");

            int SumDigits(int number)
            {
                int sum = 0;
                while (number != 0)
                {
                    sum += number % 10;
                    number /= 10;
                }
                return sum;
            }
            #endregion

            #region Q5 - IsPrime Function
            Console.WriteLine("\nQ5 - Enter a number to check if prime:");
            int primeCheck = int.Parse(Console.ReadLine());
            Console.WriteLine(IsPrime(primeCheck) ? "Prime" : "Not Prime");

            bool IsPrime(int num)
            {
                if (num <= 1) return false;
                for (int i = 2; i <= Math.Sqrt(num); i++)
                    if (num % i == 0) return false;
                return true;
            }
            #endregion

            #region Q6 - MinMaxArray (ref)
            Console.WriteLine("\nQ6 - Enter array elements:");
            int[] minmaxArr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int min = 0, max = 0;
            MinMaxArray(minmaxArr, ref min, ref max);
            Console.WriteLine($"Min = {min}, Max = {max}");

            void MinMaxArray(int[] arr, ref int min, ref int max)
            {
                min = arr[0];
                max = arr[0];
                foreach (var item in arr)
                {
                    if (item < min) min = item;
                    if (item > max) max = item;
                }
            }
            #endregion

            #region Q7 - Iterative Factorial
            Console.WriteLine("\nQ7 - Enter number for factorial:");
            int factNum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Factorial = {Factorial(factNum)}");

            long Factorial(int n)
            {
                long result = 1;
                for (int i = 2; i <= n; i++)
                    result *= i;
                return result;
            }
            #endregion

            #region Q8 - ChangeChar in string
            Console.WriteLine("\nQ8 - Enter string:");
            string inputStr = Console.ReadLine();
            Console.WriteLine("Enter position and new char:");
            int pos = int.Parse(Console.ReadLine());
            char newChar = char.Parse(Console.ReadLine());
            Console.WriteLine("Modified string: " + ChangeChar(inputStr, pos, newChar));

            string ChangeChar(string str, int position, char ch)
            {
                if (position < 0 || position >= str.Length)
                    return str;
                return str.Substring(0, position) + ch + str.Substring(position + 1);
            }
            #endregion
        }
    }
}
