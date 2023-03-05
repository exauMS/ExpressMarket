
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace ExpressMarket.View;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    string userName;
    [ObservableProperty]
    string email;
    [ObservableProperty]
    string password;

    [RelayCommand]
    async Task AddUser()
    {
        User user = new User();
        user.UserName = userName;
        user.Email = email;
        user.Password = password;
        await UserService.AddUserAsync(user);

        //login page redirection
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    [RelayCommand]
    async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}