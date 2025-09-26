using System.ComponentModel.DataAnnotations;

namespace StatusWS.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public StatusType StatusType { get; set; }
        public string? CustomText { get; set; }
        public DateTimeOffset UpdateAt { get; set; }
        public int StatusTypeId { get; set; }
    }
}
