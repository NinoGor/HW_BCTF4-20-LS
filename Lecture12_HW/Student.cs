using System;
using System.Text.RegularExpressions;

namespace Lecture12
{
    internal class Student : IComparable<Student>
    {
        // დავამატოთ მინიმალური ვალიდაცია
        private string _firstName = "Unknown";
        private string _lastName = "Unknown";
        private int _age = -1;
        private string _email = "Unknown";
        private string _phone = "Unknown";
        private int _point = -1;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("First name cannot be empty.");
                    _firstName = "Unknown";
                    return;
                }
                _firstName = value.Trim();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Last name cannot be empty.");
                    _lastName = "Unknown";
                    return;
                }
                _lastName = value.Trim();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 120)
                {
                    Console.WriteLine("Age must be between 0 and 120.");
                    _age = -1;
                    return;
                }
                _age = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@") || !value.Contains("."))
                {
                    Console.WriteLine("Invalid email format.");
                    _email = "Unknown";
                    return;
                }
                _email = value.Trim();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                string cleaned = (value ?? "").Replace(" ", "");
                if (!Regex.IsMatch(cleaned, @"^\+?[1-9]\d{1,14}$"))
                {
                    Console.WriteLine("Invalid phone number.");
                    _phone = "Unknown";
                    return;
                }
                _phone = cleaned;
            }
        }

        public int Point
        {
            get => _point;
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("Points must be between 0 and 100.");
                    _point = -1;
                    return;
                }
                _point = value;
            }
        }

        public override string ToString()
        {
            string strAge = Age != -1 ? Age.ToString() : "Unknown";
            string strPoint = Point != -1 ? Point.ToString() : "Unknown";
            return $"{FirstName} {LastName} | Age: {strAge} | Email: {Email} | Phone: {Phone} | Point: {strPoint}";
        }


        // ოპერატორების გადატვირთვა
        // ========================


        // ქულებისთვის (თუ ტოლია ასაკით)
        public static bool operator >(Student left, Student right)
        {
            if (left == null) return false;
            return left.CompareTo(right) > 0;
        }

        // ასაკისთვის
        public static bool operator <(Student left, Student right)
        {
            if (left == null || right == null) return false;
            return left.Age < right.Age;
        }
        public static double operator +(double currentSum, Student student)
        {
            if (student != null && student.Point >= 0)
            {
                return currentSum + student.Point;
            }
            return currentSum;
        }

        // 1. ვიპოვოთ ისეთი სტუდენტი რომელსაც აქვს ყველაზე დაბალი ქულა
        // გამოვიყენებთ > ოპერატორს (< ასაკისთვის გადავტვირთე)
        public static Student? FindLowestGrade(Student[] students)
        {
            if (students == null || students.Length == 0) return null;

            Student lowest = students[0];

            foreach (Student student in students)
            {
                if (student != null && student.Point != -1 && lowest > student)
                    lowest = student;
            }

            return lowest;
        }

        // 2. ვიპოვოთ ისეთი სტუდენტი რომელიც ყველაზე დიდია ასაკით
        // გამოვიყენებთ < ოპერატორს
        public static Student? FindOldest(Student[] students)
        {
            if (students == null || students.Length == 0) return null;

            Student oldest = students[0];
            foreach (Student student in students)
            {
                if (student != null && oldest < student)
                {
                    oldest = student;
                }
            }
            if (oldest.Age == -1) return null;
            return oldest;
        }

        // 3. ვიპოვოთ ყველა სტუდენტის საშუალო ქულა
        // გამოვიყენებთ + ოპერატორს
        public static double GetAverageGrade(Student[] students)
        {
            if (students == null || students.Length == 0) return 0;

            double sum = 0;
            int count = 0;

            foreach (var student in students)
            {
                if (student != null && student.Point >= 0)
                {
                    sum += student;
                    count++;
                }
            }
            return count > 0 ? sum / count : 0;
        }

        // სორტირებისთვის შევადარებთ ქულებს, თუ ერთნაირია კი სახელებს
        public int CompareTo(Student? other)
        {
            if (other == null) return 1;

            int pointComparison = this.Point.CompareTo(other.Point);
            if (pointComparison != 0) return pointComparison;

            return string.Compare(this.FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase);
        }

        // 4. დაასორტირეთ სტუდენტების მასივი
        // გამოვიყენებთ > ოპერატორს
        public static void SortStudentsByPoints(Student[] students)
        {
            if (students == null || students.Length <= 1) return;

            int n = students.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (students[j] > students[j + 1])
                    {
                        Student temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                        swapped = true;
                    }
                }
                // ეს აჩქარებს "კლასიკურ" bubble sort-ს
                if (!swapped) break;
            }
        }
    }
}