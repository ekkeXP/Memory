using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Maui_App.ViewModels
{
    //Viewmodel for the HighscoresPage
    public partial class HighscoresPageViewModel : PageViewModel
    {
        //Go to UserOptionsPage Command
        [RelayCommand]
        public async Task Home()
        {
            await Shell.Current.GoToAsync($"//{nameof(UserOptionsPage)}");
        }

        //Go to ConfirmPage Command
        [RelayCommand]
        public async Task Again()
        {
            await Shell.Current.GoToAsync($"//{nameof(ConfirmPage)}");
        }
    }
}
