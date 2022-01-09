using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    class ContactInfoUpdate : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Contact Infomation";
            Padding = new Thickness(5, 0, 5, 0);
            Entry phoneNumber = new Entry { Placeholder = "Số điện thoại" };
            Entry email = new Entry { Placeholder = "Email" };
            Entry permanentAddress = new Entry { Placeholder = "Địa chỉ thường trú" };
            Entry currentAddress = new Entry { Placeholder = "Địa chỉ hiện tại" };
            Button btnSubmit = new Button { Text = "Submit" };
            btnSubmit.Clicked += (s, e) => {
                Engine.Execute("User/Publish", "User/ChangePassword", new
                {
                    Token = Model.Token,
                });
            };
            MainContent.Children.Add(phoneNumber);
            MainContent.Children.Add(email);
            MainContent.Children.Add(btnSubmit);
        }
        protected override void SetMainPage(object page)
        {
            base.SetMainPage(page);
        }
    }
}
