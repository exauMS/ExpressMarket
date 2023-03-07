namespace ExpressMarket.View;

public partial class DetailsFormPage : ContentPage
{
	public DetailsFormPage(DetailsFormViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}