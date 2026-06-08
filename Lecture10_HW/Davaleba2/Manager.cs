namespace Lecture10_HW.Davaleba2
{
    internal class Manager : Worker
    {
        public Manager(string name, string lastName, decimal salary)
            : base(name, lastName, "Manager", salary)
        {}

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("Role: Oversees daily operations and team performance.");
        }
    }
}
