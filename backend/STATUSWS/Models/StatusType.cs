using System.ComponentModel.DataAnnotations;

namespace StatusWS.Models
{
    public class StatusType
    {
        [Key]
        public int StatusTypeId { get; set; }
        [Required]
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public ICollection<Status> Statuses { get; set; }
    }
}
