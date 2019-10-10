using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models
{
    [Table("company")]
    public class Company
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
