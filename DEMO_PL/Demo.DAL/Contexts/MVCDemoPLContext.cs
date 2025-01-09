using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    
    public class MVCDemoPLContext : DbContext 
    {
        // using dependency injection
        public MVCDemoPLContext(DbContextOptions<MVCDemoPLContext>options):base(options) // instead of overriding onconfiguring
        {

        }
        #region old way 
        /*public MVCDemoPLContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("server = .; Database = MVCDempPL; Trusted_connection = true; TrustServeCertificate = true "); //MultipleActiveResultSets = true;*/
        #endregion
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
