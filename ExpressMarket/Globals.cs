global using ExpressMarket.ViewModel;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;
global using ExpressMarket.View;
global using System.Collections.ObjectModel;
global using ExpressMarket.Model;
global using ExpressMarket.Services;
global using System.Data;
internal class GlobalsTools
{
    public static List<Article> articles = new();
    public static DataSet UserSet = new();
}