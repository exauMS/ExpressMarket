namespace ExpressMarket.ViewModel;

public partial class DashBoardViewModel : ObservableObject
{
    public ObservableCollection<Article> Fruits { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Vegetables { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Dairies { get; set; } = new ObservableCollection<Article>();
    ArticleService articleService;
    bool jsonTreated = false;


    [ObservableProperty]
    Boolean adminAccess=false;
    [ObservableProperty]
    Boolean userAccess = false;

    [ObservableProperty]
    string userStatus = "";


    public DashBoardViewModel()
    {
       
    }
    public DashBoardViewModel(ArticleService service)
    {
        articleService = service;
        RightAdminAccess();
    }

    public void RightAdminAccess()
    {

        if (GlobalsTools.isLogged == true)
        {
            if (GlobalsTools.loggedUser.UserAccessType == 1)
            {
                AdminAccess = true;
                UserAccess= true;
                UserStatus = "Admin : "+GlobalsTools.loggedUser.UserName;

            }
            else
            {
                UserAccess = true;
                UserStatus = "User : " + GlobalsTools.loggedUser.UserName;
            }
        }
       
       



    }

    [RelayCommand]
    async void Logout()
    {
        GlobalsTools.isLogged = false;
        GlobalsTools.loggedUser = null;
        await Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    async Task GoToDetailsPage()
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage));
    }

    [RelayCommand]
    async Task GoToScannerPage()
    {
        await Shell.Current.GoToAsync(nameof(ScannerWindowsPage));
    }

	
    [RelayCommand]
    async Task GoToAdminPage()
    {
        await Shell.Current.GoToAsync(nameof(UserPage));
    }
    

    [RelayCommand]
    async Task GetJson()
    {
        if (jsonTreated == false)
        {
            try
            {
                GlobalsTools.articles = await articleService.GetArticles();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            } 
            jsonTreated = true;
        }
        RefreshList();
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