using System.ComponentModel.DataAnnotations;

namespace GerenciamentoAssuntos.Domain.Models
{
    public class AssuntoRequestModel
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string PalavrasChaves { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }


    }
}
