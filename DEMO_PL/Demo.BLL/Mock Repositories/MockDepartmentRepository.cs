﻿using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mock_Repositories
{
    internal class MockDepartmentRepository : IDepartmentRepository
    {
       // [testc(department)]
        public int Add(Department department)
        {
            throw new NotImplementedException();
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
