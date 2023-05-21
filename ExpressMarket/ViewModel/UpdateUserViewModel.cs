namespace ExpressMarket.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class UpdateUserViewModel : BaseViewModel
{

    UserManagementServices MyUserServices;
    public ObservableCollection<User> ShownList { get; set; } = new();


    [ObservableProperty]
    User user;
    [ObservableProperty]
    bool canDelete;
    public UpdateUserViewModel()
    {
        this.MyUserServices = new();
        MyUserServices.ConfigTools();
        canDelete= true;
    }

    [RelayCommand]
    async Task ModifyUser()
    {
        IsBusy = false;

        //windows only
        try
        {
            await MyUserServices.UpdateUser(User.User_ID, User.UserName, User.UserPassword, User.UserAccessType);
            await Shell.Current.DisplayAlert("User updated!", "You can go back.", "OK");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("User not updated!", ex.Message, "OK");
        }

        await FillUsers();
        IsBusy = true;

    }

    [RelayCommand]
    async Task DeleteUser()
    {
        try
        {
            IsBusy = false;
            await MyUserServices.DeleteUser(User.UserName);
            
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("User not Deleted!", ex.Message, "OK");
        }

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