﻿using GerenciamentoAssuntos.Infra.CrossCutting.ExtensionMethods;
using GerenciamentoAssuntos.Domain.Interfaces;
using Npgsql;


namespace GerenciamentoAssuntos.Infra.Data.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        public NpgsqlConnection Connection()
        {
            var connectionString = ValidaVariaveisDeConexaoBD();

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi possivel realizar a conexao com o banco de dados ");

            var connection = new NpgsqlConnection(connectionString);
            return connection;
        }

        private string ValidaVariaveisDeConexaoBD()
        {
            var connectionString = ExatractConfiguration.GetConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // _logger.LogInformation("Não foi encontrado a variavel de ambiente de conexão com o banco de dados");
                return string.Empty;
            }

            return connectionString;
        }
    }
}
