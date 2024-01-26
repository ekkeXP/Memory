using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Memory.Maui_App.Services
{
    //Class for sending toast messages
    public class ToastService : IToastService
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        //Function to send Toast messages
        public async void MakeToast(string text)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}