using System.Text.Json;

namespace ExpressMarket.Services
{
    public class ArticleService : ContentPage
    {
        List<Article> articles;
        public ArticleService()
        {

        }

        public async Task<List<Article>> GetArticles()
        {
            string stream =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");
            using(var streamReader = new StreamReader(stream))
            {
                var contents = await streamReader.ReadToEndAsync();
                articles = JsonSerializer.Deserialize<List<Article>>(contents);
            }

            return articles;
        }
    }
}
