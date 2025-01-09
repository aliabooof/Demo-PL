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
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private readonly MVCDemoPLContext _dbcontext;
        public GenericRepository(MVCDemoPLContext dbcontext)

        {


            _dbcontext = dbcontext;
        }
        public int Add(T item)
        {
            _dbcontext.Set<T>().Add(item);
            return _dbcontext.SaveChanges();

        }

        public int Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);
            return _dbcontext.SaveChanges();


        }

        public T Get(int id)
        {


            return _dbcontext.Set<T>().Find(id);


        }

        public IEnumerable<T> GetAll()
            => _dbcontext.Set<T>().ToList();


        public int Update(T item)
        {
            _dbcontext.Set<T>().Update(item);
            return _dbcontext.SaveChanges();
        }
    }
}
