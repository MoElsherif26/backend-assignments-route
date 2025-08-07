using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedCSharpAssignment02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1 - Reverse ArrayList In-Place
            ArrayList arrayList = new ArrayList() { 1, 2, 3, 4, 5 };
            ReverseArrayList(arrayList);
            Console.WriteLine("Reversed ArrayList:");
            foreach (var item in arrayList)
                Console.Write(item + " ");
            Console.WriteLine();
            #endregion

            #region Q2 - Filter Even Numbers
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            List<int> evenNumbers = GetEvenNumbers(numbers);
            Console.WriteLine("Even Numbers: " + string.Join(", ", evenNumbers));
            #endregion

            #region Q3 - FixedSizeList<T>
            FixedSizeList<string> fixedList = new FixedSizeList<string>(3);
            fixedList.Add("A");
            fixedList.Add("B");
            fixedList.Add("C");
            try { fixedList.Add("D"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            try { Console.WriteLine(fixedList.Get(2)); Console.WriteLine(fixedList.Get(5)); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            #endregion

            #region Q4 - Count Greater Than X
            int[] arr = { 11, 5, 3 };
            int[] queries = { 1, 5, 13 };
            foreach (int x in queries)
            {
                int count = CountGreaterThanX(arr, x);
                Console.WriteLine(count);
            }
            #endregion

            #region Q5 - Palindrome Array
            int[] arr2 = { 1, 3, 2, 3, 1 };
            Console.WriteLine(IsPalindrome(arr2) ? "YES" : "NO");
            #endregion

            #region Q6 - Remove Duplicates
            int[] arr3 = { 1, 2, 2, 3, 4, 4, 5 };
            int[] noDuplicates = RemoveDuplicates(arr3);
            Console.WriteLine("Without Duplicates: " + string.Join(", ", noDuplicates));
            #endregion

            #region Q7 - Remove Odd Numbers
            ArrayList listWithOdds = new ArrayList() { 1, 2, 3, 4, 5, 6 };
            RemoveOddNumbers(listWithOdds);
            Console.WriteLine("Without Odd Numbers: " + string.Join(", ", listWithOdds.ToArray()));
            #endregion
        }

        static void ReverseArrayList(ArrayList list)
        {
            int left = 0, right = list.Count - 1;
            while (left < right)
            {
                var temp = list[left];
                list[left] = list[right];
                list[right] = temp;
                left++;
                right--;
            }
        }

        static List<int> GetEvenNumbers(List<int> input)
        {
            List<int> result = new List<int>();
            foreach (var num in input)
            {
                if (num % 2 == 0)
                    result.Add(num);
            }
            return result;
        }

        class FixedSizeList<T>
        {
            private T[] items;
            private int count;

            public FixedSizeList(int capacity)
            {
                items = new T[capacity];
                count = 0;
            }

            public void Add(T item)
            {
                if (count >= items.Length)
                    throw new InvalidOperationException("List is full. Cannot add more items.");
                items[count++] = item;
            }

            public T Get(int index)
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Invalid index.");
                return items[index];
            }
        }

        static int CountGreaterThanX(int[] arr, int x)
        {
            int count = 0;
            foreach (var num in arr)
            {
                if (num > x)
                    count++;
            }
            return count;
        }

        static bool IsPalindrome(int[] arr)
        {
            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                if (arr[left] != arr[right])
                    return false;
                left++;
                right--;
            }
            return true;
        }

        static int[] RemoveDuplicates(int[] arr)
        {
            HashSet<int> set = new HashSet<int>(arr);
            return new List<int>(set).ToArray();
        }

        static void RemoveOddNumbers(ArrayList list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if ((int)list[i] % 2 != 0)
                    list.RemoveAt(i);
            }
        }
    }
}
