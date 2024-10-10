namespace GerenciamentoAssuntos.Domain.Entities
{
    public class Assunto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string PalavrasChaves { get; set; }
        public int Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
