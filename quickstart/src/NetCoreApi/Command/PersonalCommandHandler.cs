using MediatR;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using NetCoreApi.Repository;
using Npgsql;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCoreApi.Command
{
    public class PersonalCommandHandler : IRequestHandler<PersonalCommand, int>
    {
        private string _connectionString;
        private IRepository<Personal> _personalRepo;

        public PersonalCommandHandler(IConfiguration configuration, IRepository<Personal> personalRepo)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _personalRepo = personalRepo;
        }

        public async Task<int> Handle(PersonalCommand request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (IDbConnection connection = new NpgsqlConnection(_connectionString))
                {
                    var result = await _personalRepo.CreateAsync(connection, new Personal
                    {
                        FullName = request.FullName,
                        Address = request.Address,
                        DOB = request.DOB,
                        PhoneNumber = request.PhoneNumber
                    });
                   
                    scope.Complete();
                    
                    scope.Dispose();

                    return result;

                }
            }
        }
    }
}