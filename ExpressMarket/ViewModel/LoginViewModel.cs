

using System.Data.OleDb;

namespace ExpressMarket.ViewModel;

public partial class LoginViewModel : BaseViewModel
{

	UserManagementServices MyUserServices = new();
    public ObservableCollection<User> ShownList { get; set; } = new();

    [ObservableProperty]
    string username;
    [ObservableProperty]
    string password;

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



        foreach (var item in GlobalsTools.UserListFromDB)
        {
            if (Username == item.UserName )
            {
               
                if (Password == item.UserPassword)
                {

                    try
                    {

                        await Shell.Current.GoToAsync(nameof(DashBoardWindowsPage), true);
                        isFind = true;
                    }
                    catch (Exception ex)
                    {

                        await Shell.Current.DisplayAlert("Erreur de connexion", ex.Message, "OK");
                        
                    }
              
                }
             
            }
            break;
        }

        if (isFind==false)
        { 
            await Shell.Current.DisplayAlert("Erreur de connexion","Votre nom d'utilisateur et/ou votre mot de passe est incorrecte","OK");
            Username = "";
            Password = "";
        }



    }




    [RelayCommand]
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



    [RelayCommand]
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
        GlobalsTools.UserListFromDB = ShownList;
        IsBusy = false;

    }



    [RelayCommand]
    async Task GoToRegisterPage()
	{
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
 
}