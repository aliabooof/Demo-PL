using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Contexts;

namespace Demo.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private MVCDemoPLContext dbcontext;

        public DepartmentRepository(MVCDemoPLContext dbcontext) // $1$ ask for object from db context
        {

             //dbcontext = /*new MVCDemoPLContext();*/  
        }
        public int Add(Department department)
        {
            throw new NotImplementedException(); // if i get a request to controller that is had to create two object
                                                 // it will open two connection and this is so bad 
                                                 // so remove creation of database and clr wil created object and it will use dependency injection look for $1$ this is the solution

        }

        public int Delete(Department department)
        {
            throw new NotImplementedException();
        }

        public Department Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
