using GerenciamentoAssuntos.Infra.CrossCutting.ExtensionMethods;
using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Mappings;
using GerenciamentoAssuntos.Infra.Data.Repositories;
using GerenciamentoAssuntos.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GerenciamentoAssuntos.Infra.Data.Connection;
using GerenciamentoAssuntos.Integration.Futebol;
using GerenciamentoAssuntos.Integration.GdeltProject;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Serilog.Exceptions;

namespace GerenciamentoAssuntos.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAssuntoService, AssuntoService>();
            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddAutoMapper(typeof(DtoMappingToProfileDomain));
            services.AddAutoMapper(typeof(DomainMappingToProfileDto));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IFutebolService, FutebolService>();
            services.AddScoped<IGdeltProjectIntegration, GdeltProjectIntegration>();
            services.AddScoped<IConteudoRepository, ConteudoRepository>();
            services.AddScoped<IConteudoService, ConteudoService>();
            ConfigureLogging();

            ExatractConfiguration.Initialize(configuration);
        }

        public static void ConfigureLogging()
        {
            var environment = "Development";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSkin(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        static ElasticsearchSinkOptions ConfigureElasticSkin(IConfigurationRoot configuration, string environment)
        {
            var teste = configuration["ElasticConfiguration:Uri"];

            var elasticsearchSinkOptions = new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = configuration["ElasticConfiguration:Index"],
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };

            return elasticsearchSinkOptions;
        }
    }
}
