using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.Account
{
    //class Login : BaseView<List<Models.Account>, StackLayout>
    class Login : BaseView<StackLayout>
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
            this.Title = "Login";
            Entry entryID = new Entry { Placeholder = "Student ID" };
            MainContent.Children.Add( entryID );
            Entry entryPassword = new Entry { Placeholder = "Password" };
            MainContent.Children.Add(entryPassword);
            Button btnLogin = new Button { Text = "Login" };
            btnLogin.Clicked += (s, e) =>
            {
                Engine.Execute("Account/Publish", "Account/Login",
                new Models.Account
                {
                    StudentId = entryID.Text,
                    Password = entryPassword.Text
                });
            };
            MainContent.Children.Add(btnLogin);
            Button btnCreateNewAcc = new Button { Text = "Create New Account" };
            btnCreateNewAcc.Clicked += (s, e) =>
            {
                Engine.Execute("Account/CreateAcc");
            };
            MainContent.Children.Add(btnCreateNewAcc);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }

    //class CreateAcc : BaseView<List<Models.Account>, StackLayout>
    class CreateAcc : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Create New Account";
            Entry entryID = new Entry { Placeholder = "Student ID" };
            MainContent.Children.Add(entryID);
            Entry entryPassword = new Entry { Placeholder = "New Password" };
            MainContent.Children.Add(entryPassword);
            Button btnCreate = new Button { Text = "Create" };
            btnCreate.Clicked += (s, e) => {
                Engine.Execute("Account/Pubish", "Account/CreateAcc",
                    new Models.Account
                    {
                        StudentId = entryID.Text,
                        Password = entryPassword.Text
                    });
            };
            MainContent.Children.Add(btnCreate);
        }

        protected override void SetMainPage(object page)
        {
            Login.PageContainter.PushAsync(this);
            base.SetMainPage(Login.PageContainter);
        }
    }
}
