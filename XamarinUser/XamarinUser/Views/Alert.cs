using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views
{
    class Alert : IView
    {
        public void Render(ControllerContext context)
        {
            App.Current.MainPage.DisplayAlert((string)context.Arguments[0], (string)context.Arguments[1], "OK");
        }
    }
}
