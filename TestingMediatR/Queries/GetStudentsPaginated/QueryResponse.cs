using TestingMediatR.Models;

namespace TestingMediatR.Queries.GetStudentsPaginated
{
    public class QueryResponse
    {
        public List<StudentDetails> Students { get; set; } = new List<StudentDetails>();
        public int Pages { get; set; } 
        public int CurrentPage { get; set; }
    }
}
