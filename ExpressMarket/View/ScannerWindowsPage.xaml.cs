namespace ExpressMarket.View;

public partial class ScannerWindowsPage : ContentPage
{
	public ScannerWindowsPage(ScannerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}