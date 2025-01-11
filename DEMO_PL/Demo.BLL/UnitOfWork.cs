using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IUnitPOfWork
    {
        private readonly MVCDemoPLContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get ; set; }
        public UnitOfWork(MVCDemoPLContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);

            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public int Complete()
        {
           return _dbContext.SaveChanges();
        }
    }
}
