using System.Text.RegularExpressions;

namespace Lecture13_HW
{
    internal class Person
    {
        private string _name = "";
        private string _lastName = "";
        private int _age;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentException("Name must not be empty!");
                }
                string trimmed = value.Trim();
                // რადგან LINQ ჯერ არ ვიცით, რეგექსს გამოვიყენებ
                if (!Regex.IsMatch(trimmed, @"^[a-zA-Z]+$"))
                {
                    throw new ArgumentException("Name must contain letters only!");
                }
                _name = trimmed;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name must not be empty!");
                }
                string trimmed = value.Trim();
                // რადგან LINQ ჯერ არ ვიცით, რეგექსს გამოვიყენებ
                if (!Regex.IsMatch(trimmed, @"^[a-zA-Z]+$"))
                {
                    throw new ArgumentException("Last name must contain letters only!");
                }
                _lastName = trimmed;
            }
        }

        // ვირტუალური, რადგან შვილ კლასს შესაძლოა უფრო კონკრეტული შეზღუდვა ჰქონდეს
        public virtual int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if(value < 0 || value > 120) 
                {
                    throw new ArgumentOutOfRangeException("Age must be in range [0-120]!");
                }
                _age = value;
            } 
        }

        protected Person() { }
        protected Person(string name, string lastName, int age)
        {
            Name = name;
            LastName = lastName;
            Age = age;
        }

    }
}
