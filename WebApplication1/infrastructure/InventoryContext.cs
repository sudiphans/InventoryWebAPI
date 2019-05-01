using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;
using Oracle.EntityFrameworkCore;

namespace EmployeeService.infrastructure
{
    public class InventoryContext : DbContext
    {

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CDetail>()
                .HasOne<CLoan>(c => c.CLoan)
                .WithOne(e => e.details)
                .HasForeignKey<CLoan>(f => f.CDetailId);
                
        }
        

        //listing all the tables
        public DbSet<CLoan> CLoans { get; set; }

        public DbSet<CDetail> CDetails { get; set; }
    }
}
