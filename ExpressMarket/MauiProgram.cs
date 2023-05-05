﻿namespace ExpressMarket;

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
		builder.Services.AddSingleton<DashBoard>();
        builder.Services.AddTransient<DashBoardWindowsPage>();
        builder.Services.AddTransient<DetailsPage>();
        builder.Services.AddTransient<DetailsFormPage>();
        builder.Services.AddTransient<DetailsFormWindowsPage>();
        builder.Services.AddTransient<ScannerWindowsPage>();
        builder.Services.AddTransient<CreateArticleFormPage>();
        builder.Services.AddTransient<UserPage>();

        //ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<DashBoardViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<DetailsFormViewModel>();
        builder.Services.AddTransient<ScannerViewModel>();
        builder.Services.AddTransient<CreateArticleFormViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        //Services
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<ArticleService>();


        return builder.Build();
	}
}
