namespace ExpressMarket.ViewModel;

public partial class ScannerViewModel : ContentPage
{
	public ScannerViewModel()
	{
		
	}

    [RelayCommand]
    async Task GoToCreateArticleFormPage()
    {
        await Shell.Current.GoToAsync(nameof(CreateArticleFormPage));
    }
}