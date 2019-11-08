using System;
using Hogwarts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Data
{
    class HogwartsContext : DbContext
    {

        public DbSet<Student> Student { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Course> Course { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Hogwarts;User=sa;Password=Doglover420;");
            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>()
            .HasKey(x => new { x.StudentId, x.CourseId });
        }

    }
}
