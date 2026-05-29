namespace Lecture7_HW
{
    // 1. შექმენით Product კლასი
    internal class Product
    {
        // მაგალითად მოცემული 10 მახასიათებელი, გამოვიყენოთ properties
        // რადგან private ველები, full property ან 'field' ქივორდი არ გაგვივლია, ვალიდაციას ცოტათი დავაიგნორებ
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // სიზუსტისთვის გამოვიყენოთ decimal, Price არის "ორიგინალი" ფასი, ფასდაკლების გარეშე
        public decimal Price { get; set; }
        // რაოდენობა ვერ იქნება უარყოფითი, გამოვიყენოთ uint
        public uint Quantity { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        // ვთქვათ რეიტინგი არის read-only property მნიშვნელობით [0, 5] რეინჯში და იცვლება SubmitRating მეთოდით
        public double Rating { get { return (RatingCount > 0) ? (double)RatingSum / RatingCount : 0; } } 
        // IsAvailable-ს არ მივცეთ set; იყოს read-only,
        // ავტომატურად "გამოითვლება" Quantity-ს მიხედვით
        public bool IsAvailable { get { return Quantity > 0; } }
        public decimal DiscountPercent { get; set; }
        // Rating-ისთვის დავამატოთ ორი Property, რომელშიც წესით private set უნდა იყოს, მაგრამ რადგან არ გაგვივლია ასე დავტოვებ
        public uint RatingSum { get; set; }
        public uint RatingCount { get; set; }

        // კონსტრუქტორი
        public Product(string id, string name, string description, decimal price, uint quantity, string brand, string category, decimal discountPercent)
        {
            // რადგან განსხვავებული სახელებია this-ს აღარ გამოვიყენებ
            // რადგან property-ებში ამ ეტაპზე ვალიდაცია არ გვაქვს, აქ მაინც დავწეროთ მინიმალური ლოგიკა
            Id = (id != "" ? id : "Error");
            Name = (name != "" ? name : "Unknown Product");
            Description = description; // შესაძლოა იყოს ცარიელი
            Price = (price >= 0 ? price : 0);
            Quantity = quantity;
            Brand = (brand != "" ? brand : "Unknown Brand"); ;
            Category = (category != "" ? category : "Unknown Category"); 
            DiscountPercent = (discountPercent >= 0 && discountPercent <= 100 ? discountPercent : 0);
            // isAvalable ჭეშმარიტი იქნება წაკითხვისას თუ რაოდენობა 0-ზე მეტია
            RatingSum = 0;
            RatingCount = 0;
            
        }

        // 2. შექმენით მინიმუმ 3 მეთოდი
        // დავაიმპლემენტიროთ მაგალითად მოცემული 3 მეთოდი:

        //•	საბოლოო ფასის გამოთვლა ფასდაკლებით 
        public decimal CalculateFinalPrice()
        {
            decimal discount = Price * (DiscountPercent / 100m);
            return Price - discount;
        }

        //•	პროდუქტის მარაგის გაზრდა 
        public void IncreaseStock(uint amount)
        {
            if (amount > 0)
            {
                Quantity += amount;
                Console.WriteLine($"\nStock of [{Name}] has been increased by {amount} units. New quantity: {Quantity}\n");
            }
            else
            {
                Console.WriteLine("Error: non-positive amount entered. No changes were made.\n");
            }
        }

        //•	პროდუქტის ინფორმაციის დაბეჭდვა
        public void PrintMainInfo()
        {
            // ამ მეთოდით გამოვიტანოთ მხოლოდ მაგალითში მოცემული მნიშვნელობები
            Console.WriteLine("----- Main Product Info -----");
            Console.Write("Product: ");
            Console.WriteLine(Name);
            Console.Write("Price: ");
            Console.WriteLine($"{Price} GEL");
            Console.Write("Discount (%): ");
            Console.WriteLine($"{DiscountPercent}%");
            Console.Write("Final price: ");
            Console.WriteLine($"{CalculateFinalPrice()} GEL");
            Console.Write("Is in stock: ");
            Console.WriteLine(IsAvailable);
            Console.WriteLine();
        }
        public void PrintFullInfo()
        {
            // ამ მეთოდით გამოვიტანოთ სრული ინფორმაცია
            Console.WriteLine("----- Full Product Info -----");
            Console.Write("Product: ");
            Console.WriteLine(Name);
            Console.Write("Brand: ");
            Console.WriteLine(Brand);
            Console.Write("Category: ");
            Console.WriteLine(Category);
            Console.Write("Description:");
            Console.WriteLine(Description);
            Console.Write("Price: ");
            Console.WriteLine($"{Price} GEL");
            Console.Write("Discount (%): ");
            Console.WriteLine($"{DiscountPercent}%");
            Console.Write("Final price: ");
            Console.WriteLine($"{CalculateFinalPrice()} GEL");
            Console.Write("Rating: ");
            // აქ დავსერჩე ფორმატირება რომ არამთელი ნაწილი ორ ციფრზე მეტით არ დაიბეჭდოს
            Console.WriteLine($"{Rating:0.##}");
            Console.Write("Is in stock: ");
            Console.WriteLine(IsAvailable);
            Console.WriteLine();
        }

        // Rating რომ საერთოდ გამოუყენებელი არ დაგვრჩეს დავამატოთ ერთი მეთოდი მისთვისაც
        public void SubmitRating(byte rating)
        {
            // ვთქვათ, მაქს. რეიტინგი 5-ია (უარყოფითობაზე არ ვამოწმებ რადგან ბაიტია)
            if (rating > 5)
            {
                Console.WriteLine($"Error: invalid rating! Please enter an integer rating from 0 to 5.");
                return;
            }

            RatingSum += rating;
            RatingCount++;          

            Console.WriteLine($"New {rating}-star rating added! Current avg. rating of [{Name}, ID: {Id}]: {Rating:0.##}/5");
        }
    }

}
