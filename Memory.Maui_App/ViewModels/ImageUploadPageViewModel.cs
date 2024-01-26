using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Maui_App.ViewModels
{
    //Viewmodel for the ImageUploadPage
    public partial class ImageUploadPageViewModel : PageViewModel
    {
        //Go to the useroptionsPage Command
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(UserOptionsPage)}");
        }
    }
}
