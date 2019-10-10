using MediatR;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using NetCoreApi.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCoreApi.Command
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly string _connectionString;
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Company> _companyRepository;
        public CreateEmployeeCommandHandler(IConfiguration configuration, IRepository<Employee> repository, IRepository<Company> companyRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (IDbConnection connection = new NpgsqlConnection(_connectionString))
                {
                    var id = await _companyRepository.CreateAsync(connection, new Company
                    {
                        Name = request.Company.Name,
                        Address = request.Company.Address
                    });

                    await _repository.CreateAsync(connection, new Employee
                    {
                        Name = request.Name,
                        Dob = request.Dob,
                        CompanyId = id
                    });

                    //insert con
                    scope.Complete();
                    scope.Dispose();
                    return id;
                }
            }

        }
    }
}
