namespace Lecture7_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("EL123", "Laptop", "Budget-friendly, thin, light.", 2500, 25, "Lenovo", "Electronics & Technology", 10);

            // რეიტინგების სიმულაციასავით:
            product1.SubmitRating(5); 
            product1.SubmitRating(4); 
            product1.SubmitRating(1);

            // გავზარდოთ მარაგიც
            product1.IncreaseStock(10);

            // გამოვიყენოთ საბოლოო ფასის მეთოდიც (თუმცა ის "არაცხადად" გამოიყენება ბეჭდვის მეთოდებშიც):
            Console.WriteLine($"Final price of {product1.Name}: {product1.CalculateFinalPrice()}\n");

            // გამოვიყენოთ ძირითადი ინფორმაციის ბეჭდვის მეთოდი (როგორც მაგალითში): 
            product1.PrintMainInfo();

            // ასევე შეგვიძლია სრული ინფორმაციის გამოტანა:
            product1.PrintFullInfo();
        }
    }
}
