using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Interface
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get;}
        public IDepartmentRepository DepartmentRepository { get;}

        int Complete();
    }
}
