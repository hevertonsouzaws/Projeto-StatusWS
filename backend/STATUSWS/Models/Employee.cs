using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace StatusWS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }    

        [Required]
        public string Position { get; set; }

        public string Photo { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
        public bool IsActive { get; set; } = true;
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
