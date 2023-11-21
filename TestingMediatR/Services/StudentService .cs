using TestingMediatR.Models;
using TestingMediatR.Repositories;

namespace TestingMediatR.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;

        public StudentService(IStudentRepository studentRepository,IGradeRepository gradeRepository)
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<StudentDetails> AddStudentAsync(StudentDetails studentDetails)
        {
            var incrementalNumber = await _studentRepository.CountAsync() + 1;
            var names = studentDetails.StudentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var (firstName, lastName) = ExtractFirstAndLastName(studentDetails.StudentName);
            studentDetails.SerialNumber = $"{incrementalNumber}_{firstName}_{lastName}";

            var createdStudent = await _studentRepository.AddAsync(studentDetails);
            await _gradeRepository.AddGradeToStudentAsync(createdStudent.Id,createdStudent.Grade);

            return createdStudent;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }

        public async Task<StudentDetails> GetStudentByEmailAsync(string email)
        {
            var result = await _studentRepository.GetStudent(x => x.StudentEmail == email);
            return result;
        }

        public async Task<StudentDetails> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentWithGradeAsync(id);
        }

        public async Task<IQueryable<StudentDetails>> GetStudentListAsync()
        {
            return  _studentRepository.GetStudentsWithGradesAsync();
        }
        public async Task<bool> UpdateStudentAsync(StudentDetails studentDetails)
        {
            var incrementalNumber = await _studentRepository.CountAsync();
            var names = studentDetails.StudentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var (firstName, lastName) = ExtractFirstAndLastName(studentDetails.StudentName);
            studentDetails.SerialNumber = $"{incrementalNumber}_{firstName}_{lastName}";

            await _gradeRepository.UpdateGradeForStudentAsync(studentDetails.Id,studentDetails.Grade);
            await _studentRepository.UpdateAsync(studentDetails);

            return true;
        }
        private (char FirstName, char LastName) ExtractFirstAndLastName(string fullName)
        {
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = names.Length > 0 ? names[0][0] : 'X';
            var lastName = names.Length > 1 ? names[1][0] : 'X';
            return (firstName, lastName);
        }
        public async Task<bool> ExistsWithPhoneNumber(string phoneNumber, int? exceptId) =>
           await _studentRepository.Exists(x => x.PhoneNumber == phoneNumber && x.Id != exceptId);

        public async Task<bool> ExistsWithEmail(string email, int? exceptId) =>
            await _studentRepository.Exists(x => x.StudentEmail == email && x.Id != exceptId);

    }
}
