using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UniversityDomain.Interfaces;
using UniversityDomain.Models;

namespace UniversityInfrastucture.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly string _connectionString;

        public InstructorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Instructor> GetAll()
        {
            var instructors = new List<Instructor>();

            var query = "SELECT InstructorID, FirstName, LastName, Email FROM INSTRUCTORS";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    instructors.Add(MapInstructor(reader));
                }
            }

            return instructors;
        }

        public Instructor? GetById(int id)
        {
            var query = "SELECT InstructorID, FirstName, LastName, Email FROM INSTRUCTORS WHERE InstructorID = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapInstructor(reader);
            }

            return null;
        }

        public bool Add(Instructor instructor)
        {
            var query = @"INSERT INTO INSTRUCTORS (FirstName, LastName, Email)
                          VALUES (@FirstName, @LastName, @Email)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;


            command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
            command.Parameters.AddWithValue("@LastName", instructor.LastName);
            command.Parameters.AddWithValue("@Email", instructor.Email);

            connection.Open();

            return command.ExecuteNonQuery() > 0;

            // ან პროცედურით
            //using var connection = new SqlConnection(_connectionString);
            //using var command = new SqlCommand("dbo.sp_InsertInstructor", connection);
            //command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
            //command.Parameters.AddWithValue("@LastName", instructor.LastName);
            //command.Parameters.AddWithValue("@Email", instructor.Email);

            //connection.Open();

            //return command.ExecuteNonQuery() > 0;
        }

        public bool Update(Instructor instructor)
        {
            var query = @"UPDATE INSTRUCTORS
                          SET FirstName = @FirstName,
                              LastName = @LastName,
                              Email = @Email
                          WHERE InstructorID = @InstructorID";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
            command.Parameters.AddWithValue("@LastName", instructor.LastName);
            command.Parameters.AddWithValue("@Email", instructor.Email);
            command.Parameters.AddWithValue("@InstructorID", instructor.InstructorID);

            connection.Open();

            return command.ExecuteNonQuery() > 0;

            //ან პროცედურით
            //using var connection = new SqlConnection(_connectionString);
            //using var command = new SqlCommand("dbo.sp_UpdateInstructor", connection);
            //command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.AddWithValue("@InstructorID", instructor.InstructorID);
            //command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
            //command.Parameters.AddWithValue("@LastName", instructor.LastName);
            //command.Parameters.AddWithValue("@Email", instructor.Email);

            //connection.Open();

            //return command.ExecuteNonQuery() > 0;
        }

        // თუ ინსტრუქტორი "მითითებულია" რომელიმე კურსზე, ასეთი ინსტრუქტორის წაშლის მცდელობას არ დავუშვებ
        // ამისთვის მაქვს მეთოდი HasAssignedCourses რომელშიც ExecuteScalar არის გამოყენებული
        public bool Delete(int id)
        {
            if (HasAssignedCourses(id))
            {
                return false;
            }

            var query = "DELETE FROM INSTRUCTORS WHERE InstructorID = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Id", id);

            // ან სანაცვლოდ პროცედურით:
            //using var connection = new SqlConnection(_connectionString);
            //using var command = new SqlCommand("dbo.sp_DeleteInstructor", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@InstructorID", id);

            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }

        private bool HasAssignedCourses(int instructorId)
        {
            var query = "SELECT COUNT(1) FROM COURSES WHERE InstructorID = @InstructorId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InstructorId", instructorId);

            connection.Open();

            var count = (int)command.ExecuteScalar();
            return count > 0;
        }

        private Instructor MapInstructor(SqlDataReader reader)
        {
            return new Instructor
            {
                InstructorID = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3)
            };
        }
    }
}