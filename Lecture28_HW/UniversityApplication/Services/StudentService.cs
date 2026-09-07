using System;
using System.Collections.Generic;
using System.Text;
using UniversityDomain.Interfaces;
using UniversityDomain.Models;

namespace UniversityApplication.Services
{
    public class StudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void GetAllStudents()
        {
            var students = _studentRepository.GetAll();

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

        }

        public void GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id);

            if (student != null)
            {
                Console.WriteLine(student);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        public void AddStudent(Student student)
        {
            var success = _studentRepository.Add(student);
            if (success)
            {
                Console.WriteLine("Student added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add student.");
            }
        }

        public void UpdateStudentGpa(int id, decimal gpa)
        {
            var success = _studentRepository.UpdateGpa(id, gpa);
            if (success)
            {
                Console.WriteLine("Student GPA updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update student GPA.");
            }
        }

        //დამატებით
        public void DeleteStudent(int id)
        {
            var success = _studentRepository.Delete(id);
            if (success)
            {
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete student.");
            }
        }

    }


}