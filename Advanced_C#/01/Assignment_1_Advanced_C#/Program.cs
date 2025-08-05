using System;
using System.Collections.Generic;

namespace Assignment01_AdvancedCS
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Optimized Bubble Sort
            Console.WriteLine("\n--- Optimized Bubble Sort ---");
            int[] arr = { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine("Original Array: " + string.Join(", ", arr));

            OptimizedBubbleSort(arr);
            Console.WriteLine("Sorted Array: " + string.Join(", ", arr));
            #endregion

            #region Generic Range<T>
            Console.WriteLine("\n--- Generic Range<T> ---");
            var intRange = new Range<int>(10, 20);
            Console.WriteLine($"Is 15 in range (10, 20)? {intRange.IsInRange(15)}");
            Console.WriteLine($"Length of range (10, 20): {intRange.Length()}");

            var doubleRange = new Range<double>(1.5, 3.7);
            Console.WriteLine($"Is 2.5 in range (1.5, 3.7)? {doubleRange.IsInRange(2.5)}");
            Console.WriteLine($"Length of range (1.5, 3.7): {doubleRange.Length()}");
            #endregion

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void OptimizedBubbleSort(int[] array)
        {
            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }
    }

    public class Range<T> where T : IComparable<T>
    {
        private T MinValue { get; }
        private T MaxValue { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Min value should not be greater than max value.");

            MinValue = min;
            MaxValue = max;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0;
        }

        public dynamic Length()
        {
            return (dynamic)MaxValue - (dynamic)MinValue;
        }
    }
}
