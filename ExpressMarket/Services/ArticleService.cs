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
            string stream = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");
            using (var streamReader = new StreamReader(stream))
            {
                var contents = await streamReader.ReadToEndAsync();
                articles = JsonSerializer.Deserialize<List<Article>>(contents);
            }

            return articles;
        }

        public async void DeleteArticle(Article Data)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");
            // Récupère le contenu JSON existant du fichier
            string jsonContent = "";
            try
            {
                jsonContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            // Désérialise le contenu JSON en une liste
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articlesDelete = JsonSerializer.Deserialize<List<Article>>(jsonContent, options);


            articlesDelete.RemoveAll(article => article.Id == Data.Id);
            string updatedJsonContent = JsonSerializer.Serialize(articlesDelete, options);

            // Écrit le contenu JSON sérialisé dans le fichier
            try
            {
                File.WriteAllText(filePath, updatedJsonContent);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            await Shell.Current.DisplayAlert("Successfully Deleted!", "You can go back.", "OK");
            
        }

        public async void UpdateArticle(Article Data)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");

            // Récupère le contenu JSON existant du fichier
            string jsonContent = "";
            try
            {
                jsonContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            // Désérialise le contenu JSON en une liste
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articles = JsonSerializer.Deserialize<List<Article>>(jsonContent, options);

            // Recherche l'article à modifier
            Article articleUpdate = articles.FirstOrDefault(article => article.Id == Data.Id);

            if (articleUpdate == null || string.IsNullOrWhiteSpace(Data.Creator) || string.IsNullOrWhiteSpace(Data.Type) || string.IsNullOrEmpty(Data.Creator) || string.IsNullOrEmpty(Data.Type))
            {

                await Shell.Current.DisplayAlert("Modify error!", "Article not modified", "OK");
            }
            else
            {
                // Met à jour les propriétés de l'article avec les nouvelles valeurs
                articleUpdate.Creator = Data.Creator;
                articleUpdate.Type = Data.Type;

                // Sérialise la liste mise à jour en JSON
                string updatedJsonContent = JsonSerializer.Serialize(articles, options);

                // Écrit le contenu JSON sérialisé dans le fichier
                try
                {
                    File.WriteAllText(filePath, updatedJsonContent);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }

                await Shell.Current.DisplayAlert("Succesfully updated!", "You can go back.", "OK");
            }
        }
    }
}
