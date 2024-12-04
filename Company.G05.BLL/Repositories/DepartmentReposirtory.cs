using Company.G05.BLL.Interface;
using Company.G05.DAL.Data.Context;
using Company.G05.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Repositories
{
     
    public class DepartmentReposirtory :GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentReposirtory(AppDbContext context ):base(context)  // Ask CLR object From APPDbcontext
        {
           
        }

        

       
    }
}
