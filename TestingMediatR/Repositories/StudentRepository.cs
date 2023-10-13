using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TestingMediatR.Data;
using TestingMediatR.Models;

namespace TestingMediatR.Repositories
{
    public class StudentRepository : BaseRepository<StudentDetails>, IStudentRepository
    {
        private readonly DbContextClass _dbcontext;
        public StudentRepository(DbContextClass dbContext) : base(dbContext)

        {
            _dbcontext = dbContext;
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext.Students.CountAsync();
        }
        public async Task<StudentDetails> GetStudentWithGradeAsync(int studentId)
        {
            return await _dbcontext.Students
                .Include(s => s.Grade) 
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }
        public IQueryable<StudentDetails> GetStudentsWithGradesAsync()
        {
            return  _dbcontext.Students
                .Include(s => s.Grade)
                .AsQueryable();
        }
    }
}
