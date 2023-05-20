namespace ExpressMarket.View;

public partial class UpdateUserPage : ContentPage
{
	public UpdateUserPage(UpdateUserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }
}