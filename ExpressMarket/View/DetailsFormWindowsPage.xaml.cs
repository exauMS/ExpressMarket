namespace ExpressMarket.View;

public partial class DetailsFormWindowsPage : ContentPage
{
	public DetailsFormWindowsPage(DetailsFormViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}