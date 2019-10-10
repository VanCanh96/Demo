using Dapper;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using NetCoreApi.Repositoties.Interface;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Repositoties.Implement
{
    public class PersonalRepository : IRepositoryBase<Personal>
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

        public async Task<int> Add(IDbConnection connection, Personal personal)
        {
            var sql = "INSERT INTO personal(\"FullName\",\"Address\",\"DOB\",\"PhoneNumber\") VALUES(@FullName, @Address, @DOB, @PhoneNumber)";

            var data = await connection.ExecuteAsync(sql, new { personal.FullName, personal.Address, personal.DOB, personal.PhoneNumber });
            return data;
        }

        public void Delete(int id)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                connection.Execute("DELETE FROM personal WHERE Id = @ID", new { Id = id });
            }
        }

        public async Task<IEnumerable<Personal>> GetAll()
        {
            using (IDbConnection connection = Connection)
            {
                //connection.Open();
                return await connection.QueryAsync<Personal>("SELECT * FROM personal");
            }
        }

        public Personal GetById(int id)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                return connection.Query<Personal>("SELECT * FROM personal WHERE id = @ID", new { ID = id }).FirstOrDefault();
            }
        }

        public void Update(Personal item)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                connection.Query("UPDATE personal SET fullname = @FullName, address =@Address, dob = @DOB, phonenumber=@PhoneNumber WHERE id = @ID", item);
            }
        }
    }
}