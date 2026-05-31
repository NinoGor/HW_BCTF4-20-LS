namespace Lecture8_HW
{
    internal class Car
    {
        /* რადგან nullable types ჯერ არ ვიცით,
           მივანიჭებ ამ default მნიშვნელობებს და თუ არავალიდური მნიშვნელობის მინიჭების მცდელობა იქნება,
           ასეთ მნიშვნელობას არ მივანიჭებ და default მნიშვნელობებით შემეძლება იდენტიფიცირება და, სადაც საჭირო
           იქნება, გაფილტვრა არავალიდური მნიშვნელობების მქონე ობიექტების. */
        private string _brand = "Unknown";
        private string _model = "Unknown";
        private ushort _year = 0;
        private decimal _price = -1m;
        private string _color = "Unknown";

        public string Brand
        {
            get { return _brand; }
            set
            {
                // ==null და =="" შემოწმება არ გვეყოფა, "    " რომ გავტრიმოთ არასწორი იქნება.
                // ამიტომ გამოვიყენებ სტრინგის მეთოდს
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    Console.WriteLine($"Invalid brand! Brand is set to {_brand}");
                }
                else
                {
                    _brand = value.Trim();
                }
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine($"Invalid model! Model is set to {_model}");
                }
                else
                {
                    _model = value.Trim();
                }
            }
        }

        public ushort Year
        {
            get { return _year; }
            set
            {
                // თურმე ყველაზე ძველი მანქანა 1886 წლის არის
                // DateTime-ს გამოვიყენებ რომ მომავლის მანქანები არ მივიღოთ
                if (value >= 1886 && value <= DateTime.Now.Year)
                {
                    _year = value;
                }
                else
                {
                    Console.WriteLine($"Invalid year! Year is set to {_year}.");
                }
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0) // ალტერნატიულად, 0 ფასის მანქანებიც შეგვიძლია არავალიდურად ჩავთვალოთ
                {
                    _price = value;
                }
                else
                {
                    Console.WriteLine($"Invalid price! Price is set to {_price}");
                }
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine($"Invalid color! Color is set to {_color}.");
                }
                else
                {
                    _color = value.Trim();
                }
            }
        }

        public Car() { }
        public Car(string brand, string model, ushort year, decimal price, string color)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
        }

        // ჯერ დავწერ არასტატიკურ მეთოდებს
        public void PrintInfo()
        {
            string priceStr = HasValidPrice() ? Price.ToString() : "Unknown";
            string yearStr = HasValidYear() ? Year.ToString() : "Unknown";
            Console.WriteLine($"Car: {Brand} {Model}," +
                $"\nYear: {yearStr},\nPrice: {priceStr}$,\nColor: {Color}.");
        }

        public bool HasValidPrice()
        {
            return _price != -1;
        }

        public bool HasValidYear()
        {
            return _year != 0;
        }
        
        // ახლა სტატიკური მეთოდები
        // გვქონდეს TryParse-ის მსგავსი მეთოდი, რომელიც სტრინგიდან ეცდება მანქანის "გაპარსვას".
        public static bool TryParse(string line, char delimiter, out Car parsedCar)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // null-ს ამ ეტაპზე მოვერიდები და default მნიშვნელობებით გავაკეთებ ობიექტს
                parsedCar = new Car();
                return false;
            }

            string[] data = line.Split(delimiter);

            if (data.Length < 5)
            {
                parsedCar = new Car();
                return false;
            }

            if (!ushort.TryParse(data[2], out ushort year)) { year = 0; }
            if (!decimal.TryParse(data[3], out decimal price)) { price = -1; }

            parsedCar = new Car(data[0], data[1], year, price, data[4]);
            return true;
        }

        // სტატიკური მეთოდი ფაილიდან მანქანების წასაკითხად
        public static Car[] ReadCarsFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Error: The file '{fileName}' not found.\nReturning empty array...");
                return [];
            }
            string[] lines = File.ReadAllLines(fileName);
            //გამოვყოთ მანქანების მასივი
            Car[] cars = new Car[lines.Length];
            /* შესაძლოა ზოგიერთი ხაზი არავალიდური იყოს, ამ შემთხვევაში
               მანქანას არ დავამატებ სანამ ვალიდურ ხაზს არ მივიღებ.
               თუ ასე მოხდა მასივი მთლიანად არ შეივსება, ამიტომ Array.resize-ს
               გამოვიყენებ, რომელიც უკვე განვიხილეთ ერთხელ.
             */
            int validCount = 0;

            // line-ების მოდიფიკაციას არ ვაკეთებთ, არც მათი ინდექსი გვჭირდება
            // ამიტომ foreach-ით შეგვიძლია შევავსოთ მანქანების მასივი
            foreach (string line in lines)
            {
                if (TryParse(line, ',', out Car temp))
                {
                    cars[validCount] = temp;
                    validCount++;
                }
            }
            if (cars.Length != validCount)
                Array.Resize(ref cars, validCount);
            
            return cars;
        }

        // მანქანების მასივიდან ყველა მანქანის დაბეჭდვა
        public static void PrintAll(Car[] cars)
        {
            if (cars == null || cars.Length == 0)
            {
                Console.WriteLine("No cars available to display.");
                return;
            }
            int num = 1;
            foreach (Car car in cars)
            {
                Console.Write($"#{num} ");
                car.PrintInfo();
                Console.WriteLine();
                num++;
            }
        }

        // მაგალითად, შეგვიძლია დამატებით ასეთი მეთოდიც დავწეროთ, რომელიც კონკრეტული ბრენდის მანქანებს დაბეჭდავს
        public static void PrintCarsByBrand(Car[] cars, string brand)
        {
            if (cars == null || cars.Length == 0)
            {
                Console.WriteLine("No cars available to display.");
                return;
            }
            int num = 1;
            foreach (Car car in cars)
            {
                // ასოების რეგისტრებმა რომ არ შეგვიშალოს ხელი ToLower-ს გამოვიყენებ
                if (car.Brand.ToLower() == brand.ToLower())
                {
                    Console.Write($"#{num} ");
                    car.PrintInfo();
                    Console.WriteLine();
                    num++;
                }
            }
        }
    }
}
