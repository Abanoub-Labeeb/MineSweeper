using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public class ApplicationContext : DbContext
    {
    
        #region Application Context Configuration

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //UseSqlServer is an extension method in Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=ProjectsSchema;persist security info=True; Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityModelConfiguration());
            modelBuilder.Entity<DBFunctionsResult>(e => e.HasNoKey());
        }

        #endregion

    }
}
