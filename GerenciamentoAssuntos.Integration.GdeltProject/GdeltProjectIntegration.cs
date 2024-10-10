using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using GerenciamentoAssuntos.Infra.CrossCutting.ExtensionMethods;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Web;

namespace GerenciamentoAssuntos.Integration.GdeltProject
{
    public class GdeltProjectIntegration : IGdeltProjectIntegration
    {
        private readonly ILogger<GdeltProjectIntegration> _logger;
        public GdeltProjectIntegration(ILogger<GdeltProjectIntegration> logger)
        {
            _logger = logger;
        }

        public async Task<GdeltResponse> GetByNoticiasAsync(string palavrasChaves)
        {
            try
            {
                _logger.LogInformation($"[GdeltProjectIntegration - GetByNoticiasAsync] - parameters {palavrasChaves}");

                var baseUrl = ExatractConfiguration.GetBaseUrlGdelt;

                var queryParams = HttpUtility.ParseQueryString(string.Empty);
                queryParams["query"] = palavrasChaves;
                queryParams["mode"] = "artlist";
                queryParams["format"] = "json";

                var uriBuilder = new UriBuilder(baseUrl);
                uriBuilder.Query = queryParams.ToString();

                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(uriBuilder.Uri);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var resp = JsonSerializer.Deserialize<GdeltResponse>(responseBody);

                    return resp;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[GdeltProjectIntegration - GetByNoticiasAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }
    }
}
