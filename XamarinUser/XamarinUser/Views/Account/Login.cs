using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using System.Diagnostics;

namespace XamarinUser.Views.Account
{
    class Login : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Login";
            Entry entryID = new Entry { Placeholder = "Username" };
            MainContent.Children.Add(entryID);
            Entry entryPassword = new Entry { Placeholder = "Password" };
            entryPassword.IsPassword = true;
            MainContent.Children.Add(entryPassword);
            CheckBox cbxViewPasswd = new CheckBox()
            {
                IsChecked = false,
                Color = Color.Black
            };
            cbxViewPasswd.CheckedChanged += (s, e) =>
            {
                if (cbxViewPasswd.IsChecked) entryPassword.IsPassword = false;
                else entryPassword.IsPassword = true;
            };
            StackLayout viewPasswd = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                     cbxViewPasswd,
                     new Label {Text = "View Password", VerticalTextAlignment = TextAlignment.Center, TextColor = Color.Black}
                }
            };
            MainContent.Children.Add(viewPasswd);
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
                        Id = id,
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
            CheckBox cbxViewPasswd = new CheckBox()
            {
                IsChecked = false,
                Color = Color.Black
            };
            cbxViewPasswd.CheckedChanged += (s, e) =>
            {
                if (cbxViewPasswd.IsChecked) entryPassword.IsPassword = false;
                else entryPassword.IsPassword = true;
            };
            StackLayout viewPasswd = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                     cbxViewPasswd,
                     new Label {Text = "Show Password", VerticalTextAlignment = TextAlignment.Center, TextColor = Color.Black}
                }
            };
            MainContent.Children.Add(viewPasswd);
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
                        Id = id,
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
