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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly  MVCDemoPLContext _dbcontext; //

        public DepartmentRepository(MVCDemoPLContext dbcontext) // $1$ ask CLR for object from db context
                                                                // injection mean that dbcontext will hold ref for created object
        {

            //dbcontext = /*new MVCDemoPLContext();*/  $1$
            _dbcontext = dbcontext;
        }
        public int Add(Department department)
        {
            _dbcontext.departments.Add(department);     // if i get a request to controller that is had to create two object
                                                        // it will open two connection and this is so bad 
                                                        // so remove creation of database and clr wil created object and it will use dependency injection look for $1$ this is the solution
            return _dbcontext.SaveChanges();

        }

        public int Delete(Department department)
        {
            _dbcontext.departments.Remove (department); // WITH TRACKING
            return _dbcontext.SaveChanges();


        }

        public Department Get(int id)
        {
            
            /*var department = _dbcontext.departments.Local.Where(d=>d.Id==id).FirstOrDefault();

            if(department is null)
                department = _dbcontext.departments.Where(d => d.Id == id).FirstOrDefault();

            return department; */

            return _dbcontext.departments.Find(id);


        }

        public IEnumerable<Department> GetAll()
            => _dbcontext.departments.ToList();
       

        public int Update(Department department)
        {
            _dbcontext.departments.Update(department); // WITH TRACKING
            return _dbcontext.SaveChanges();
        }
    }
}
