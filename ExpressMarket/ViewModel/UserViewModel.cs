namespace ExpressMarket.ViewModel;

public partial class UserViewModel : BaseViewModel
{

   

    public ObservableCollection<User> ShownList { get; set; } = new();

	[RelayCommand]
	async void FillFromDB()
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
		catch(Exception ex) {
			await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
		}
		
		
		foreach(var item in MyUserList)
		{
		ShownList.Add(item);
		}
		IsBusy = false;
	
	}

}