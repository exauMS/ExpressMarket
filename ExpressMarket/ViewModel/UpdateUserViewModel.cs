namespace ExpressMarket.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class UpdateUserViewModel : BaseViewModel
{

    UserManagementServices MyUserServices = new();
    public ObservableCollection<User> ShownList { get; set; } = new();


    [ObservableProperty]
    User user;

    public UpdateUserViewModel(UserManagementServices MyUserServices)
    {
        this.MyUserServices = MyUserServices;
        MyUserServices.ConfigTools();
    }

    [RelayCommand]
    async Task ModifyUser()
    {
        IsBusy = false;
       
        await MyUserServices.UpdateUser(User.UserName, User.UserPassword, User.UserAccessType);

        await FillUsers();
        IsBusy = true;

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


    async Task FillUsers()
    {
     
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
        ShownList.Clear();

        foreach (User item in MyUserList)
        {
            ShownList.Add(item);
        }
        GlobalsTools.UserListFromDB = ShownList; //Intégration de notre liste de User dans notre liste global
       

    }
}