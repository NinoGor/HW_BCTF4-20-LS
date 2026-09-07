using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDomain.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[Instructor ID: {InstructorID}] {FirstName} {LastName} - {Email}";
        }
    }
}
