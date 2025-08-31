using ASSLINQ;

namespace Assignment02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customerList = ListGenerators.CustomerList;
            List<Product> productList = ListGenerators.ProductList;

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

            #region Set Operators

            // 1. Find the unique Category names from Product List
            var uniqueCategories = productList
                .Select(p => p.Category)
                .Distinct();

            Console.WriteLine("Unique Categories:");
            foreach (var cat in uniqueCategories)
                Console.WriteLine(cat);

            // 2. Unique first letter from both product and customer names
            var uniqueFirstLetters = productList.Select(p => p.ProductName[0])
                .Union(customerList.Select(c => c.CustomerName[0]));

            Console.WriteLine("\nUnique first letters from products & customers:");
            foreach (var l in uniqueFirstLetters)
                Console.WriteLine(l);

            // 3. Common first letter from both product and customer names
            var commonFirstLetters = productList.Select(p => p.ProductName[0])
                .Intersect(customerList.Select(c => c.CustomerName[0]));

            Console.WriteLine("\nCommon first letters:");
            foreach (var l in commonFirstLetters)
                Console.WriteLine(l);

            // 4. Product first letters not in customer names
            var productOnlyFirstLetters = productList.Select(p => p.ProductName[0])
                .Except(customerList.Select(c => c.CustomerName[0]));

            Console.WriteLine("\nProduct-only first letters:");
            foreach (var l in productOnlyFirstLetters)
                Console.WriteLine(l);

            // 5. Last 3 characters from all names
            var lastThreeChars = productList.Select(p => p.ProductName.Length >= 3 ?
                                                            p.ProductName[^3..] : p.ProductName)
                .Concat(customerList.Select(c => c.CustomerName.Length >= 3 ?
                                                            c.CustomerName[^3..] : c.CustomerName));

            Console.WriteLine("\nLast 3 chars from all names:");
            foreach (var l in lastThreeChars)
                Console.WriteLine(l);

            #endregion

            #region Quantifiers

            // 1. Check if any word in dictionary contains "ei"
            string[] dictionary = System.IO.File.ReadAllLines("dictionary_english.txt");
            bool containsEi = dictionary.Any(word => word.Contains("ei"));

            Console.WriteLine($"\nAny word contains 'ei'? {containsEi}");

            // 2. Group products by category where at least one product out of stock
            var categoriesWithSomeOutOfStock = productList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));

            Console.WriteLine("\nCategories with at least one product out of stock:");
            foreach (var g in categoriesWithSomeOutOfStock)
            {
                Console.WriteLine($"Category: {g.Key}");
                foreach (var p in g)
                    Console.WriteLine($"  {p.ProductName} ({p.UnitsInStock})");
            }

            // 3. Group products by category where all products are in stock
            var categoriesAllInStock = productList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));

            Console.WriteLine("\nCategories with all products in stock:");
            foreach (var g in categoriesAllInStock)
            {
                Console.WriteLine($"Category: {g.Key}");
                foreach (var p in g)
                    Console.WriteLine($"  {p.ProductName} ({p.UnitsInStock})");
            }

            #endregion

            #region Grouping Operators

            // 1. Partition numbers by remainder mod 5
            List<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groupedNumbers = numbers.GroupBy(n => n % 5);

            foreach (var g in groupedNumbers)
            {
                Console.WriteLine($"\nNumbers with remainder {g.Key} when divided by 5:");
                foreach (var n in g)
                    Console.WriteLine(n);
            }

            // 2. Partition dictionary words by first letter
            var groupedWords = dictionary
                .GroupBy(w => w[0]);

            Console.WriteLine("\nWords grouped by first letter:");
            foreach (var g in groupedWords)
            {
                Console.WriteLine($"Words starting with {g.Key}:");
                foreach (var w in g.Take(5)) // just show 5 for readability
                    Console.WriteLine($"  {w}");
            }

            // 3. Custom comparer: words that have same characters
            string[] Arr = { "from", "salt", "earn", "last", "near", "form" };
            var anagramGroups = Arr.GroupBy(
                word => new string(word.OrderBy(c => c).ToArray())
            );

            Console.WriteLine("\nWords grouped by anagrams:");
            foreach (var g in anagramGroups)
            {
                Console.WriteLine("Group:");
                foreach (var w in g)
                    Console.WriteLine($"  {w}");
            }

            #endregion
        }
    }
}