namespace Lecture8_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"../../../CarsData.txt";
            // შევამოწმოთ, რომ ასეთი ფაილი არსებობს, თუ არ არსებობს, გაგრძელებას აზრი არ აქვს
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Could not find the file at {filePath}");
                return;
            }

            Car[] loadedCars = Car.ReadCarsFromFile(filePath);
            Console.WriteLine($"Loaded {loadedCars.Length} cars:\n");

            Car.PrintAll(loadedCars);

            // დამატებითი მეთოდის გატესტვა (ორიგინალ ფაილში ბრენდები არ მეორდება)
            //string searchBrand = "BMW";
            //Console.WriteLine($"\nCars of brand: '{searchBrand}:\n");
            //Car.PrintCarsByBrand(loadedCars, searchBrand);
        }
    }
}
