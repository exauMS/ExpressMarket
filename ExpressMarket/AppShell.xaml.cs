

namespace ExpressMarket;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        if(DeviceInfo.Platform == DevicePlatform.Android)
            Routing.RegisterRoute(nameof(DashBoard), typeof(DashBoard));
        else
             Routing.RegisterRoute(nameof(DashBoardWindowsPage), typeof(DashBoardWindowsPage));

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(DetailsFormPage), typeof(DetailsFormPage));
    }
}
