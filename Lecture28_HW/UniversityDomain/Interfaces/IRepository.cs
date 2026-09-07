namespace UniversityDomain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);

    }
}
