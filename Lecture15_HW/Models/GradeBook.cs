/* ეს კლასი შევქმენი SRP-ის დასაცავად, რომ სერვის კლასს დამატებით
   მონაცემთა შენახვის პასუხისმგებლობაც არ ჰქონოდა. */
namespace Lecture15_HW.Models
{
    internal class GradeBook
    {
        public List<string> StudentNames { get; set; } = new List<string>();
        public Dictionary<string, int> StudentGrades { get; set; } = new Dictionary<string, int>();
    }
}
