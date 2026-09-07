using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UniversityDomain.Interfaces;
using UniversityDomain.Models;

namespace UniversityInfrastucture.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();

            var query = "SELECT * FROM Students";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    students.Add(MapStudent(reader));
                }

            }

            return students;

        }

        public Student? GetById(int id)
        {
            var student = new Student();
            var query = "SELECT * FROM Students where Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapStudent(reader);
            }

            return null;
        }

        public bool Add(Student student)
        {
            var query = @"INSERT INTO Students (FirstName, LastName, Email, Age, GPA, PhoneNumber, IsActive, RegisteredAt, DepartmentId)
                         VALUES (@FirstName, @LastName, @Email, @Age, @GPA, @PhoneNumber, @IsActive, @RegisteredAt, @DepartmentId)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.Text; // default is text, but you can set it explicitly if you want to use a stored procedure instead


            // todo procedure (შესრულებულია ქვემოთ)
            //command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@IsActive", student.IsActive);
            command.Parameters.AddWithValue("@RegisteredAt", student.RegisteredAt);
            command.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);


            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }

        // ალტერნატიული Add პროცედურით
        //public bool Add(Student student)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    using var command = new SqlCommand("dbo.sp_InsertStudent", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.AddWithValue("@FirstName", student.FirstName);
        //    command.Parameters.AddWithValue("@LastName", student.LastName);
        //    command.Parameters.AddWithValue("@Email", student.Email);
        //    command.Parameters.AddWithValue("@Age", student.Age);
        //    command.Parameters.AddWithValue("@GPA", student.GPA);
        //    command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
        //    command.Parameters.AddWithValue("@IsActive", student.IsActive);
        //    command.Parameters.AddWithValue("@RegisteredAt", student.RegisteredAt);
        //    command.Parameters.AddWithValue("@DepartmentID", student.DepartmentId);

        //    connection.Open();

        //    return command.ExecuteNonQuery() > 0;
        //}


        public bool UpdateGpa(int id, decimal gpa)
        {

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_UpdateGpaOnSingleStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NewGpa", gpa);
            command.Parameters.AddWithValue("@StudentId", id);
            connection.Open();

            return command.ExecuteNonQuery() > 0;

        }

        private Student MapStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Age = reader.GetInt32(4),
                GPA = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                PhoneNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                IsActive = reader.IsDBNull(7) ? null : reader.GetBoolean(7),
                RegisteredAt = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
                DepartmentId = reader.IsDBNull(9) ? null : reader.GetInt32(9)
            };
        }

        // დავამატოთ update და delete

        public bool Update(Student student)
        {
           var query = @"UPDATE Students
                          SET FirstName = @FirstName, LastName = @LastName, Email = @Email,
                              Age = @Age, GPA = @GPA, PhoneNumber = @PhoneNumber,
                              IsActive = @IsActive, DepartmentId = @DepartmentId
                          WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@IsActive", student.IsActive);
            command.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
            connection.Open();
            return command.ExecuteNonQuery() > 0;


            // ან პროცედურით
            //using var connection = new SqlConnection(_connectionString);
            //using var command = new SqlCommand("dbo.sp_UpdateStudent", connection);
            //command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.AddWithValue("@Id", student.Id);
            //command.Parameters.AddWithValue("@FirstName", student.FirstName);
            //command.Parameters.AddWithValue("@LastName", student.LastName);
            //command.Parameters.AddWithValue("@Email", student.Email);
            //command.Parameters.AddWithValue("@Age", student.Age);
            //command.Parameters.AddWithValue("@GPA", student.GPA);
            //command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            //command.Parameters.AddWithValue("@IsActive", student.IsActive);
            //command.Parameters.AddWithValue("@DepartmentID", student.DepartmentId);

            //connection.Open();

            //return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            var query = "DELETE FROM Students WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            return command.ExecuteNonQuery() > 0;

            // ან პროცედურით
            //using var connection = new SqlConnection(_connectionString);
            //using var command = new SqlCommand("dbo.sp_DeleteStudent", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@Id", id);
            //connection.Open();

            //return command.ExecuteNonQuery() > 0;
        }

    }
}