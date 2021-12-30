using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinUser.Views.Account
{
    class Alert : IView
    {
        public void Render(ControllerContext context)
        {
            App.Current.MainPage.DisplayAlert((string)context.Arguments[0], (string)context.Arguments[1], "OK");
        }
    }
}
