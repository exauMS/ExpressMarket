namespace ExpressMarket.View;

public partial class UserPage : ContentPage	
{
	UserViewModel viewModel;
	public UserPage(UserViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.FillUsers();    // Réinitialise la observablecollection
        BindingContext = viewModel;
    }
}