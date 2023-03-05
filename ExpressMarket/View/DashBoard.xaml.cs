namespace ExpressMarket.View;

public partial class DashBoard : ContentPage
{
	public DashBoard(DashBoardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
	}

   
}