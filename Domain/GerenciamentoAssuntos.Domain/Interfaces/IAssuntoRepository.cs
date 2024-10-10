using GerenciamentoAssuntos.Domain.Entities;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IAssuntoRepository
    {
        Task<Assunto> GetByIdAsync(int id);
        Task<IEnumerable<Assunto>> GetAllAsync();
        Task<Assunto> GetByAsssunto(string palavrasChaves);
        Task<Assunto> AddAsync(Assunto assuntos);
        Task<Assunto> UpdateAsync(Assunto assuntos);
        Task<int> DeleteAsync(int id);
    }
}
