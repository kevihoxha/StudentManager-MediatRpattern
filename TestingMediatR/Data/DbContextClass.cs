using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestingMediatR.Models;

namespace TestingMediatR.Data
{
    public class DbContextClass : DbContext
    {
        public DbSet<StudentDetails> Students { get; set; }
        public DbSet<StudentGrade> Grades { get; set; }
        public DbSet<StudentRoles> Roles { get; set; }


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
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(
            new IdentityRoleClaim<string> { Id = 1, RoleId = "1", ClaimType = CustomClaimTypes.Permission, ClaimValue = "CanViewStudentDetails" },
            new IdentityRoleClaim<string> { Id = 2, RoleId = "2", ClaimType = CustomClaimTypes.Permission, ClaimValue = "CanEditStudentDetails" }
);
            modelBuilder.Entity<StudentGrade>()
            .Property(g => g.GradeId)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudentDetails>()
                .HasOne(sd => sd.Grade)
                .WithMany()
                .HasForeignKey(sd => sd.Id).HasPrincipalKey(c => c.GradeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentDetails>()
                .HasOne(sd => sd.StudentRoles)
                .WithMany()
                .HasForeignKey(sd => sd.Id).HasPrincipalKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
