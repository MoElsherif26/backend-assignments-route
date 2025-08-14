namespace Assignment04_AdvancedCSharp
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }

        public Book(string _ISBN, string _Title,
                    string[] _Authors, DateTime _PublicationDate,
                    decimal _Price)
        {
            ISBN = _ISBN;
            Title = _Title;
            Authors = _Authors;
            PublicationDate = _PublicationDate;
            Price = _Price;
        }

        public override string ToString()
        {
            string authorsStr = string.Join(", ", Authors);
            return $"ISBN: {ISBN}, Title: {Title}, Authors: {authorsStr}, " +
                   $"Publication Date: {PublicationDate.ToShortDateString()}, Price: ${Price:F2}";
        }
    }

    public class BookFunctions
    {
        public static string GetTitle(Book B)
        {
            return $"Title: {B.Title}";
        }

        public static string GetAuthors(Book B)
        {
            return $"Authors: {string.Join(", ", B.Authors)}";
        }

        public static string GetPrice(Book B)
        {
            return $"Price: ${B.Price:F2}";
        }
    }

    public delegate string BookDelegate(Book book);

    public class LibraryEngine
    {
        public static void ProcessBooks(List<Book> bList, BookDelegate fPtr)
        {
            Console.WriteLine("\n=== Using User-Defined Delegate ===");
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }

        public static void ProcessBooksWithFunc(List<Book> bList, Func<Book, string> fPtr)
        {
            Console.WriteLine("\n=== Using Built-in Func<Book, string> Delegate ===");
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
                {
                    new Book("978-0-12-345678-9", "Clean Code",
                        new[] { "Robert C. Martin" },
                        new DateTime(2008, 8, 1), 45.99m),

                    new Book("978-0-98-765432-1", "Design Patterns",
                        new[] { "Erich Gamma", "Richard Helm", "Ralph Johnson", "John Vlissides" },
                        new DateTime(1994, 10, 31), 54.95m),

                    new Book("978-1-11-111111-1", "The Pragmatic Programmer",
                        new[] { "Andrew Hunt", "David Thomas" },
                        new DateTime(1999, 10, 20), 42.00m)
                };

            Console.WriteLine("=== All Books (using ToString) ===");
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine("\n--- Testing with GetTitle ---");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetTitle);

            Console.WriteLine("\n--- Testing with GetAuthors ---");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetAuthors);

            Console.WriteLine("\n--- Testing with GetPrice ---");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetPrice);

            Console.WriteLine("\n--- Testing with Func<Book, string> delegate ---");
            LibraryEngine.ProcessBooksWithFunc(books, BookFunctions.GetTitle);

            Console.WriteLine("\n=== Using Anonymous Method (GetISBN) ===");
            BookDelegate getISBNAnonymous = delegate (Book b)
            {
                return $"ISBN: {b.ISBN}";
            };
            LibraryEngine.ProcessBooks(books, getISBNAnonymous);

            LibraryEngine.ProcessBooks(books, delegate (Book b)
            {
                return $"ISBN (Direct Anonymous): {b.ISBN}";
            });

            Console.WriteLine("\n=== Using Lambda Expression (GetPublicationDate) ===");
            BookDelegate getPublicationDateLambda = b => $"Publication Date: {b.PublicationDate.ToShortDateString()}";
            LibraryEngine.ProcessBooks(books, getPublicationDateLambda);

            LibraryEngine.ProcessBooks(books, b => $"Pub Date (Direct Lambda): {b.PublicationDate.ToString("yyyy-MM-dd")}");

            Console.WriteLine("\n=== Additional Examples ===");

            LibraryEngine.ProcessBooksWithFunc(books, b => $"{b.Title} by {string.Join(" & ", b.Authors)} (${b.Price})");

            LibraryEngine.ProcessBooksWithFunc(books, b =>
            {
                var authorCount = b.Authors.Length;
                var authorText = authorCount == 1 ? "author" : "authors";
                return $"{b.Title} has {authorCount} {authorText}";
            });

            Console.WriteLine("\n=== Method Group Conversion ===");
            Func<Book, string> funcDelegate = BookFunctions.GetTitle;
            LibraryEngine.ProcessBooksWithFunc(books, funcDelegate);

            Console.WriteLine("\n=== Custom Methods Examples ===");

            string GetBookAge(Book b)
            {
                var age = DateTime.Now.Year - b.PublicationDate.Year;
                return $"{b.Title} is {age} years old";
            }
            LibraryEngine.ProcessBooksWithFunc(books, GetBookAge);

            LibraryEngine.ProcessBooks(books, b =>
                $"[{b.ISBN}] {b.Title} - Published: {b.PublicationDate.Year}, Price: ${b.Price:F2}");

            decimal priceThreshold = 45.00m;
            Console.WriteLine($"\n=== Books over ${priceThreshold} ===");
            LibraryEngine.ProcessBooksWithFunc(books, b =>
                b.Price > priceThreshold ? $"{b.Title} - ${b.Price:F2} (Above threshold)" : $"{b.Title} - ${b.Price:F2}");

            Console.WriteLine("\n=== Complex Anonymous Method ===");
            BookDelegate complexAnonymous = delegate (Book b)
            {
                string result = $"Book: {b.Title}\n";
                result += $"  - ISBN: {b.ISBN}\n";
                result += $"  - Authors: {string.Join(", ", b.Authors)}\n";
                result += $"  - Published: {b.PublicationDate.ToLongDateString()}\n";
                result += $"  - Price: ${b.Price:F2}";
                return result;
            };

            foreach (var book in books.Take(1))
            {
                Console.WriteLine(complexAnonymous(book));
            }

            Console.WriteLine("\n=== Comparing All Delegate Types ===");
            Book sampleBook = books[0];

            Console.WriteLine("1. Static Method Reference:");
            Console.WriteLine("   " + BookFunctions.GetTitle(sampleBook));

            Console.WriteLine("\n2. User-Defined Delegate:");
            BookDelegate userDelegate = BookFunctions.GetAuthors;
            Console.WriteLine("   " + userDelegate(sampleBook));

            Console.WriteLine("\n3. Built-in Func Delegate:");
            Func<Book, string> funcDel = BookFunctions.GetPrice;
            Console.WriteLine("   " + funcDel(sampleBook));

            Console.WriteLine("\n4. Anonymous Method:");
            BookDelegate anonMethod = delegate (Book b) { return $"ISBN: {b.ISBN}"; };
            Console.WriteLine("   " + anonMethod(sampleBook));

            Console.WriteLine("\n5. Lambda Expression:");
            BookDelegate lambdaExpr = b => $"Published: {b.PublicationDate.ToShortDateString()}";
            Console.WriteLine("   " + lambdaExpr(sampleBook));

            Console.WriteLine("\n=== Practical Example: Filter and Process ===");
            var expensiveBooks = books.Where(b => b.Price > 45.00m).ToList();
            Console.WriteLine("Expensive books (over $45):");
            LibraryEngine.ProcessBooksWithFunc(expensiveBooks, b => $"{b.Title}: ${b.Price:F2}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}