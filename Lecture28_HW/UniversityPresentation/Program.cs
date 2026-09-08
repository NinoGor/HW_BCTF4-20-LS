using Microsoft.Extensions.Configuration;
using System.Text;
using UniversityApplication.Services;
using UniversityDomain.Interfaces;
using UniversityDomain.Models;
using UniversityInfrastucture.Repositories;

namespace UniversityPresentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            var _connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.OutputEncoding = Encoding.UTF8;

            #region გაკვეთილზე გაკეთებული
            IStudentRepository studentRepository = new StudentRepository(_connectionString);
            var studentService = new StudentService(studentRepository);

            //Console.WriteLine("All students:");
            //studentService.GetAllStudents();

            //Console.WriteLine("Student by id 1");
            //studentService.GetStudentById(1);

            //Console.WriteLine("Adding a new student:");
            //studentService.AddStudent(new Student { FirstName = "John", LastName = "Doe",  Email = "john.doe@example.com", Age = 20, GPA = 3.5M, PhoneNumber = "123-456-7890", IsActive = true, RegisteredAt = DateTime.Now, DepartmentId = 1 });

            //Console.WriteLine("Updating a student:");
            //studentService.UpdateStudentGpa(2, 3.9M);
            #endregion

            IInstructorRepository instructorRepository = new InstructorRepository(_connectionString);
            var instructorService = new InstructorService(instructorRepository);

            Console.WriteLine("---All instructors");
            instructorService.GetAllInstructors();

            int testId = 1;
            Console.WriteLine($"\n---Instructor by id={testId}");
            instructorService.GetInstructorById(testId);

            //Console.WriteLine("\n---Inserting a new instructor");
            //instructorService.AddInstructor(new Instructor
            //{
            //    FirstName = "ნინო",
            //    LastName = "გორგილაძე",
            //    Email = "ninogor@gmail.com",
            //});
            //Console.WriteLine("\nAll instructors");
            //instructorService.GetAllInstructors();


            //Console.WriteLine("\n---Updating an instructor:");
            //instructorService.UpdateInstructor(new Instructor { InstructorID = 1, FirstName = "ვინმე", LastName = "განსხვავებული", Email = "test@university.edu" });
            //Console.WriteLine("\nAll instructors");
            //instructorService.GetAllInstructors();


            //Console.WriteLine("\n---Deleting instructor:");
            //instructorService.DeleteInstructor(4);
            //Console.WriteLine("\nAll instructors");
            //instructorService.GetAllInstructors();

        }
    }
}