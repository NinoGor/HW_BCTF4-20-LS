using UniversityDomain.Interfaces;
using UniversityDomain.Models;

namespace UniversityApplication.Services
{
    public class InstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public void GetAllInstructors()
        {
            var instructors = _instructorRepository.GetAll();

            foreach (var instructor in instructors)
            {
                Console.WriteLine(instructor);
            }
        }

        public void GetInstructorById(int id)
        {
            var instructor = _instructorRepository.GetById(id);

            if (instructor != null)
            {
                Console.WriteLine(instructor);
            }
            else
            {
                Console.WriteLine("Instructor not found.");
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            var success = _instructorRepository.Add(instructor);
            if (success)
            {
                Console.WriteLine("Instructor added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add instructor.");
            }
        }

        public void UpdateInstructor(Instructor instructor)
        {
            var success = _instructorRepository.Update(instructor);
            if (success)
            {
                Console.WriteLine("Instructor updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update instructor.");
            }
        }

        public void DeleteInstructor(int id)
        {
            var success = _instructorRepository.Delete(id);
            if (success)
            {
                Console.WriteLine("Instructor deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete instructor.");
            }
        }
    }
}
