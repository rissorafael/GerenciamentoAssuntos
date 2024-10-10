using Microsoft.Extensions.Configuration;

namespace GerenciamentoAssuntos.Infra.CrossCutting.ExtensionMethods
{
    public static class ExatractConfiguration
    {
        static IConfiguration Config;

        public static void Initialize(IConfiguration configuration)
        {
            Config = configuration;
        }

        public static string GetConnectionString
        {
            get
            {
                return GetConnection();
            }
        }

        public static string GetBaseUrlGdelt
        {
            get
            {
                return GetBaseUrlGdeltString();
            }
        }

        private static string GetConnection()
        {
            var connectionString = Config.GetConnectionString("MyPostgresConnection");
            return connectionString;
        }
        private static string GetBaseUrlGdeltString()
        {
            var chaveCriptografiaPath = Config.GetSection("GdeltApi:url").Value;
            return chaveCriptografiaPath;
        }
    }
}
