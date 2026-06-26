using Lecture15_HW.Models;

namespace Lecture15_HW.Services
{
    internal class GradeManagerService
    {
        // თავად სერვისი არ ფლობს მონაცემებს
        private readonly GradeBook _gradeBook;

        public GradeManagerService(GradeBook gradeBook)
        {
            _gradeBook = gradeBook ?? throw new ArgumentNullException(nameof(gradeBook), "GradeBook state cannot be null.");
        }

        // InputValidator-ის მეშვეობით იუზერის მხრიდან გარანტირებულად ვალიდური ტიპის მონაცემებს ავიღებ
        // თუმცა, ეს მეთოდები რომ დამოუკიდებელი იყოს მაინც დავუმატებ check-ებს და გავისვრი exception-ებს
        public void AddStudent(string name, int grade)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Student name cannot be null or empty.", nameof(name));
            }

            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
            }

            // რადგან დავალებაში dictionary-ს გასაღებებად სახელები გვაქვს, რაც უნიკალური იდენტიფიკატორი არაა,
            // იძულებული ვარ ჩავთვალო, რომ რამდენიმე სტუდენტი ვერ გვეყოლება ერთი სახელით.

            if (_gradeBook.StudentGrades.ContainsKey(name))
            {
                Console.WriteLine($"Error: Student '{name}' already exists. Use update instead.");
                return;
            }

            _gradeBook.StudentNames.Add(name);
            _gradeBook.StudentGrades.Add(name, grade);
            Console.WriteLine($"Student '{name}' has been added.");
        }

        public void SearchStudent(string name)
        {
            if (_gradeBook.StudentGrades.ContainsKey(name))
            {
                Console.WriteLine($"Student: {name}, Grade: {_gradeBook.StudentGrades[name]}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        public void UpdateGrade(string name, int newGrade)
        {
            if (newGrade < 0 || newGrade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(newGrade), "Grade must be between 0 and 100.");
            }

            if (_gradeBook.StudentGrades.ContainsKey(name))
            {
                _gradeBook.StudentGrades[name] = newGrade;
                Console.WriteLine($"Updated grade of '{name}' to: {newGrade}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        public void PrintAllStudents()
        {
            if (_gradeBook.StudentNames.Count == 0)
            {
                Console.WriteLine("The student list is empty.");
                return;
            }
            // რადგანაც მოთხოვნილი იყო ლისტის მიხედვით დაბეჭდვა...
            Console.WriteLine("--- Students:");
            foreach (var name in _gradeBook.StudentNames)
            {
                // მაინც შევამოწმოთ რომ ნიშანი აქვს ამ სტუდენტს
                if (_gradeBook.StudentGrades.ContainsKey(name))
                {
                    Console.WriteLine($"{name} | Grade: {_gradeBook.StudentGrades[name]}");
                }
            }
        }
    }
}
