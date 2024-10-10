using System.ComponentModel.DataAnnotations;


namespace GerenciamentoAssuntos.Domain.Models
{
    public class AssuntoReqModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string PalavrasChaves { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime DataAtualizacao { get; set; }
    }
}
