namespace ExpressMarket.ViewModel;

public partial class DetailsViewModel : ContentPage
{
    public ObservableCollection<Article> Fruits { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Vegetables { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Dairies { get; set; } = new ObservableCollection<Article>();

    public DetailsViewModel()
	{
        RefreshList();
    }


    [RelayCommand]
    async Task GoToDetailsFormPage(Article data)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            await Shell.Current.GoToAsync(nameof(DetailsFormPage), true, new Dictionary<string, object>
            {
                {"ArticleFromDetails", data }

            });
        }  
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await Shell.Current.GoToAsync(nameof(DetailsFormWindowsPage), true, new Dictionary<string, object>
            {
                {"ArticleFromDetails", data }

            });
        }
           
        else
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