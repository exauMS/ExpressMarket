namespace ExpressMarket.ViewModel;

public partial class MainViewModel : ObservableObject
{
	public MainViewModel()
	{
        CreateUserDataTables MyUserTables = new();
    }

    [RelayCommand]
    async Task GoToDashBoardPage()
    {
            await Shell.Current.GoToAsync(nameof(DashBoardWindowsPage));
    }

    [RelayCommand]
    async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}