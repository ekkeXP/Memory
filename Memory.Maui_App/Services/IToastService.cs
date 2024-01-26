using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Maui_App.Services
{
    //Interface for sending toast messages
    public interface IToastService
    {
        public void MakeToast(string text);
    }
}
