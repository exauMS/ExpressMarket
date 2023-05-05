

namespace ExpressMarket;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));

        if(DeviceInfo.Platform == DevicePlatform.Android)
        {
            Routing.RegisterRoute(nameof(DashBoard), typeof(DashBoard));
            Routing.RegisterRoute(nameof(DetailsFormPage), typeof(DetailsFormPage));

        }
        else
        {
            Routing.RegisterRoute(nameof(DashBoardWindowsPage), typeof(DashBoardWindowsPage));
            Routing.RegisterRoute(nameof(DetailsFormWindowsPage), typeof(DetailsFormWindowsPage));
           
        }
        Routing.RegisterRoute(nameof(ScannerWindowsPage), typeof(ScannerWindowsPage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(CreateArticleFormPage), typeof(CreateArticleFormPage));
       
       
    }
}
