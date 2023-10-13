using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using System.Diagnostics;
using TestingMediatR.Data;
using TestingMediatR.Models;

namespace TestingMediatR.Repositories
{
    public class GradeRepository : BaseRepository<StudentGrade>, IGradeRepository
    {
        private readonly DbContextClass _dbcontext;
        public GradeRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task AddGradeToStudentAsync(int studentId, StudentGrade grade)
        {
            var student = await _dbcontext.Students.FindAsync(studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            student.GradeId = grade.GradeId;
            student.Grade = grade;

            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateGradeForStudentAsync(int studentId, StudentGrade updatedGrade)
        {
            var student = await _dbContext.Students.FindAsync(studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            var currentGrade = await _dbContext.Grades.FindAsync(student.GradeId);

            if (currentGrade == null)
            {
                throw new NotFoundException("Current grade not found.");
            }

            currentGrade.Grade = updatedGrade.Grade;
            currentGrade.Description = updatedGrade.Description;

            await _dbContext.SaveChangesAsync();
        }
    }
}
