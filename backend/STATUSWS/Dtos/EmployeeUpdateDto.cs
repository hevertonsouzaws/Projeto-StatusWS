namespace StatusWS.Dtos
{
    public class EmployeeUpdateDto
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
        public int? StatusTypeId { get; set; }        
        public string? CustomText { get; set; }
        public bool? IsActive { get; set; }
    }
}
