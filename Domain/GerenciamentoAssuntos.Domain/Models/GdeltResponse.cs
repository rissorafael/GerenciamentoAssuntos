
namespace GerenciamentoAssuntos.Domain.Models
{
    public class GdeltResponse
    {
        public List<Article> articles { get; set; }

        public class Article
        {
            public string url { get; set; }
            public string url_mobile { get; set; }
            public string title { get; set; }
            public string seendate { get; set; }
            public string socialimage { get; set; }
            public string domain { get; set; }
            public string language { get; set; }
            public string sourcecountry { get; set; }
        }


    }
}
