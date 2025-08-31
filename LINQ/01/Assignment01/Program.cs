using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customerList = ListGenerator.CustomerList;
            List<Product> productList = ListGenerator.ProductList;

            #region LINQ - Element Operators

            // 1. Get first Product out of Stock
            var firstOutOfStock = productList.FirstOrDefault(p => p.UnitsInStock == 0);
            if (firstOutOfStock != null)
                Console.WriteLine($"First product out of stock: ID={firstOutOfStock.ProductID}, Name={firstOutOfStock.ProductName}, Category={firstOutOfStock.Category}, Price={firstOutOfStock.UnitPrice:C}, Stock={firstOutOfStock.UnitsInStock}");
            else
                Console.WriteLine("No product out of stock.");

            // 2. First product price > 1000 or null
            var expensiveProduct = productList.FirstOrDefault(p => p.UnitPrice > 1000);
            if (expensiveProduct != null)
                Console.WriteLine($"First product with price > 1000: ID={expensiveProduct.ProductID}, Name={expensiveProduct.ProductName}, Price={expensiveProduct.UnitPrice:C}");
            else
                Console.WriteLine("No product with price > 1000.");

            // 3. Retrieve the second number greater than 5
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThan5 = arr.Where(x => x > 5).Skip(1).FirstOrDefault();
            Console.WriteLine("Second number > 5: " + secondGreaterThan5);

            #endregion

            #region LINQ - Aggregate Operators

            // 1. Count odd numbers
            int oddCount = arr.Count(x => x % 2 == 1);
            Console.WriteLine("Odd numbers count: " + oddCount);

            // 2. Customers and how many orders each has
            Console.WriteLine("\nCustomers and Orders Count:");
            var customerOrders = customerList.Select(c => new { c.CustomerName, OrdersCount = c.Orders.Length });
            foreach (var c in customerOrders)
                Console.WriteLine($"- {c.CustomerName}: {c.OrdersCount}");

            // 3. Categories and how many products each has
            Console.WriteLine("\nCategories and Products Count:");
            var categoryCount = productList.GroupBy(p => p.Category)
                                           .Select(g => new { Category = g.Key, Count = g.Count() });
            foreach (var c in categoryCount)
                Console.WriteLine($"- {c.Category}: {c.Count}");

            // 4. Sum of numbers
            int sum = arr.Sum();
            Console.WriteLine("\nSum of numbers: " + sum);

            // 5-8 Dictionary file
            Console.WriteLine("\nDictionary File Stats:");
            string filePath = "dictionary_english.txt";
            if (File.Exists(filePath))
            {
                string[] words = File.ReadAllLines(filePath)
                                     .Where(w => !string.IsNullOrWhiteSpace(w))
                                     .ToArray();

                if (words.Length > 0)
                {
                    Console.WriteLine($"- Total words: {words.Length}");
                    Console.WriteLine($"- Total characters: {words.Sum(w => w.Length)}");
                    Console.WriteLine($"- Shortest word length: {words.Min(w => w.Length)}");
                    Console.WriteLine($"- Longest word length: {words.Max(w => w.Length)}");
                    Console.WriteLine($"- Average word length: {words.Average(w => w.Length):F2}");
                }
                else
                {
                    Console.WriteLine("Dictionary file is empty!");
                }
            }
            else
            {
                Console.WriteLine("Dictionary file not found!");
            }

            // 9. Total units in stock per category
            Console.WriteLine("\nStock per Category:");
            var stockPerCategory = productList.GroupBy(p => p.Category)
                                              .Select(g => new { g.Key, TotalStock = g.Sum(p => p.UnitsInStock) });
            foreach (var s in stockPerCategory)
                Console.WriteLine($"- {s.Key}: {s.TotalStock}");

            // 10. Cheapest price per category
            Console.WriteLine("\nCheapest Price per Category:");
            var cheapestPerCategory = productList.GroupBy(p => p.Category)
                                                 .Select(g => new { g.Key, MinPrice = g.Min(p => p.UnitPrice) });
            foreach (var s in cheapestPerCategory)
                Console.WriteLine($"- {s.Key}: {s.MinPrice:C}");

            // 11. Products with cheapest price in each category
            Console.WriteLine("\nCheapest Products per Category:");
            var cheapestProducts = from p in productList
                                   group p by p.Category into g
                                   let minPrice = g.Min(p => p.UnitPrice)
                                   from p in g
                                   where p.UnitPrice == minPrice
                                   select new { g.Key, p.ProductName, p.UnitPrice };

            foreach (var p in cheapestProducts)
                Console.WriteLine($"- {p.Key}: {p.ProductName} ({p.UnitPrice:C})");

            // 12. Most expensive price per category
            Console.WriteLine("\nMost Expensive Price per Category:");
            var maxPerCategory = productList.GroupBy(p => p.Category)
                                            .Select(g => new { g.Key, MaxPrice = g.Max(p => p.UnitPrice) });
            foreach (var s in maxPerCategory)
                Console.WriteLine($"- {s.Key}: {s.MaxPrice:C}");

            // 13. Products with most expensive price per category
            Console.WriteLine("\nMost Expensive Products per Category:");
            var mostExpensiveProducts = from p in productList
                                        group p by p.Category into g
                                        let maxPrice = g.Max(p => p.UnitPrice)
                                        from p in g
                                        where p.UnitPrice == maxPrice
                                        select new { g.Key, p.ProductName, p.UnitPrice };
            foreach (var p in mostExpensiveProducts)
                Console.WriteLine($"- {p.Key}: {p.ProductName} ({p.UnitPrice:C})");

            // 14. Average price per category
            Console.WriteLine("\nAverage Price per Category:");
            var avgPerCategory = productList.GroupBy(p => p.Category)
                                            .Select(g => new { g.Key, AvgPrice = g.Average(p => p.UnitPrice) });
            foreach (var s in avgPerCategory)
                Console.WriteLine($"- {s.Key}: {s.AvgPrice:C}");

            #endregion

            #region LINQ - Ordering Operators

            // 1. Sort products by name
            Console.WriteLine("\nProducts sorted by Name:");
            var productsByName = productList.OrderBy(p => p.ProductName);
            foreach (var p in productsByName)
                Console.WriteLine($"- {p.ProductName}");

            // 2. Case-insensitive sort of words
            Console.WriteLine("\nWords sorted (case-insensitive):");
            string[] arrWords = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedInsensitive = arrWords.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedInsensitive)
                Console.WriteLine($"- {w}");

            // 3. Sort products by stock (descending)
            Console.WriteLine("\nProducts sorted by UnitsInStock (DESC):");
            var stockDesc = productList.OrderByDescending(p => p.UnitsInStock);
            foreach (var p in stockDesc)
                Console.WriteLine($"- {p.ProductName} (Stock: {p.UnitsInStock})");

            // 4. Sort digits by length then alphabetically
            Console.WriteLine("\nDigits sorted by length then alphabetically:");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var digitsSorted = digits.OrderBy(d => d.Length).ThenBy(d => d);
            foreach (var d in digitsSorted)
                Console.WriteLine($"- {d}");

            // 5. Sort words by length then case-insensitive
            Console.WriteLine("\nWords sorted by length then case-insensitive:");
            var wordsSorted = arrWords.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in wordsSorted)
                Console.WriteLine($"- {w}");

            // 6. Sort products by category then price (descending)
            Console.WriteLine("\nProducts sorted by Category then Price (DESC):");
            var productsCatPrice = productList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            foreach (var p in productsCatPrice)
                Console.WriteLine($"- {p.Category}: {p.ProductName} ({p.UnitPrice:C})");

            // 7. Sort words by length then case-insensitive descending
            Console.WriteLine("\nWords sorted by length then case-insensitive (DESC):");
            var wordsSortedDesc = arrWords.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in wordsSortedDesc)
                Console.WriteLine($"- {w}");

            // 8. Digits with second letter = 'i', reversed
            Console.WriteLine("\nDigits where 2nd letter = 'i' (Reversed):");
            var digitsSecondI = digits.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            foreach (var d in digitsSecondI)
                Console.WriteLine($"- {d}");

            #endregion

            #region LINQ - Transformation Operators

            // 1. Names of products
            Console.WriteLine("\nProduct Names:");
            var productNames = productList.Select(p => p.ProductName);
            foreach (var name in productNames)
                Console.WriteLine($"- {name}");

            // 2. Uppercase + lowercase versions
            Console.WriteLine("\nUpper + Lower of words:");
            string[] words2 = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var upperLower = words2.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            foreach (var w in upperLower)
                Console.WriteLine($"- {w.Upper} | {w.Lower}");

            // 3. Products with renamed Price
            Console.WriteLine("\nProducts with renamed Price:");
            var productInfo = productList.Select(p => new { p.ProductName, Price = p.UnitPrice, p.Category });
            foreach (var p in productInfo)
                Console.WriteLine($"- {p.ProductName}, Category: {p.Category}, Price: {p.Price:C}");

            // 4. Numbers equal to index
            Console.WriteLine("\nNumbers equal to their index:");
            int[] arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var matchIndex = arr2.Select((num, index) => new { Num = num, Index = index })
                                 .Where(x => x.Num == x.Index);
            foreach (var x in matchIndex)
                Console.WriteLine($"- Num {x.Num} = Index {x.Index}");

            // 5. All pairs numbersA < numbersB
            Console.WriteLine("\nAll pairs (a < b):");
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA
                        from b in numbersB
                        where a < b
                        select new { a, b };
            foreach (var pair in pairs)
                Console.WriteLine($"- ({pair.a}, {pair.b})");

            // 6. Orders total < 500
            Console.WriteLine("\nOrders with Total < 500:");
            var ordersLess500 = customerList.SelectMany(c => c.Orders)
                                            .Where(o => o.Total < 500);
            foreach (var o in ordersLess500)
                Console.WriteLine($"- OrderID: {o.OrderID}, Total: {o.Total}");

            // 7. Orders made in 1998 or later
            Console.WriteLine("\nOrders from 1998 or later:");
            var ordersAfter1998 = customerList.SelectMany(c => c.Orders)
                                              .Where(o => o.OrderDate.Year >= 1998);
            foreach (var o in ordersAfter1998)
                Console.WriteLine($"- OrderID: {o.OrderID}, Date: {o.OrderDate.ToShortDateString()}, Total: {o.Total}");

            #endregion

        }
    }
}
