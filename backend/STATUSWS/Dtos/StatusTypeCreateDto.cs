using System.ComponentModel.DataAnnotations;

namespace StatusWS.Dtos
{
    public class StatusTypeCreateDto
    {
        [Required]
        public string Description { get; set; }
        public string IconUrl { get; set; } = "https://tarefas.websupply.com.br/painel/assets/StatusGeolocalizacao-DxUl3vfK.png";
    }
}
