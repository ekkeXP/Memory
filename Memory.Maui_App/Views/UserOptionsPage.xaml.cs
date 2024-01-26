using Microsoft.Maui.Controls;
using System;

namespace Memory.Maui_App.Views;

public partial class UserOptionsPage : ContentPage
{
    UserOptionsPageViewModel UOPVM;
    IMemoryScoreRepository DB;

    //Constructor
    public UserOptionsPage(UserOptionsPageViewModel viewModel, IMemoryScoreRepository db)
	{
		InitializeComponent();
        DB = db;
        this.BindingContext = UOPVM = viewModel;
        UOPVM.DB = DB;
        DBLoad();
	}

    //Task to load existing images in database
    private async Task DBLoad()
    {
        UOPVM.UploadedCardsAmount = 0;
        String[] images = UOPVM.DB.GetImages();
        foreach (String Url in images)
        {
            if (File.Exists(Url))
            {
                UOPVM.UploadedCardsAmount++;
            }
            else
            {
                UOPVM.DB.DeleteImg(Url);
            }
        }
    }

    //Event to catch a completed entry
    private async void OnEntryCompleted(object sender, EventArgs e)
    {
        // When the user presses Enter in the Entry field, simulate a button click.
        UOPVM.EnteredCardAmount = amount.Text;
        UOPVM.EnteredName = name.Text;
        await UOPVM.ValidateEntries();

    }


}
