using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        /*private readonly MVCDemoPLContext _dbcontext;*/ // i inherits dbcontext from genericrepository cuz its private protected if it private i wont inherit it

        public EmployeeRepository(MVCDemoPLContext dbcontext) : base(dbcontext)
        {
         
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchEmployeesByName(string name)=>
           _dbcontext.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())); 
        
    }
}
