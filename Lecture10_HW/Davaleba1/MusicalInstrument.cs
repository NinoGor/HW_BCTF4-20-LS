namespace Lecture10_HW.Davaleba1
{
    /*ეს base კლასი უნდა იყოს აბსტრაქტული, რადგან ზოგადად მუსიკალური ინსტრუმენტის 
      ობიექტის არსებობა ყველანაირად არალოგიკურია. ობიექტები უნდა გვქონდეს კონკრეტული ინსტრუმენტების. */
    internal abstract class MusicalInstrument
    {
        /* ვიფიქრე  ხმებზე რა მომეფიქრებინა განსხვავებული  და გადავწყვიტე, რომ თითოეულ
         კლასში მექნება შესაბამის ინსტრუმენტთან ასოცირებული ცნობილი ნაწარმოებები და "ხმის გამოცემისას"
         რენდომად ვითომ გაისმება რომელიმე მათგანის ხმა.*/
        private static readonly Random Random = new();

        private string? _name;
        private string? _description;
        private string? _history;

        // დავწერ private სეთერებს,
        // გარედან ამ მნიშვნელობების მოდიფიკაციის ნებას არ დავრთავ
        public string Name
        {
            get { return _name ?? "Unknown Instrument"; }
            private set
            {
                _name = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }
        public string Description
        {
            get { return _description ?? "No description provided."; }
            private set
            {
                _description = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }
        public string HistoryInfo
        {
            get { return _history ?? "No history info provided."; }
            private set
            {
                _history = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }

        // დავალების პირობაში მოჭრილი იყო ტექსტი, თუ სახელწოდების განსაზღვრის გარდა რა უნდა მოხდეს კონსტრუქტორში
        // აღწერასა და ისტორიასაც განვსაზღვრავ
        protected MusicalInstrument(
            string name,
            string description,
            string history)
        {
            Name = name;
            Description = description;
            HistoryInfo = history;
        }

        protected string GetRandomPiece()
        {
            string[] pieces = GetFamousPieces();

            // თუ ცარიელი მასივია უცნობი ნაწარმოები დავაბრუნოთ
            return pieces.Length > 0
                ? pieces[Random.Next(pieces.Length)]
                : "Unknown Piece";
        }

        // ამ აბსტრაქტულ მეთოდს კი child კლასებში დავაიმპლემენტირებ
        protected abstract string[] GetFamousPieces();

        // ხმის მეთოდიც აბსტრაქტულია, არ გვაქვს ზოგადი ხმის ლოგიკა
        public abstract void Sound();

        // დანარჩენი მეთოდები კი ვირტუალურია და თუ სადმე საჭირო იქნება override მოხდება
        public virtual void Show() // პირობის მიხედვით უნდა აჩვენოს სახელწოდება
        {
            Console.WriteLine($"Name: {Name}");
        }

        public virtual void Desc()
        {
            Console.WriteLine($"Description: {Description}");
        }

        public virtual void History()
        {
            Console.WriteLine($"History: {HistoryInfo}");
        }

        public override string ToString()
        {
            return $"Instrument: {Name}\nDescription: {Description}\nHistory: {HistoryInfo}";
        }
    }
}
