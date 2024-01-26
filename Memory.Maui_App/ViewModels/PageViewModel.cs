using Microsoft.Maui.Controls;

namespace Memory.Maui_App.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//Parent Viewmodel for all the global variables needed in most viewmodels
public partial class PageViewModel : BaseViewModel
{
    [ObservableProperty]
    public static string enteredName = "";

    [ObservableProperty]
    public static string enteredCardAmount = "";

    [ObservableProperty]
    public static int uploadedCardsAmount = 0;

    [ObservableProperty]
    public static string resultText = "";

    [ObservableProperty]
    public static List<string> highscores = new List<string>();

    public IMemoryScoreRepository DB;
}
