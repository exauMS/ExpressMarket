namespace ExpressMarket.ViewModel;

public partial class DetailsViewModel : ObservableObject
{


    public ObservableCollection<Article> Fruits { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Vegetables { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Dairies { get; set; } = new ObservableCollection<Article>();

    [ObservableProperty]
    Boolean isConnected=false;

 

    public DetailsViewModel()
	{
        RefreshList();
        if (GlobalsTools.isLogged==true)
        {
            isConnected = true;
        }
    }

    [RelayCommand]
    async Task GoToDetailsFormPage(Article data)
    {
       
       
        
            await Shell.Current.GoToAsync(nameof(DetailsFormWindowsPage), true, new Dictionary<string, object>
            {
                {"ArticleFromDetails", data }

            });
        
           
      
    }

    public void RefreshList()
    {
        Fruits.Clear();
        Vegetables.Clear();
        Dairies.Clear();
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