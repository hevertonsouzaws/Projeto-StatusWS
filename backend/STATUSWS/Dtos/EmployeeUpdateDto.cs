using System.ComponentModel.DataAnnotations;

namespace StatusWS.Dtos
{
    public class EmployeeUpdateDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "A senha deve ter no mínimo 4 caracteres.")]
        public string? Password { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
        public int? StatusTypeId { get; set; }        
        public string? CustomText { get; set; }
        public bool? IsActive { get; set; }
    }
}
