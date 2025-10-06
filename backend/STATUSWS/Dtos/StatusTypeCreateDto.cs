using System.ComponentModel.DataAnnotations;

namespace StatusWS.Dtos
{
    public class StatusTypeCreateDto
    {
        [Required(ErrorMessage = "A descrição do Status Type é obrigatória.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 100 caracteres.")]
        public string Description { get; set; }

        [RegularExpression(@"^.*\.(gif|png|jpe?g)$", ErrorMessage = "A URL do ícone deve terminar com .gif, .png ou .jpg.")]
        public string IconUrl { get; set; } = "https://cdn-icons-gif.flaticon.com/8756/8756038.gif";
    }
}
