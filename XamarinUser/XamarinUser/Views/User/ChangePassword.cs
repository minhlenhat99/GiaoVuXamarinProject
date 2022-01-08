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
            Entry entryOldPass = new Entry { Placeholder = "Mật khẩu hiện tại" };
            Entry entryNewPass = new Entry { Placeholder = "Mật khẩu mới" };
            Button btnSubmit = new Button { Text = "Submit" };
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
            MainContent.Children.Add(btnSubmit);
        }
        protected override void SetMainPage(object page)
        {
            Begin.PageContainter.PushAsync(this);
            base.SetMainPage(Begin.PageContainter);
        }
    }
}
