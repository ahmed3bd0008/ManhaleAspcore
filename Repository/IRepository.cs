using System.Collections.Generic;

namespace ManhaleAspNetCore.Repository
{
    public interface IRepository <T>
    {
        T GetById(int id);
        List<T> GetAll();
        T Add(T Item);
        T Update(T item);
        T Delete(int id);
    }
}
