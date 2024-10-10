using GerenciamentoAssuntos.Domain.Models;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IAssuntoService
    {
        Task<List<AssuntoResponseModel>> GetAllAsync();
        Task<AssuntoResponseModel> GetByAsssunto(string assuntosNome);
        Task<AssuntoResponseModel> AddAsync(AssuntoRequestModel request);
        Task<AssuntoResponseModel> UpdateAsync(AssuntoReqModel request);
        Task<AssuntoResponseModel> DeleteAsync(int id);
        Task<AssuntoResponseModel> GetByIdAsync(int id);
    }
}
