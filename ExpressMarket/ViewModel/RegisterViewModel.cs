namespace ExpressMarket.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
    UserManagementServices MyUserServices = new();

    [ObservableProperty]
    String userNameRegister;
    [ObservableProperty]
    String passwordRegister;

  
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
            await MyUserServices.ReadAccessTable();
            await MyUserServices.ReadUserTable();
            
     
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
        await MyUserServices.InsertUser(UserNameRegister, PasswordRegister, 2);
        FillUsers();
        await Shell.Current.GoToAsync("..");
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
            await Shell.Current.DisplayAlert("Fill Register Database", ex.Message, "OK");
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

      }
}