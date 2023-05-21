

namespace ExpressMarket;

public partial class AppShell : Shell
{



	public AppShell()
	{


		InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(DashBoardWindowsPage), typeof(DashBoardWindowsPage));
        Routing.RegisterRoute(nameof(DetailsFormWindowsPage), typeof(DetailsFormWindowsPage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(UpdateUserPage), typeof(UpdateUserPage));
        Routing.RegisterRoute(nameof(ScannerWindowsPage), typeof(ScannerWindowsPage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(CreateArticleFormPage), typeof(CreateArticleFormPage));
       
       
    }
}
