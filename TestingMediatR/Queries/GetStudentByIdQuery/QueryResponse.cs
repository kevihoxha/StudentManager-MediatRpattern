using TestingMediatR.Models;

namespace TestingMediatR.Queries.GetStudentByIdQuery
{
    public class StudentQueryResponse
    {
        public string StudentName { get; set; }
        public int Grade { get; set; }
        public string RoleName { get; set; }
    }
}
