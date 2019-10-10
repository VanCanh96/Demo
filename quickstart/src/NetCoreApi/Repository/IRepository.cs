using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Repository
{
    public interface IRepository<T>
        where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<int> CreateAsync(IDbConnection connection, T data);
    }
}
