namespace ExpressMarket.View;

public partial class DashBoardWindowsPage : ContentPage
{
	public DashBoardWindowsPage(DashBoardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}