using System;
using System.Collections.Generic;
using System.Text;
using UniversityDomain.Models;

namespace UniversityDomain.Interfaces
{
    // აქ შეგვიძლია დავამატოთ ინსტრუქტორისთვის სპეციფიკური მეთოდებიც
    // დავალებაში მოთხოვნილი მეთოდები წამოვა IRepository<T>-დან 
    public interface IInstructorRepository : IRepository<Instructor>
    {
    }
}
