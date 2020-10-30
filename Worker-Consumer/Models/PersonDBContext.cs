using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_Consumer.Models
{
    class PersonDBContext : DbContext
    {

        public DbSet<Person> Person { get; set; }
        public DbSet<Cotista> Cotista { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WorkerDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("Person");

            modelBuilder.Entity<Cotista>()
                .ToTable("Cotista");
        }

        


    }
}
