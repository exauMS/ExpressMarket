namespace ExpressMarket.ViewModel;

public partial class ScannerViewModel : ObservableObject
{
    DeviceOrientationServices MyDeviceOrientationService;
    [ObservableProperty]
    string codeFromScan;

    [ObservableProperty]
    Article product;

    [ObservableProperty]
    string imageUrl="scanner.png",name="Waiting For The Scan...";
    

    public  ScannerViewModel()
	{
        
        MyDeviceOrientationService = new DeviceOrientationServices();
        MyDeviceOrientationService.ConfigureScanner();
        MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_Changed;
       
    }

    [RelayCommand]
    async Task GoToCreateArticleFormPage()
    {
       await Shell.Current.GoToAsync(nameof(CreateArticleFormPage));
    }

    private  void SerialBuffer_Changed(object sender, EventArgs e)
    {
        QueueBuffer queue = new QueueBuffer();
      
        CodeFromScan = queue.Dequeue().ToString();

        foreach(var data in GlobalsTools.articles)
        {
            if(data.Code == CodeFromScan)
            {
                Product= data;
                ImageUrl = Product.ImageUrl;
                Name = Product.Name;
            }
        }

        if(Name== "Waiting For The Scan...")
        {
            Name = "Article With Code " + CodeFromScan + " Not Found... Create One At The Top Right";
        }
        
    }
}