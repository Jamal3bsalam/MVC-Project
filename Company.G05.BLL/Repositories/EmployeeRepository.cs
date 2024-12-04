using Company.G05.BLL.Interface;
using Company.G05.DAL.Data.Context;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {
            
        }

        public  async Task<IEnumerable<Employee>> GetByNameAsync(string SearchInput)
        {
            return await _context.Employees.Where(E => E.Name.ToLower().Contains(SearchInput.ToLower())).Include(E => E.WorkFor).ToListAsync();
        }
    }
}
