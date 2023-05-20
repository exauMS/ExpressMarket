using Newtonsoft.Json;

namespace ExpressMarket.ViewModel;

public partial class CreateArticleFormViewModel : ObservableObject
{
    [ObservableProperty]
    ImageSource img;
    [ObservableProperty]
    Article product;
	public CreateArticleFormViewModel()
	{
        Product = new Article();
	}

    [RelayCommand]
	public async void AddImage()
	{
        PickOptions options= new();
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                    Img = image;
                    Product.ImageUrl = "name.png";
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Add image error ", ex.Message, "OK");
            // The user canceled or something went wrong
        }
    }

    [RelayCommand]
    async void Create()
    {
        if (string.IsNullOrWhiteSpace(Product.Creator) || string.IsNullOrWhiteSpace(Product.Type)
            || string.IsNullOrEmpty(Product.Creator) || string.IsNullOrEmpty(Product.Type)
            || string.IsNullOrWhiteSpace(Product.Name) || string.IsNullOrWhiteSpace(Product.Code)
            || string.IsNullOrEmpty(Product.Name) || string.IsNullOrEmpty(Product.Code)
            || string.IsNullOrWhiteSpace(Product.ImageUrl) || string.IsNullOrEmpty(Product.ImageUrl))
        {
            await Shell.Current.DisplayAlert("Create error", "Entry are empty", "OK");
        }
        else
        {

            GlobalsTools.articles.Add(Product);
            await Shell.Current.DisplayAlert("Successfully Created!", "You can go back.", "OK");

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "DATA.json");
            string jsonContent = File.ReadAllText(filePath);

            var articleJson = JsonConvert.DeserializeObject<List<Article>>(jsonContent);

            articleJson.Add(Product);

            string updatedJsonContent = JsonConvert.SerializeObject(articleJson);
            File.WriteAllText(filePath, updatedJsonContent);

        }
    }
}