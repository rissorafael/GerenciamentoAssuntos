using GerenciamentoAssuntos.Domain.Models;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IGdeltProjectIntegration
    {
        Task<GdeltResponse> GetByNoticiasAsync(string palavrasChaves);
    }
}
