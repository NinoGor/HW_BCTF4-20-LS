using UniversityDomain.Models;

namespace UniversityDomain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        bool UpdateGpa(int id, decimal gpa);
    }

}
