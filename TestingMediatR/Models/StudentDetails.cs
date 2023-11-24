
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingMediatR.Models
{
    public class StudentDetails
    {

        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentAddress { get; set; }
        public int StudentAge { get; set; }
        public string PhoneNumber { get; set; }
        public string SerialNumber { get; set; }

        public int GradeId { get; set; }
        public StudentGrade Grade { get; set; }
        public int? RoleId { get; set; }

        public StudentRoles StudentRoles { get; set; }
    }
}
