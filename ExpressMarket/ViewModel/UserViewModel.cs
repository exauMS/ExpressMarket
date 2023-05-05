namespace ExpressMarket.ViewModel;

public partial class UserViewModel : BaseViewModel
{

   

    public ObservableCollection<User> ShownList { get; set; } = new();

	[RelayCommand]


 
	async void FillFromDB()
	{
		IsBusy = true;
		List<User> MyUserList = new();

        GlobalsTools.UserSet.Clear();

        MyUserList.Clear();

        DataRow AccessA = GlobalsTools.UserSet.Tables["Access"].NewRow();
        AccessA[0] = 1;
        AccessA[1] = "Azedine";
        AccessA[2] = true;
        AccessA[3] = false;
        AccessA[4] = false;
        AccessA[5] = false;


        DataRow AccessB = GlobalsTools.UserSet.Tables["Access"].NewRow();
        AccessB[0] = 2;
        AccessA[1] = "Abd";
        AccessA[2] = true;
        AccessA[3] = true;
        AccessA[4] = true;
        AccessA[5] = true;

        DataRow AccessC = GlobalsTools.UserSet.Tables["Access"].NewRow();
        AccessC[0] = 3;
        AccessC[1] = "Ilias";
        AccessC[2] = true;
        AccessC[3] = true;
        AccessC[4] = true;
        AccessC[5] = true;

        GlobalsTools.UserSet.Tables["Access"].Rows.Add(AccessA);
        GlobalsTools.UserSet.Tables["Access"].Rows.Add(AccessB);
        GlobalsTools.UserSet.Tables["Access"].Rows.Add(AccessC);


        DataRow UserA = GlobalsTools.UserSet.Tables["Users"].NewRow();
        UserA[0] = 1;
        UserA[1] = "Azedine";
        UserA[2] = "aaaa";
        UserA[3] = 1;

        DataRow UserB = GlobalsTools.UserSet.Tables["Users"].NewRow();
        UserB[0] = 2;
        UserB[1] = "Salma";
        UserB[2] = "ssss";
        UserB[3] = 3;

        GlobalsTools.UserSet.Tables["Users"].Rows.Add(UserA);
        GlobalsTools.UserSet.Tables["Users"].Rows.Add(UserB);

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