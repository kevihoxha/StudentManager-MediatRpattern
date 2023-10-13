using TestingMediatR.Models;

namespace TestingMediatR.Repositories
{
    public interface IGradeRepository : IRepository<StudentGrade>
    {
        Task AddGradeToStudentAsync(int studentId, StudentGrade grade);
        Task UpdateGradeForStudentAsync(int studentId, StudentGrade updatedGrade);
    }

}
