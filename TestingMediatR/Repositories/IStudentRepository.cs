using System.Linq.Expressions;
using TestingMediatR.Models;

namespace TestingMediatR.Repositories
{
    public interface IStudentRepository : IRepository<StudentDetails>
    {
        Task<int> CountAsync();
        Task<StudentDetails> GetStudentWithDetailsAsync(int studentId);
        public IQueryable<StudentDetails> GetStudentsWithGradesAsync();
    }
}
