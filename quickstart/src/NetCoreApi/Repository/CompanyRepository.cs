using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using Npgsql;

namespace NetCoreApi.Repository
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuration)
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

        public async Task<int> CreateAsync(IDbConnection connection, Company company)
        {
            var sql = "INSERT INTO company(\"Name\", \"Address\") VALUES(@Name, @Address) RETURNING company.\"Id\";";
            return await connection.QueryFirstAsync<int>(sql, new { company.Name, company.Address });
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            using (IDbConnection connection = Connection)
            {
                var sql = "SELECT * FROM company c left join employee e on c.\"Id\" = e.\"CompanyId\"";
                var lookup = new Dictionary<Int64, Company>();

                var data =  await connection.QueryAsync<Company, Employee, Company>(sql,
                    (c, e) =>
                {
                    Company cm;
                    if (!lookup.TryGetValue(c.Id, out cm))
                    {
                        lookup.Add(c.Id, cm = c);
                    }

                    if (e != null)
                    {
                        if (cm.Employees == null)
                            cm.Employees = new List<Employee>();
                        cm.Employees.Add(e);
                    }

                    return cm;
                });

                return data.Distinct();
            }
        }
    }
}
