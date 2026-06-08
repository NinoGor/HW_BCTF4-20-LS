namespace Lecture10_HW.Davaleba2
{
    internal class President : Worker
    {
        public President(string name, string lastName, decimal salary)
            : base(name, lastName, "President", salary)
        {}

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("Role: Leads the organization and makes strategic decisions.");
        }
    }
}
