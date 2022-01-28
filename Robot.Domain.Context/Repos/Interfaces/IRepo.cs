using System.Collections.Generic;

namespace Robot.DAL.Repos.Interfaces
{
    public interface IRepo<T>
    {
        int Add(T entity);

        T Get(int id);

        int Save(T entity);

        bool Delete(T entity);

        List<T> GetAll();
    }
}
