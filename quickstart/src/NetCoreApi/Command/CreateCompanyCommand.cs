using MediatR;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Command
{
    public class CreateCompanyCommand
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
