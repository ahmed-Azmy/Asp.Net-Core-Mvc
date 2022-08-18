using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppG02DbContext context;

        public EmployeeRepository(MVCAppG02DbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string value)       
           => await context.Employees.Where(E => E.Name.Contains(value)).ToListAsync();            
        
       
    }
}
