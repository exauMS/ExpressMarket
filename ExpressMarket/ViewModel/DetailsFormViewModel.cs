namespace ExpressMarket.ViewModel;

[QueryProperty(nameof(Data), "ArticleFromDetails")]
public partial class DetailsFormViewModel : ObservableObject
{
	
    [ObservableProperty]
    Article data;

    public DetailsFormViewModel()
	{
        
    }

   [RelayCommand]
   async void Delete(Article article)
    {
        if(GlobalsTools.articles.Contains(article))
        {
            GlobalsTools.articles.Remove(article);
        }
        await Shell.Current.DisplayAlert("Successfully Deleted!", "You can go back.", "OK");

    }

    [RelayCommand]
    async void Update(Article article)
    {
        if(string.IsNullOrWhiteSpace(article.Creator) || string.IsNullOrWhiteSpace(article.Type) || string.IsNullOrEmpty(article.Creator)|| string.IsNullOrEmpty(article.Type))
        { return; }
        foreach (var item in GlobalsTools.articles)
        {
            if(item.Code== article.Code)
            {
                item.Creator = article.Creator;
                item.Type= article.Type;
            }
        }
        await Shell.Current.DisplayAlert("Successfully Updated!", "You can go back.", "OK");

    }

    
    


   
}