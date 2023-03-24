namespace ExpressMarket.View;

public partial class ScannerWindowsPage : ContentPage
{
    ScannerViewModel viewModel;

    public ScannerWindowsPage(ScannerViewModel viewModel)
	{
        this.viewModel = viewModel;
		InitializeComponent();
		BindingContext= viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.Product = null;
        viewModel.ImageUrl = "scanner.png";
        viewModel.Name = "Waiting For The Scan...";
        BindingContext = viewModel;
       


    }

    
}