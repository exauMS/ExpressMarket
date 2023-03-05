using System.Collections.ObjectModel;

namespace ExpressMarket.ViewModel;

public partial class DashBoardViewModel : ContentPage
{
    public ObservableCollection<Article> Fruits { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Vegetables { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Dairies { get; set; } = new ObservableCollection<Article>();
    ArticleService articleService;


    public DashBoardViewModel()
    {

    }
    public DashBoardViewModel(ArticleService service)
    {
        articleService = service;
       
    }

    [RelayCommand]
    async Task GoToDetailsPage()
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage));
    }


    [RelayCommand]
    async Task GetJson()
    {
        try
        {
            GlobalsTools.articles = await articleService.GetArticles();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");

        }
        foreach (var item in GlobalsTools.articles)
        {
            if (item.Type.Equals("Fruit"))
                Fruits.Add(item);
            else if (item.Type.Equals("Vegetable"))
                Vegetables.Add(item);
            else if (item.Type.Equals("Dairy"))
                Dairies.Add(item);

        }
    }

}