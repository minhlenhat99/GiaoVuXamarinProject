using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.User
{
    class Test : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            string sayHello = "Xin chào ";
            switch (RoleId)
            {
                case 0:
                    sayHello += "quản trị viên";
                    break;
                case 1:
                    sayHello += "sinh viên";
                    break;
            }
            Label SayHello = new Label()
            {
                Text = sayHello,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            MainContent.Children.Add(SayHello);
            Button btnLogout = new Button()
            {
                Text = "Log out"
            };
            btnLogout.Clicked += (s, e) => Engine.Execute("User/Logout");
            MainContent.Children.Add(btnLogout);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }


}
