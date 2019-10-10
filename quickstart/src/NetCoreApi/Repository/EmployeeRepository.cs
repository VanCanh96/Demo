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
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
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

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (IDbConnection connection = Connection)
            {
                string sql = "select * from employee e inner join company c on e.\"CompanyId\" = c.\"Id\"";
                return await connection.QueryAsync<Employee, Company, Employee>(sql, (employee, company) => { employee.Company = company; return employee; });
            }
        }

        public async Task<int> CreateAsync(IDbConnection connection, Employee employee)
        {
            var sql = "INSERT INTO employee(\"Name\", \"Dob\", \"CompanyId\") VALUES(@Name, @Dob, @CompanyId)";
            var data = await connection.ExecuteAsync(sql, new { employee.Name, employee.Dob, employee.CompanyId });
            return data;
        }
    }
}
