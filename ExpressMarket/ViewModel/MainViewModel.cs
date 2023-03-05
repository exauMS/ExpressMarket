namespace ExpressMarket.ViewModel;

public partial class MainViewModel : ObservableObject
{
	public MainViewModel()
	{
		
	}

    [RelayCommand]
    async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}