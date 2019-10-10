using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NetCoreApi.Repositoties.Interface
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<int> Add(IDbConnection connection, T data);

        void Delete(int id);

        T GetById(int id);

        void Update(T item);
    }
}