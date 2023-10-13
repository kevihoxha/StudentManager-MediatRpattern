using TestingMediatR.Models;

namespace TestingMediatR.Services
{
    public interface IStudentService
    {
        Task<StudentDetails> AddStudentAsync(StudentDetails studentDetails);
        Task<bool> DeleteStudentAsync(int id);
        Task<StudentDetails> GetStudentByIdAsync(int id);
        Task<StudentDetails> GetStudentByEmailAsync(string email);
        Task<IQueryable<StudentDetails>> GetStudentListAsync();
        Task<bool> UpdateStudentAsync(StudentDetails studentDetails);
        Task<bool> ExistsWithEmail(string email, int? exceptId);
        Task<bool> ExistsWithPhoneNumber(string phoneNumber, int? exceptId);


    }
}
