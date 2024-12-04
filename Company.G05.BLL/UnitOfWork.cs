using Company.G05.BLL.Interface;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IDepartmentRepository _departmentRepository { get; set; }
        public IEmployeeRepository  _employeeRepository { get; set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _departmentRepository = new DepartmentReposirtory(context);
            _employeeRepository = new EmployeeRepository(context);
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public  int Complete()
        {
             var count =  _context.SaveChanges();
             return count;
        }

    }
}
