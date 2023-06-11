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
    ArticleService articleService;

    public DetailsFormViewModel(ArticleService service)
	{
        articleService = service;
        RightAccess();
    }

    public DetailsFormViewModel()
    {
        RightAccess();
    }

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
      void Delete()
     {


        if (GlobalsTools.articles.Contains(Data))
        {
            GlobalsTools.articles.Remove(Data);
            articleService.DeleteArticle(Data);
        }

         


     }

    [RelayCommand]
     void Update()
    {
        articleService.UpdateArticle(Data);
    }

}