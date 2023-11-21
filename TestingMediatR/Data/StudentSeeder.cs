using Microsoft.EntityFrameworkCore;
using TestingMediatR.Models;

namespace TestingMediatR.Data
{

    public class StudentSeeder
    {
        private readonly DbContextClass _context;

        public StudentSeeder(DbContextClass context)
        {
            _context = context;
        }

        public static void SeedStudents(DbContextClass context)
        {
            // Seed grades if they don't exist
            SeedGrades(context);

            // Seed roles if they don't exist
            SeedRoles(context);

            // Seed students with roles
            SeedStudentDetails(context);
        }

        private static void SeedGrades(DbContextClass _context)
        {
            if (!_context.Grades.Any(g => g.Description == "excellent"))
            {
                var excellent = new StudentGrade { Grade = 100, Description = "excellent" };
                _context.Add(excellent);
            }

            if (!_context.Grades.Any(g => g.Description == "average"))
            {
                var average = new StudentGrade { Grade = 70, Description = "average" };
                _context.Add(average);
            }

            if (!_context.Grades.Any(g => g.Description == "bad"))
            {
                var bad = new StudentGrade { Grade = 20, Description = "bad" };
                _context.Add(bad);
            }

            _context.SaveChanges();
        }

        private static void SeedRoles(DbContextClass _context)
        {
            if (!_context.Roles.Any(r => r.RoleName == "SuperAdmin"))
            {
                var superAdminRole = new StudentRoles { RoleName = "SuperAdmin" };
                _context.Add(superAdminRole);
            }

            if (!_context.Roles.Any(r => r.RoleName == "Admin"))
            {
                var adminRole = new StudentRoles { RoleName = "Admin" };
                _context.Add(adminRole);
            }

            if (!_context.Roles.Any(r => r.RoleName == "Basic"))
            {
                var basicRole = new StudentRoles { RoleName = "Basic" };
                _context.Add(basicRole);
            }

            _context.SaveChanges();
        }

        // Seed three students with roles
        private static void SeedStudentDetails(DbContextClass _context)
        {
            var studentWithDetails = _context.Students
                    .Include(s => s.Grade)
                    .Include(s => s.StudentRole)
                    .FirstOrDefault(s => s.Id == 1);
            var students = new List<StudentDetails>
        {
            new StudentDetails
            {
                Id = 1,
                  StudentName = "Super Admin",
                    StudentEmail = "superadmin@example.com",
                    StudentAge = 30,
                    StudentAddress = "Memo Meto",
                    PhoneNumber = "3556822334556",
                    SerialNumber = "1_S_A",
                GradeId = _context.Grades.Single(g => g.Description == "excellent").GradeId,
                RoleId = _context.Roles.Single(r => r.RoleName == "SuperAdmin").RoleId
            },

            new StudentDetails
            {
                 Id= 2,
                    StudentName = "Admin User",
                    StudentEmail = "admin@example.com",
                    StudentAge = 25,
                    StudentAddress = "Memo Meto",
                    PhoneNumber = "3556822334556",
                    SerialNumber = "2_A_U",
                GradeId = _context.Grades.Single(g => g.Description == "excellent").GradeId,
                RoleId = _context.Roles.Single(r => r.RoleName == "Admin").RoleId
            },
             new StudentDetails
                {
                    Id= 3,
                    StudentName = "Basic User",
                    StudentEmail = "basicuser@example.com",
                    StudentAge = 22,
                    StudentAddress = "Memo Meto",
                    PhoneNumber = "3556822334556",
                    SerialNumber = "3_B_U",
                    GradeId = _context.Grades.Single(g => g.Description == "excellent").GradeId,
                RoleId = _context.Roles.Single(r => r.RoleName == "Basic").RoleId
        } };
            _context.UpdateRange(students);
            _context.SaveChanges();

            foreach (var student in students)
            {
                var existingStudent = _context.Students.Find(student.Id);
                if (existingStudent == null)
                {
                    _context.Add(student);
                }
                else
                {
                    _context.Entry(existingStudent).CurrentValues.SetValues(student);
                }
            }

            _context.SaveChanges();
        }
    }
}


