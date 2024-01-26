using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Memory.Maui_App.Services;

namespace Memory.Maui_App.ViewModels
{
    //Viewmodel for the UserOptionsPage
    public partial class UserOptionsPageViewModel : PageViewModel
    {
        IToastService TS;
        //Constructor
        public UserOptionsPageViewModel(IToastService ts)
        {
            this.TS = ts;
        }

        //Checks for the entered information
        [RelayCommand]
        public async Task ValidateEntries()
        {
            int CardAmount;
            if (int.TryParse(EnteredCardAmount, out CardAmount))
            {
                if (CardAmount % 2 == 0 && CardAmount >= 10 && CardAmount <= 40)
                {
                    if (EnteredName.Length > 0)
                    {
                        if (UploadedCardsAmount << 1 >= int.Parse(EnteredCardAmount))
                        {
                            await GoToGamePage();
                        }
                        else
                        {
                            TS.MakeToast("You did not upload enough images");
                        }
                    }
                    else
                    {
                        TS.MakeToast("Enter your name");
                    }

                }
                else
                {
                    TS.MakeToast("Enter an even number between 10 and 40");
                }
            }
            else
            {
                TS.MakeToast("You didn't enter a number");
            }
        }

        //Go to GamePage Command
        [RelayCommand]
        public async Task GoToGamePage()
        {
            await Shell.Current.GoToAsync($"//{nameof(ConfirmPage)}");
        }

        //Go to ImageUploadPage Command
        [RelayCommand]
        public async Task GoToImageUpload()
        {
            await Shell.Current.GoToAsync($"//{nameof(ImageUploadPage)}");
        }

    }
}
