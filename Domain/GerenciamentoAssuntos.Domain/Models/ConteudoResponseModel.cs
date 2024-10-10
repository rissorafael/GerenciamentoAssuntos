using GerenciamentoAssuntos.Domain.Validators;

namespace GerenciamentoAssuntos.Domain.Models
{
    public class ConteudoResponseModel : Validator
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int AssuntoId { get; set; }
    }
}
