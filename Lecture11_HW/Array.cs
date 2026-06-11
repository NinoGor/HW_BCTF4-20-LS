namespace Lecture11_HW
{
    // ამ კლასს გამოვიყენებ ორივე დავალებისთვის
    internal class Array : IOutput2, ICalc2
    {
        private int[] _nums;

        public Array(int[] nums)
        {
            _nums = nums;
        }

        // ასევე დავუწეროთ Length property
        public int Length
        {
            get
            {
                if (_nums == null)
                    return -1;
                return _nums.Length;
            }
        }

        // დამხმარე მეთოდი ვალიდაციისთვის
        private bool IsValid()
        {
            if (_nums == null)
            {
                Console.WriteLine("Error: array is null.");
                return false;
            }
            else if (_nums.Length == 0)
            {
                Console.WriteLine("Array is empty.");
                return false;
            }
            return true;
        }

        public void ShowEven()
        {
            if (!IsValid())
            {
                return;
            }

            Console.WriteLine("Even numbers:");
            // თუ ლუწი რიცხვები არ იქნება, სჯობს შესაბამისი მესიჯი გამოვიტანოთ
            bool found = false;

            foreach (int num in _nums)
            {
                if (num % 2 == 0)
                {
                    Console.Write(num + " ");
                    found = true;
                }
            }

            if (!found)
            {
                Console.Write("No even numbers in Array object.");
            }
            Console.WriteLine();
        }

        // ანალოგიურად
        public void ShowOdd()
        {
            if (!IsValid())
            {
                return;
            }

            Console.WriteLine("Odd numbers:");
            bool found = false;

            foreach (int num in _nums)
            {
                if (num % 2 != 0)
                {
                    Console.Write(num + " ");
                    found = true;
                }
            }

            if (!found)
            {
                Console.Write("No odd numbers in Array object.");
            }
            Console.WriteLine();
        }

        // დავალება 2-ის მეთოდები

        // Distinct რაოდენობა ნიშნავს რა რაოდენობა დაგვრჩებოდა დუბლიკატებს თუ მოვაშორებდით
        // რამდენი უნიკალური რიცხვი შეგვიძლია ამოვიღოთ, მაგ.: [-1,2,2,5] -> 3 (-1, 2 და  5)

        // თუ არასწორად გავიგე და გვინდა დავითვალოთ რიცხვები, რომლებიც მხოლოდ ერთხელ გვხვდება
        // მასივში, მაშინ დაგვჭირდება ქვემოთ დაკომენტარებული ვერსია
        public int CountDistinct()
        {
            if (!IsValid())
            {
                return 0;
            }

            int count = 1;

            // შეგვიძლია თითოეული რიცხვისთვის შევამოწმოთ ის თუ მეორდება მის მარცხნივ
            // ასე ყველა "ახალი უნიკალური" რიცხვი დაგვემატება რაოდენობაში, მაგრამ მეტი არა

            for (int i = 1; i < _nums.Length; i++)
            {
                bool isDuplicate = false;

                for (int j = 0; j < i; j++)
                {
                    if (_nums[i] == _nums[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate) count++;
            }

            return count;
        }

        // ითვლის რამდენი რიცხვია, რომელსაც დუპლიკატი არ აქვს
        //public int CountDistinct()
        //{
        //    if (!IsValid())
        //    {
        //        return 0;
        //    }

        //    int count = 0;

        //    for (int i = 0; i < _nums.Length; i++)
        //    {
        //        bool isDuplicate = false;

        //        for (int j = 0; j < _nums.Length; j++)
        //        {
        //            if (i == j) continue;

        //            if (_nums[i] == _nums[j])
        //            {
        //                isDuplicate = true;
        //                break;
        //            }
        //        }

        //        if (!isDuplicate) count++;
        //    }

        //    return count;
        //}

        public int EqualToValue(int valueToCompare)
        {
            if (!IsValid())
            {
                return 0;
            }

            int count = 0;
            foreach (int num in _nums)
            {
                if (num == valueToCompare)
                {
                    count++;
                }
            }
            return count;
        }


        //ბარემ ToString-ის override-იც გვქონდეს
        public override string ToString()
        {
            if (_nums == null) return "Array is null!";
            if (_nums.Length == 0) return "[Empty]";
            return "[" + string.Join(", ", _nums) + "]";
        }
    }
}
