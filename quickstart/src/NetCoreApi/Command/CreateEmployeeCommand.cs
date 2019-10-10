using MediatR;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Command
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string Name { get; set; }

        public DateTime Dob { get; set; }

        public CreateCompanyCommand Company { get; set; }
    }
}
