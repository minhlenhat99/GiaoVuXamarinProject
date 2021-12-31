using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.User
{
    class Test : BaseView<StackLayout>
    {
        static NavigationPage _pageContainer;
        public static NavigationPage PageContainter
        {
            get
            {
                if (_pageContainer == null)
                {
                    _pageContainer = new NavigationPage();
                }
                return _pageContainer;
            }
        }
        protected override void RenderCore()
        {
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
