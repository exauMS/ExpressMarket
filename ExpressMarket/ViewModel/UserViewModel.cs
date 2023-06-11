namespace ExpressMarket.ViewModel;

public partial class UserViewModel : BaseViewModel
{

    UserManagementServices MyDBServices = new();
   


    public ObservableCollection<User> ShownList { get; set; } = new();

    public UserViewModel(UserManagementServices MyDBServices)
	{
		this.MyDBServices = MyDBServices;

		MyDBServices.ConfigTools();
    }

    [RelayCommand]
    async Task GoToUpdateUserPage(User user)
    {
        if (user is null)
            return;

        await Shell.Current.GoToAsync(nameof(UpdateUserPage), true, new Dictionary<string, object>
        {
            {"User", user }

        });
    }

    [RelayCommand]
	public async void FillUsers()
	{
        GlobalsTools.UserSet.Tables["Users"].Clear();
        GlobalsTools.UserSet.Tables["Access"].Clear();

        try
        {
            await MyDBServices.ReadAccessTable();
            await MyDBServices.ReadUserTable();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }

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
		catch(Exception ex) {
			await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
		}
        ShownList.Clear();


        foreach (var item in MyUserList)
		{
		    ShownList.Add(item);
		}
        GlobalsTools.UserListFromDB = ShownList;
        IsBusy = false;
	
	}

}