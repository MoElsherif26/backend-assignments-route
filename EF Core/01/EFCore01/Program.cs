using EFCore01.Data;

namespace EFCore01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            Console.WriteLine("Database created successfully!");
        }
    }
}
