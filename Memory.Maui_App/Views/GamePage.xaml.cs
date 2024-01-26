using Microsoft.Maui.Controls;
using System;

namespace Memory.Maui_App.Views;

public partial class GamePage : ContentPage
{
	GamePageViewModel GPVM;
    //Constructor
	public GamePage(GamePageViewModel viewModel, IMemoryScoreRepository db)
	{
		InitializeComponent();
		this.BindingContext = GPVM = viewModel;
        GPVM.DB = db;
    }

    //Function to only update the Cards
    private void drawCardsOnly()
    {
        foreach(Grid G in CardList)
        {
            foreach(object o in G)
            {
                if(o.GetType() == typeof(Image))
                {
                    Image I = (Image)o;
                    I.Source = GPVM.currentGame.cards[int.Parse(I.StyleId)].GetCardFace();
                }
                
            }
        }
    }

    //Function to initialize the Grids, Images and Buttons
    private async void drawCards()
    {
        if (GPVM.FirstRun)
        {
            GPVM.FirstRun = false;
            CardList.Clear();
            for (int i = 0; i < int.Parse(GPVM.EnteredCardAmount); i++)
            {
                Grid G = new Grid
                {
                    WidthRequest = 60,
                    HeightRequest = 88
                };
                Image I = new Image
                {
                    StyleId = $"{i}",
                    Source = GPVM.currentGame.cards[i].GetCardFace(),
                    Margin = new Thickness(3, 0, 0, 0),
                    Aspect = Aspect.AspectFit,
                };
                Button B = new Button
                {
                    Margin = new Thickness(3, 0, 0, 0),
                    BackgroundColor = Colors.Transparent,
                    BorderColor = Colors.Transparent,
                    WidthRequest = 60,
                    HeightRequest = 88
                };
                B.Command = new Command(() =>
                {
                    ButtonFunctions(I);
                });
                G.Add(I);
                G.Add(B);
                CardList.Add(G);
            }
        }
        else
        {
            drawCardsOnly();
        }
        InvalidateMeasure();
        await GPVM.CheckFinished();
    }


    private void ButtonFunctions(Image I)
    {
        GPVM.ImageClicked(I);
        drawCards();
        //Task.Delay(1000);
        GPVM.CheckMatched();
        drawCards();
    }

    //OnAppearing method
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        GPVM.init();
        drawCards();
    }
}
