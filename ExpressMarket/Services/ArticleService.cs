using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            using var stream = await FileSystem.OpenAppPackageFileAsync("DATA.json");
            using var streamReader = new StreamReader(stream);
            var contents = await streamReader.ReadToEndAsync();
            articles = JsonSerializer.Deserialize<List<Article>>(contents);

            return articles;
        }
    }
}
