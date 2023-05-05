namespace ExpressMarket.ViewModel;

public partial class MainViewModel : ObservableObject
{
	public MainViewModel()
	{
        UserDataTables MyUserTables = new();
    }

    [RelayCommand]
    async Task GoToDashBoardPage()
    {
        if(DeviceInfo.Platform == DevicePlatform.Android)
            await Shell.Current.GoToAsync(nameof(DashBoard));
        else if(DeviceInfo.Platform == DevicePlatform.WinUI)
            await Shell.Current.GoToAsync(nameof(DashBoardWindowsPage));
        else
            await Shell.Current.GoToAsync(nameof(DashBoardWindowsPage));

    }
}