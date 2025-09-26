namespace StatusWS.Dtos
{
    public class StatusDto
    {
        public int StatusId { get; set; }
        public string? CustomText { get; set; }
        public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;
        public StatusTypeDto StatusType { get; set; }
    }
}
