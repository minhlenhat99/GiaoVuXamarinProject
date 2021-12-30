using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using System.Diagnostics;

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
            Entry entryID = new Entry { Placeholder = "Username" };
            MainContent.Children.Add( entryID );
            Entry entryPassword = new Entry { Placeholder = "Password" };
            entryPassword.IsPassword = true;
            MainContent.Children.Add(entryPassword);
            Button btnLogin = new Button { Text = "Login" };
            btnLogin.Clicked += (s, e) =>
            {
                string id = entryID.Text;
                string pass = entryPassword.Text;
                if (id == "" || pass == "" || id == null || pass == null)
                {
                    DisplayAlert("Error", "Entry text is empty", "OK");
                }
                else
                {
                    Engine.Execute("Account/Publish", "Account/Login",
                    new Models.Account
                    {
                        StudentId = id,
                        Password = App.MD5Hash(pass)
                    });
                }
                
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
            entryPassword.IsPassword = true;
            MainContent.Children.Add(entryPassword);
            Button btnCreate = new Button { Text = "Create" };
            btnCreate.Clicked += (s, e) => {
                string id = entryID.Text;
                string pass = entryPassword.Text;
                if (id == "" || pass == "" || id == null || pass == null)
                {
                    DisplayAlert("Error", "Entry text is empty", "OK");
                }
                else
                {
                    Engine.Execute("Account/Publish", "Account/CreateAcc",
                    new Models.Account
                    {
                        StudentId = id,
                        Password = App.MD5Hash(pass)
                    });
                }
               
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
