using GerenciamentoAssuntos.Domain.Interfaces;

namespace GerenciamentoAssuntos.Integration.Futebol
{
    public class FutebolService : IFutebolService
    {
        public FutebolService()
        {

        }

        public async Task GetClubeFutebol(string nomeClube)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://www.thesportsdb.com/api/v1/json/3/searchteams.php?t={nomeClube}";


                    HttpResponseMessage response = await client.GetAsync(url);


                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}