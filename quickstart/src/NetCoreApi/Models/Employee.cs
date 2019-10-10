using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Dob { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
