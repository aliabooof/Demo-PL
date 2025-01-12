using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    
    public class MVCDemoPLContext :  IdentityDbContext<ApplicationUser>
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

        // i wont use these cuz i inherits IdentityDbContext which inherits another class that has properties user and roles

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }
    }
}
