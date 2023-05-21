global using ExpressMarket.ViewModel;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;
global using ExpressMarket.View;
global using System.Collections.ObjectModel;
global using ExpressMarket.Model;
global using ExpressMarket.Services;
global using System.Data;
global using System.IO;

internal class GlobalsTools
{
    public static List<Article> articles = new();
    public static DataSet UserSet = new();
    public static ObservableCollection<User> UserListFromDB { get; set; } = new();
    public static User loggedUser;
    public static Boolean isLogged=false;
}