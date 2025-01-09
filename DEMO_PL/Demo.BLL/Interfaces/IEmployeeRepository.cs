using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
       IQueryable<Employee> GetEmployeesByAddress(string address); // IEnumerable will get all the data then filter here
                                                                    // IQueryable will get the filtered data from db
    }
}
