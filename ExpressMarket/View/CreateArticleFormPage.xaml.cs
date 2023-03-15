namespace ExpressMarket.View;

public partial class CreateArticleFormPage : ContentPage
{
	public CreateArticleFormPage(CreateArticleFormViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
	}

    
}