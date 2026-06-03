using Lecture9_HW.Enums;
// 4.შექმენით Employee კლასი რომელსაც ექნება შექმნილი enum-ების property-ები
// და პლიუს თავისი ფროფერთები: name, surname, dateofbirth (datetime)
namespace Lecture9_HW
{
    internal class Employee
    {
        // მინ. რა ასაკის ადამიანი შეიძლება ჩაითვალოს ვალიდურ თანამშრომლად
        private const int MinEmployeeAge = 18;

        private string? _name;
        private string? _surname;
        private DateTime? _dateOfBirth;

        // არავალიდური მნიშვნელობის შემთხვევაში null-ს შევინახავ,
        // რომ პროგრამის სხვა ნაწილებში ასეთი მნიშვნელობები მარტივად "გაიფილტროს"
        public string? Name
        {
            get { return _name; }
            set
            {
                _name = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            }
        }
        public string? Surname
        {
            get { return _surname; }
            set
            {
                _surname = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (!value.HasValue)
                {
                    _dateOfBirth = null;
                    return;
                }
                // არ ვიღებთ მომავლის თარიღს
                if (value > DateTime.Today)
                {
                    _dateOfBirth = null;
                    return;
                }
                // არ ვიღებთ ისეთ დაბადების თარიღს რომლითაც თანაშრომელი დასაშვებზე მცირე ასაკის გამოდის
                if (CalculateAge(value.Value) < MinEmployeeAge)
                {
                    _dateOfBirth = null;
                    return;
                }

                _dateOfBirth = value;
            }
        }

        // ენამ ტიპების შემთხვევაში ცალკე რაიმე ვალიდაციას არ დავწერ, ამიტომ auto-property-ებს გამოვიყენებ,
        // ამ ფროფერთების უკან ისედაც იქნება private field-ი, თუმცა არაცხადად
        public Country Country { get; set; }
        public Gender Gender { get; set; }

        // აქ ოდნავ პირობას ავცდები, კონტაქტების ენამს ვიყენებ კლას Contact-ში, სადაც კონტაქტის ენამ ტიპიც 
        // მაქვს და მნიშვნელობაც, ხოლო თანამშრომელს ექნება კონტაქტების მასივი, რადგან შეიძლება რამდენიმე
        // საკონტაქტო ჰქონდეს, თუნდაც ერთი ტიპის. მაგ. ორი ნომერი და კიდევ მეილიც.
        public Contact[] Contacts { get; set; }


        // 5.Employ კლასს ჩაუმატეთ პარამეტრიანი კონსტრუქტორი რომელიც ყველა ფროფერთის შეავსებს.
        public Employee(
            string? name,
            string? surname,
            DateTime? dateOfBirth,
            Country country,
            Gender gender,
            Contact[] contacts)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Country = country;
            Gender = gender;
            Contacts = contacts;
        }


        // 6.Employ კლასს ჩაუმატეთ მეთოდი რომელიც გამოთვლის და დაგვიბრუნებს ასაკს.

        // დამხმარე სტატიკური მეთოდი, რომელსაც ვალიდაციისთვისაც გამოვიყენებ და მე-6 ნაბიჯისთვისაც
        private static int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;

            // საათები უგულებელვყოთ .Date-ით
            if (birthDate.Date.AddYears(age) > DateTime.Today.Date)
            {
                age--;
            }

            return age;
        }
        // ეს "ობიექტზე მიბმული" ფუნქცია კი დაგვიბრუნებს კონკრეტული თანამშრომლის ასაკს
        public int? GetAge()
        {
            // თუ თარიღი null-ია (არასწორია), ასაკიც null-ი დავაბრუნოთ
            if (!DateOfBirth.HasValue)
            {
                return null;
            }
            return CalculateAge(DateOfBirth.Value);
        }

        public override string ToString()
        {
            string ageText = GetAge()?.ToString() ?? "Invalid";
            string dobText = DateOfBirth?.ToString("yyyy-MM-dd") ?? "Invalid";

            string contactsText = "";

            if (Contacts == null || Contacts.Length == 0)
            {
                contactsText = "No contacts";
            }
            else
            {
                for (int i = 0; i < Contacts.Length; i++)
                {
                    contactsText += Contacts[i].ToString();

                    if (i < Contacts.Length - 1)
                    {
                        contactsText += ", ";
                    }
                }
            }

            return $"Name: {Name ?? "Invalid"}\nSurname: {Surname ?? "Invalid"}\n" +
                   $"DateOfBirth: {dobText}\nAge: {ageText}\nCountry: {Country}\nGender: {Gender}\n" +
                   $"Contacts: {contactsText}";
        }
    }
}
