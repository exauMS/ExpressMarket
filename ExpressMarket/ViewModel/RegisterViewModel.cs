namespace ExpressMarket.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
    UserManagementServices MyUserServices = new();

    [ObservableProperty]
    String userName;
    [ObservableProperty]
    Int32 userAccess;
    [ObservableProperty]
    String password;

  
    public ObservableCollection<User> ShownList { get; set; } = new();


    public RegisterViewModel(UserManagementServices myUserServices)
    {
       this.MyUserServices = myUserServices;
        MyUserServices.ConfigTools();
    }

    async Task ReadTables()
    {
        GlobalsTools.UserSet.Tables["Users"].Clear();
        GlobalsTools.UserSet.Tables["Access"].Clear();
        try
        {
           
            await MyUserServices.ReadUserTable();
            await MyUserServices.ReadAccessTable();
     
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
    }


    [RelayCommand]
    async Task AddUser()
    {
        await ReadTables();
        FillUsers();
        await MyUserServices.InsertUser(UserName, Password, UserAccess);
        FillUsers();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }


    async void FillUsers()
    {
        IsBusy = true;
        List<User> MyUserList = new();

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
      async Task GoToLoginPage()
      {
          await Shell.Current.GoToAsync("..");

         // Task Back() => Shell.Current.GoToAsync(nameof(LoginPage));
      }
}