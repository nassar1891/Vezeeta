using System.Collections.Generic;

namespace VezeetaAPI.Interfaces
{
    public interface ICrudRepository<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
