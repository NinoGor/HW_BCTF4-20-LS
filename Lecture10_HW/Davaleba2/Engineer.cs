namespace Lecture10_HW.Davaleba2
{
    internal class Engineer : Worker
    {
        public Engineer(string name, string lastName, decimal salary)
            : base(name, lastName, "Engineer", salary)
        {}

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("Role: Designs and develops technical systems and solutions.");
        }
    }
}
