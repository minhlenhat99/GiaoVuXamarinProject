using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using System.Diagnostics;

namespace XamarinUser.Views.Account
{
    class Login : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Login";
            Entry entryID = new Entry { Placeholder = "Username" };
            Entry entryPassword = new Entry { Placeholder = "Password" };
            entryPassword.IsPassword = true;
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
                        Username = id,
                        Password = App.MD5Hash(pass)
                    });
                }
                
            };
            Button btnCreateNewAcc = new Button { Text = "Create New Account" };
            btnCreateNewAcc.Clicked += (s, e) =>
            {
                Engine.Execute("Account/CreateAcc");
                //var testLayout = new 
                //DatePicker datePicker = new DatePicker
                //{
                //    Date = new DateTime(1999, 1, 1),
                //};
            };
            MainContent.Children.Add(entryID);
            MainContent.Children.Add(entryPassword);
            MainContent.Children.Add(viewPasswd);
            MainContent.Children.Add(btnLogin);
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
            Entry entryPassword = new Entry { Placeholder = "New Password" };
            entryPassword.IsPassword = true;
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
                        Username = id,
                        Password = App.MD5Hash(pass)
                    });
                }
               
            };
            MainContent.Children.Add(entryID);
            MainContent.Children.Add(entryPassword);
            MainContent.Children.Add(viewPasswd);
            MainContent.Children.Add(btnCreate);
        }
        protected override void SetMainPage(object page)
        {
            Login.PageContainter.PushAsync(this);
            base.SetMainPage(Login.PageContainter);
        }
    }
}
