using System;
using System.Linq;

namespace Assignment05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q19 - Identity Matrix
            Console.WriteLine("Q19 - Enter size of identity matrix:");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write((i == j ? 1 : 0) + " ");
                Console.WriteLine();
            }
            #endregion

            #region Q20 - Sum of Array Elements
            Console.WriteLine("\nQ20 - Enter array elements separated by space:");
            int[] arr20 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int sum = arr20.Sum();
            Console.WriteLine($"Sum = {sum}");
            #endregion

            #region Q21 - Merge Two Sorted Arrays
            Console.WriteLine("\nQ21 - Enter first sorted array:");
            int[] a1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine("Enter second sorted array:");
            int[] a2 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] merged = a1.Concat(a2).OrderBy(x => x).ToArray();
            Console.WriteLine("Merged Array: " + string.Join(" ", merged));
            #endregion

            #region Q22 - Frequency of Elements
            Console.WriteLine("\nQ22 - Enter array elements:");
            int[] arr22 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var grouped = arr22.GroupBy(x => x);
            foreach (var group in grouped)
                Console.WriteLine($"Value {group.Key} => Frequency: {group.Count()}");
            #endregion

            #region Q23 - Max and Min in Array
            Console.WriteLine("\nQ23 - Enter array elements:");
            int[] arr23 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine($"Max = {arr23.Max()}, Min = {arr23.Min()}");
            #endregion

            #region Q24 - Second Largest
            Console.WriteLine("\nQ24 - Enter array elements:");
            int[] arr24 = Console.ReadLine().Split().Select(int.Parse).Distinct().OrderByDescending(x => x).ToArray();
            Console.WriteLine(arr24.Length >= 2 ? $"Second Largest = {arr24[1]}" : "Not enough distinct elements.");
            #endregion

            #region Q25 - Longest Distance Between Equal Values
            Console.WriteLine("\nQ25 - Enter array:");
            int[] arr25 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int maxDistance = 0;
            Dictionary<int, int> firstIndices = new();
            for (int i = 0; i < arr25.Length; i++)
            {
                if (!firstIndices.ContainsKey(arr25[i]))
                    firstIndices[arr25[i]] = i;
                else
                    maxDistance = Math.Max(maxDistance, i - firstIndices[arr25[i]]);
            }
            Console.WriteLine($"Longest Distance = {maxDistance}");
            #endregion

            #region Q26 - Reverse Words in Sentence
            Console.WriteLine("\nQ26 - Enter a sentence:");
            string sentence = Console.ReadLine();
            string reversedWords = string.Join(" ", sentence.Split(' ').Reverse());
            Console.WriteLine(reversedWords);
            #endregion

            #region Q27 - Copy 2D Array
            Console.WriteLine("\nQ27 - Copy from 2D Array:");
            Console.Write("Enter rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Enter cols: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] arr1 = new int[rows, cols];
            int[,] arr2 = new int[rows, cols];

            Console.WriteLine("Enter elements for first array:");
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Element [{i},{j}]: ");
                    arr1[i, j] = int.Parse(Console.ReadLine());
                    arr2[i, j] = arr1[i, j];
                }

            Console.WriteLine("Copied Second Array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write(arr2[i, j] + " ");
                Console.WriteLine();
            }
            #endregion

            #region Q28 - Reverse One-Dimensional Array
            Console.WriteLine("\nQ28 - Enter array to reverse:");
            int[] arr28 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine("Reversed Array: " + string.Join(" ", arr28.Reverse()));
            #endregion

            Console.WriteLine("\n✔️ All Questions Completed!");
        }
    }
}
