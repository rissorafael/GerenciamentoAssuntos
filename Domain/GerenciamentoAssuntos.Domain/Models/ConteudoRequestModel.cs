
namespace GerenciamentoAssuntos.Domain.Models
{
    public class ConteudoRequestModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int AssuntoId { get; set; }
    }
}
