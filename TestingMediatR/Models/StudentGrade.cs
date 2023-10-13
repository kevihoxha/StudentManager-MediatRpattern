using System.ComponentModel.DataAnnotations;

namespace TestingMediatR.Models
{
    public class StudentGrade
    {
        [Key]
        public int GradeId { get; set; }
        public int Grade {  get; set; }
        public string Description { get; set; }
    }
}
