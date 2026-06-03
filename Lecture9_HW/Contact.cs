using Lecture9_HW.Enums;

namespace Lecture9_HW
{
    internal class Contact
    {
        private string? _value;

        // პირდაპირ auto-property არაცხადი private field-ით რადგან დამატებით ვალიდაცია აქ არ მაქვს
        // set-ს private-ს გავაკეთებ, მარტო კონსტრუქტორი გამოიყენებს, კონტაქტის ტიპის ცვლილებას არ დავუშვებ
        public Contacts Type { get; private set; }

        public string? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = ValidateValue(value, Type);
            }
        }

        public Contact(Contacts type, string? value)
        {
            Type = type;
            Value = value;
        }

        private static string? ValidateValue(string? value, Contacts type)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            value = value.Trim();
            // ნომრების შემთხვევაში ციფრებს შორის სფეისები რომ პრობლემა არ იყოს, საერთოდ მოვაშოროთ
            // Trim-ს მაინც ვტოვებთ რომ space-ის გარდა სხვა ტიპის whitespace-ებიც დატრიმოს, თუ არის
            value = value.Replace(" ", "");

            switch (type)
            {
                case Contacts.Email:
                    return IsValidEmail(value) ? value : null;

                case Contacts.Phone:
                case Contacts.Fax:
                    return IsValidPhone(value) ? value : null;

                default:
                    return null;
            }
        }

        private static bool IsValidEmail(string value)
        {
            // შეიძლება სრულყოფილი ლოგიკა არც ესაა,
            // მაგრამ შევამოწმებ რომ @ გვაქვს და ის დასაწყისში არ არის
            // რომ . გვაქვს და ის ბოლოში არ არის
            // რომ @-სა და.-ს შორის ერთი სიმბოლო მაინცაა
            // თუ ეს სიმბოლოები არ გვაქვს -1 დაბრუნდება და return false მოხდება
            int atIndex = value.IndexOf('@');
            int dotIndex = value.LastIndexOf('.');

            return atIndex > 0 &&
                   dotIndex > atIndex + 1 &&
                   dotIndex < value.Length - 1;
        }

        private static bool IsValidPhone(string value)
        {
            // კომპლექსურ ლოგიკას არ დავწერ, ვთქვათ, მარტივად, ნომერი უნდა იყოს მინ 5 ციფრი,
            //  შედგებოდეს მხოლოდ ციფრებისგან, ან შესაძლოა პირველი სიმბოლო იყოს პლიუსი
            if (value.Length < 5)
            {
                return false;
            }
            if (!char.IsDigit(value[0]) && value[0] != '+')
            {
                return false;
            }
            for (int i = 1; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Type}: {Value ?? "Invalid"}";
        }
    }
}