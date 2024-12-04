using Company.G05.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Interface
{
    public interface IEmployeeRepository:IGenenricRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
        Task<IEnumerable<Employee>> GetByNameAsync(string Name);
    }
}
