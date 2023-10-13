using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestingMediatR.Models;

namespace TestingMediatR.Data
{
    public class DbContextClass : DbContext
    {
        public DbSet<StudentDetails> Students { get; set; }
        public DbSet<StudentGrade> Grades { get; set; }

        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGrade>()
            .Property(g => g.GradeId)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudentDetails>()
                .HasOne(sd => sd.Grade)       
                .WithMany()                   
                .HasForeignKey(sd => sd.Id).HasPrincipalKey(c =>c.GradeId)
                .OnDelete(DeleteBehavior.Cascade); 
        }

    }
}
