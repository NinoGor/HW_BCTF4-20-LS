namespace Lecture10_HW.Davaleba2
{
    internal abstract class Worker
    {
        private string? _name;
        private string? _surname;
        private string? _position;
        private decimal _salary;

        public string Name
        {
            get { return _name ?? "[Unknown Name]"; }
            private set
            {
                _name = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }
        public string Surname
        {
            get
            {
                return _surname ?? "";
            }
            private set
            {
                _surname = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }

        public string Position
        {
            get
            {
                return _position ?? "Unknown Position";
            }
            private set
            {
                _position = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }

        public decimal Salary
        {
            get
            {
                return _salary;
            }
            private set
            {
                _salary = value < 0 ? -1 : value;
            }
        }

        protected Worker(string name, string surname, string position, decimal salary)
        {
            Name = name;
            Surname = surname;
            Position = position;
            Salary = salary;
        }

        public abstract void Print();

        protected string GetSalaryString()
        {
            return Salary == -1
                ? "Unknown Salary"
                : $"{Salary}";
        }

        public override string ToString()
        {
            return $"{Position} - {Name} {Surname}. Salary: {GetSalaryString()}";
        }
    }
}
