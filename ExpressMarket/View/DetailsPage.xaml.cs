namespace ExpressMarket.View;

public partial class DetailsPage : ContentPage
{
	DetailsViewModel viewModel;
	public DetailsPage(DetailsViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.RefreshList();    // Réinitialise la observablecollection
        BindingContext = viewModel;
    }
}