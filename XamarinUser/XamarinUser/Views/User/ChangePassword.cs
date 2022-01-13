using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    class ChangePassword : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Change Password";
            Padding = new Thickness(5, 0, 5, 0);
            Entry entryOldPass = new Entry { Placeholder = "Mật khẩu hiện tại", IsPassword = true };
            Entry entryNewPass = new Entry { Placeholder = "Mật khẩu mới", IsPassword = true };
            CheckBox cbxViewPasswd = new CheckBox()
            {
                IsChecked = false,
                Color = Color.Black
            };
            cbxViewPasswd.CheckedChanged += (s, e) =>
            {
                if (cbxViewPasswd.IsChecked)
                {
                    entryOldPass.IsPassword = false;
                    entryNewPass.IsPassword = false;
                }
                else
                {
                    entryOldPass.IsPassword = true;
                    entryNewPass.IsPassword = true;
                }
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
            Button btnSubmit = new Button { Text = "Submit", VerticalOptions = LayoutOptions.End };
            btnSubmit.Clicked += (s, e) => {
                Engine.Execute("User/Publish", "User/ChangePassword", new
                {
                    Token = Model.Token,
                    OldPass = App.MD5Hash(entryOldPass.Text),
                    NewPass = App.MD5Hash(entryNewPass.Text)
                });
            };
            MainContent.Children.Add(entryOldPass);
            MainContent.Children.Add(entryNewPass);
            MainContent.Children.Add(viewPasswd);
            MainContent.Children.Add(btnSubmit);
        }
        protected override void SetMainPage(object page)
        {
            Begin.PageContainter.PushAsync(this);
            base.SetMainPage(Begin.PageContainter);
        }
    }
}
