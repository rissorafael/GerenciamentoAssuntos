using Dapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Interfaces;

namespace GerenciamentoAssuntos.Infra.Data.Repositories
{
    public class ConteudoRepository : IConteudoRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ConteudoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Conteudo>> GetByAsssuntoId(int assuntoId)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryAsync<Conteudo>(@"
                                                SELECT 
                                                       Id
                                                      ,Link
                                                      ,AssuntoId
                                                    FROM Conteudo
                                                      Where AssuntoId = @assuntoId",
                                 new
                                 {
                                     assuntoId = assuntoId
                                 });

            return result;
        }

        public async Task<int> AddlistAsync(List<Conteudo> conteudoList)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.ExecuteAsync(@"
                              
                              INSERT INTO Conteudo
                              (
                                 Link
                                ,AssuntoId
                             )
                              VALUES 
                              (
                                 @Link
                                ,@AssuntoId
                            )
                           RETURNING 
                                Id
                               ,Link
                               ,AssuntoId
                           ;",
                           conteudoList);


            return result;

        }
    }
}
