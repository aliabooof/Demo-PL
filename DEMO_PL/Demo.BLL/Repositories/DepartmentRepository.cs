 using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Contexts;
using System.ComponentModel;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MVCDemoPLContext dbcontext):base(dbcontext) // asking clr to create obj from dbcontext
        {
            
        }

    }
}
