

namespace ExpressMarket.ViewModel;

public partial class LoginViewModel : ObservableObject
{
	public LoginViewModel()
	{
		
	}

    [RelayCommand]
    async Task GoToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}