using Lecture29_HW.Data;

namespace Lecture29_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new MovieDbContext();
            context.Database.EnsureCreated();
            Console.WriteLine("The DB has been created or already exists.");
        }
    }
}
