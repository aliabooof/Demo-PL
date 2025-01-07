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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("server = .; Database = MVCDempPL; Trusted_connection = true; TrustServeCertificate = true "); //MultipleActiveResultSets = true; 
       public DbSet<Department> departmen { get; set; }
    }
}
