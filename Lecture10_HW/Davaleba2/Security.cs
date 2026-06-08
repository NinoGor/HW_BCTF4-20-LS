namespace Lecture10_HW.Davaleba2
{
    internal class Security : Worker
    {
        public Security(string name, string lastName, decimal salary)
            : base(name, lastName, "Security", salary)
        {}

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("Role: Ensures safety of people and property.");
        }
    }
}
