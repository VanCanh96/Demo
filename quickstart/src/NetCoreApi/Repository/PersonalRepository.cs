using Dapper;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Repository
{
    public class PersonalRepository : IRepository<Personal>
    {
        private readonly string _connectionString;

        public PersonalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connectionString);
            }
        }
        public async Task<int> CreateAsync(IDbConnection connection, Personal personal)
        {
            var sql = "INSERT INTO personal(\"FullName\",\"Address\",\"DOB\",\"PhoneNumber\") VALUES(@FullName, @Address, @DOB, @PhoneNumber)";

            var data = await connection.ExecuteAsync(sql, new { personal.FullName, personal.Address, personal.DOB, personal.PhoneNumber });
            return data;
        }

        public async Task<IEnumerable<Personal>> GetAllAsync()
        {
            using (IDbConnection connection = Connection)
            {
                return await connection.QueryAsync<Personal>("SELECT * FROM personal");
            }
        }
    }
}
