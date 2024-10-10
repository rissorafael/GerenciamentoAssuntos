using GerenciamentoAssuntos.Domain.Models;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IConteudoService
    {
        Task<List<ConteudoResponseModel>> GetByConteudoAsync(int assuntoId);
    }
}
