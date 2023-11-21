using System.ComponentModel.DataAnnotations;

namespace TestingMediatR.Models
{
    public class StudentRoles
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
