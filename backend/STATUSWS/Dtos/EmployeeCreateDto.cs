using System.ComponentModel.DataAnnotations;

namespace StatusWS.Dtos
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "A senha deve ter entre 4 e 50 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O cargo deve ter entre 3 e 100 caracteres.")]
        public string Position { get; set; }
        public string? Photo { get; set; }
    }
}
