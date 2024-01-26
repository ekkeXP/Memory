using Microsoft.Maui.Graphics.Text;
using System.Runtime.ConstrainedExecution;

namespace Memory.Maui_App.Views;

public partial class HighscoresPage : ContentPage
{
	HighscoresPageViewModel HsPVM;
    //Constructor
	public HighscoresPage(HighscoresPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = HsPVM = viewModel;
	}

    //Function for updating Highscores
    private void UpdateHighscores()
    {
        if(HighscoresStack.Count >= 1) HighscoresStack.Clear();

        foreach (string s in HsPVM.Highscores)
        {
            Label L = new Label()
            {
                Text = s,
                Margin = new Thickness(5, 0),
                TextColor = Colors.Black
            };
            HighscoresStack.Add(L);
        }
    }

    //OnAppearing method
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateHighscores();
    }
}
