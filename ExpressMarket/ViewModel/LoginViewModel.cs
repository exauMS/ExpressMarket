namespace ExpressMarket.ViewModel;

public partial class LoginViewModel : BaseViewModel
{

	UserManagementServices MyUserServices = new();
    public ObservableCollection<User> ShownList { get; set; } = new();

    [ObservableProperty]
    string userNameLogin;
    [ObservableProperty]
    string userPasswordLogin;

    Boolean isFind =false;

	public LoginViewModel(UserManagementServices MyUserServices)
	{
        this.MyUserServices = MyUserServices;
        MyUserServices.ConfigTools();
    }

	[RelayCommand]
	async Task GoDashBoard()
	{
        await ReadAccess();
        FillUsers();



        foreach (var item in ShownList) // lecture de la liste des User de la DB
        {
            if (UserNameLogin == item.UserName ) // Verification si le USER existe bien
            {
                if (UserPasswordLogin == item.UserPassword) // Si son mot de passe est le bon
                {
                    try
                    {
                        GlobalsTools.loggedUser = item;
                        GlobalsTools.isLogged = true;
                        await Shell.Current.GoToAsync(nameof(DashBoardWindowsPage), true); // Alors il peux naviguer dans le dashboard 
                       
                        isFind = true;
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Erreur de connexion", ex.Message, "OK");
                    }
                }
            }

        }

        if (isFind==false)
        { 
            await Shell.Current.DisplayAlert("Erreur de connexion","Votre nom d'utilisateur et/ou votre mot de passe est incorrecte","OK");
            UserNameLogin = "";
            UserPasswordLogin = "";
        }
    }

    async Task ReadAccess()
    {
        GlobalsTools.UserSet.Tables["Users"].Clear();
        GlobalsTools.UserSet.Tables["Access"].Clear();
        try
        {
            await MyUserServices.ReadAccessTable();
            await MyUserServices.ReadUserTable();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
    }


    async void FillUsers()
    {
        IsBusy = true;
        List<User> MyUserList = new();
        ShownList.Clear();
        try
        {

            MyUserList = GlobalsTools.UserSet.Tables["users"].AsEnumerable().Select(e => new User()
            {
                User_ID = e.Field<Int16>("User_ID"),
                UserName = e.Field<String>("UserName"),
                UserPassword = e.Field<String>("UserPassword"),
                UserAccessType = e.Field<Int16>("UserAccessType"),
            }).ToList();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }


        foreach (var item in MyUserList)
        {
            ShownList.Add(item);
        }
        GlobalsTools.UserListFromDB = ShownList; //Intégration de notre liste de User dans notre liste global
        IsBusy = false;

    }

    [RelayCommand]
    async Task GoToRegisterPage()
	{
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}