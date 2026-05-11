namespace Lecture2_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Davaleba 1
            /* დაწერეთ C# Sharp პროგრამა, რომლითაც მომხმარებელი შეიყვანს ამომრჩევლის ასაკს 
               და პროგრამა განსაზღვრავს, აქვს თუ არა მას არჩევნებზე ხმის მიცემის უფლება. */

            Console.WriteLine("<---- Davaleba 1 ---->");
            Console.WriteLine("Sheiyvanet amomrchevlis asaki:");

            int age = int.Parse(Console.ReadLine());

            bool canVote = age >= 18;

            Console.WriteLine(canVote ?
                "gilocav! xmis micemis ufleba gaqvt." :
                "samwuxarod xmis micemis ufleba jer ar gaqvt.");
            #endregion

            #region Davaleba 2
            /* დაწერეთ C# პროგრამა, რომელიც დაადგენს სამ რიცხვს შორის უდიდესს. */
            Console.WriteLine("\n\n<---- Davaleba 2 ---->");

            Console.WriteLine("sheiyvanet 1-li ricxvi:");
            double num1 = double.Parse(Console.ReadLine());

            Console.WriteLine("sheiyvanet me-2 ricxvi:");
            double num2 = double.Parse(Console.ReadLine());

            Console.WriteLine("sheiyvanet me-3 ricxvi:");
            double num3 = double.Parse(Console.ReadLine());

            // radgan if-else-it jer ar gvimushavia, nested ternary operators gamoviyeneb
            // simartivistvis, tu ramdenimea maqsimaluri, rigit pirvels gamoitans
            string strLargest = (num1 >= num2 && num1 >= num3) ? "1-li" : (num2 >= num3) ? "me-2" : "me-3";

            Console.WriteLine(strLargest + "ricxvi maqsimaluria warmodgenil ricxvebs shoris.");
            #endregion

            #region Davaleba 3
            Console.WriteLine("\n\n<---- Davaleba 3 ---->");
            /* დაწერეთ C# პროგრამა ორი მოცემული მთელი რიცხვის ჯამის გამოსათვლელად. 
               თუ ეს ორი რიცხვი ერთნაირია, მაშინ დააბრუნეთ გასამმაგებული მათი ჯამი. */
            int a, b;

            // 2 qeisi gasatestad
            //a = 1; b = 2;
            //a = 2; b = 2;

            // an user inputit:
            Console.WriteLine("mteli ricxvi N1: ");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("mteli ricxvi N2: ");
            b = int.Parse(Console.ReadLine());

            Console.WriteLine("shedegi: " + (a == b ? 3 * (a + b) : a + b));
            #endregion
        }
    }
}
