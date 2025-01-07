using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    internal interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();//navigational properties is icollection
        Department Get(int id);
        int Add( Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}