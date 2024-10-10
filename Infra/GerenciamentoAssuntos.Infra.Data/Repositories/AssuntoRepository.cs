using Dapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Interfaces;

namespace GerenciamentoAssuntos.Infra.Data.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AssuntoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Assunto> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Assunto>(@"
                                                    SELECT 
                                                       Id
                                                      ,Titulo
                                                      ,PalavrasChaves
                                                      ,Status
                                                      ,DataCriacao
                                                      ,DataAtualizacao
                                                    FROM Assunto
                                                      Where Id =@id",
                new
                {
                    Id = id
                });

            return result;

        }

        public async Task<IEnumerable<Assunto>> GetAllAsync()
        {
            try
            {
                using var connection = _connectionFactory.Connection();
                var result = await connection.QueryAsync<Assunto>(@"
                                                SELECT 
                                                       Id
                                                      ,Titulo
                                                      ,PalavrasChaves
                                                      ,Status
                                                      ,DataCriacao
                                                      ,DataAtualizacao
                                                    FROM Assunto
                                                      Where 1=1");

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Assunto> GetByAsssunto(string palavrasChaves)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Assunto>(@"
                                                SELECT 
                                                       Id
                                                      ,Titulo
                                                      ,PalavrasChaves
                                                      ,Status
                                                      ,DataCriacao
                                                      ,DataAtualizacao
                                                    FROM Assunto
                                                      Where PalavrasChaves = @palavrasChaves",
                                 new
                                 {
                                     palavrasChaves = palavrasChaves
                                 });

            return result;

        }

        public async Task<Assunto> AddAsync(Assunto assuntos)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Assunto>(@"
                              
                              INSERT INTO Assunto
                              (
                                 Titulo
                                ,PalavrasChaves
                                ,Status
                                ,DataCriacao
                                ,DataAtualizacao
                              )
                              VALUES 
                              (
                                 @Titulo
                                ,@PalavrasChaves
                                ,@Status
                                ,@DataCriacao
                                ,@DataAtualizacao
                              )
                           RETURNING 
                                Id
                               ,Titulo
                               ,PalavrasChaves
                               ,Status
                               ,DataCriacao
                               ,DataAtualizacao
                          ;",
                new
                {
                    assuntos.Titulo,
                    assuntos.PalavrasChaves,
                    assuntos.Status,
                    assuntos.DataCriacao,
                    assuntos.DataAtualizacao,
                });

            return result;

        }

        public async Task<Assunto> UpdateAsync(Assunto assuntos)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Assunto>(@"
                              
                              UPDATE Assunto
                                 SET Titulo = @Titulo
   	                                ,palavraschaves = @PalavrasChaves
	                                ,status = @Status
	                                ,dataatualizacao = @DataAtualizacao
                                WHERE Id = @Id
                           RETURNING 
                                Id
                               ,Titulo
                               ,PalavrasChaves
                               ,Status
                               ,DataCriacao
                               ,DataAtualizacao
                             ;",
                new
                {
                    assuntos.Id,
                    assuntos.Titulo,
                    assuntos.PalavrasChaves,
                    assuntos.Status,
                    assuntos.DataAtualizacao,
                });

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.ExecuteAsync(@"
                              
                              DELETE FROM  Assunto
                                 WHERE Id = @id
                            ;",
                new
                {
                    Id = id
                });

            return result;

        }
    }
}
