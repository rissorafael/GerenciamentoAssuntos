using Npgsql;

namespace GerenciamentoAssuntos.Domain.Interfaces
{
    public interface IConnectionFactory
    {
        NpgsqlConnection Connection();
    }
}
