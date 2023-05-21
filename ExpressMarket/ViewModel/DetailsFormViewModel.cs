using System.Text.Json;

namespace ExpressMarket.ViewModel;

[QueryProperty(nameof(Data), "ArticleFromDetails")]
public partial class DetailsFormViewModel : ObservableObject
{
	
    [ObservableProperty]
    Article data;

    [ObservableProperty]
    Boolean rightUpdateAccess = false;

    [ObservableProperty]
    Boolean rightDestroyAccess = false;

    public DetailsFormViewModel()
	{
        RightAccess();
    }
    //RightAccess();
    public void RightAccess()
    {

       
            if (GlobalsTools.loggedUser.UserAccessType == 1)
            {
                RightUpdateAccess = true;
                RightDestroyAccess = true;

            }
            else if (GlobalsTools.loggedUser.UserAccessType == 2)
            {
                RightDestroyAccess = true;

            }
      
            
        
    }

    [RelayCommand]
     async void Delete()
     {


        if (GlobalsTools.articles.Contains(Data))
        {
            GlobalsTools.articles.Remove(Data);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");
            // Récupère le contenu JSON existant du fichier
            string jsonContent = File.ReadAllText(filePath);

            // Désérialise le contenu JSON en une liste
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articlesDelete = JsonSerializer.Deserialize<List<Article>>(jsonContent, options);


            articlesDelete.RemoveAll(article => article.Id == Data.Id);
            string updatedJsonContent = JsonSerializer.Serialize(articlesDelete, options);

            // Écrit le contenu JSON sérialisé dans le fichier
            File.WriteAllText(filePath, updatedJsonContent);
        }

          await Shell.Current.DisplayAlert("Successfully Deleted!", "You can go back.", "OK");


     }

    [RelayCommand]
    async void Update()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");

        // Récupère le contenu JSON existant du fichier
        string jsonContent = File.ReadAllText(filePath);

        // Désérialise le contenu JSON en une liste
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var articles = JsonSerializer.Deserialize<List<Article>>(jsonContent, options);

        // Recherche le joueur à modifier
        Article articleUpdate = articles.FirstOrDefault(article => article.Id == Data.Id);

        if (articleUpdate == null || string.IsNullOrWhiteSpace(Data.Creator) || string.IsNullOrWhiteSpace(Data.Type) || string.IsNullOrEmpty(Data.Creator) || string.IsNullOrEmpty(Data.Type)) {

            await Shell.Current.DisplayAlert("Modify error!", "Article not modified", "OK");
        } else 
        {
            // Met à jour les propriétés du joueur avec les nouvelles valeurs
            articleUpdate.Creator = Data.Creator;
            articleUpdate.Type = Data.Type;

            // Sérialise la liste mise à jour en JSON
            string updatedJsonContent = JsonSerializer.Serialize(articles, options);

            // Écrit le contenu JSON sérialisé dans le fichier
            File.WriteAllText(filePath, updatedJsonContent);

            await Shell.Current.DisplayAlert("Succesfully updated!", "You can go back.", "OK");
        }
    }

}