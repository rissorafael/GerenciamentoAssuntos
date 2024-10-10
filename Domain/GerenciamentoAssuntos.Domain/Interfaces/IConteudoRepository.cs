
using GerenciamentoAssuntos.Domain.Entities;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IConteudoRepository
    {
        Task<IEnumerable<Conteudo>> GetByAsssuntoId(int assuntoId);
        Task<int> AddlistAsync(List<Conteudo> conteudoList);
    }
}
