using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Command
{
    public class PersonalCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
    }
}
