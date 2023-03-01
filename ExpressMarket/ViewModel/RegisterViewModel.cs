using Android.Widget;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace ExpressMarket.View.StartUp;

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
        user.UserName = UserName;
        user.Email = Email;
        user.Password = Password;
        await UserService.AddUserAsync(user);

        //login page redirection
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}