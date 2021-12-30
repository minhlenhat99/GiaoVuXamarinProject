using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;
using Xamarin.Forms;

namespace XamarinUser.Views.Account
{
    class AlertError : IView
    {
        public async void Render(ControllerContext context)
        {
            await App.Current.MainPage.DisplayAlert("Error", (string) context.Arguments[0], "OK");
        }
    }
}
