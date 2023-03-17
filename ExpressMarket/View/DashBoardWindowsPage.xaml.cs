namespace ExpressMarket.View;

public partial class DashBoardWindowsPage : ContentPage
{
    DashBoardViewModel viewModel;

    public DashBoardWindowsPage(DashBoardViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext= viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.RefreshList();    // Réinitialise la observablecollection
        BindingContext = viewModel;
    }
}