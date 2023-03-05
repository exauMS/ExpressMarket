namespace ExpressMarket;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<DashBoard>();
        builder.Services.AddTransient<DetailsPage>();
        //ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<DashBoardViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        //Services
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<ArticleService>();


        return builder.Build();
	}
}
