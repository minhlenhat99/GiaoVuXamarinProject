using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinUser.Models;

namespace XamarinUser.Views.User
{
    class ContactInfoUpdate : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Contact Infomation";
            Padding = new Thickness(5, 10, 5, 0);
            Label phoneNumberLb = CreateLabel("Số điện thoại");
            Entry phoneNumber = CreateEntry("0912345678");
            Label emailLb = CreateLabel("Email");
            Entry email = CreateEntry("example_hust@gmail.com");
            Label permanentAddressLb = CreateLabel("Địa chỉ thường trú");
            Editor permanentAddress = CreateEditor("Số 1, Trâu Quỳ");
            Label currentAddressLb = CreateLabel("Địa chỉ hiện tại");
            Editor currentAddress = CreateEditor("Số 1, Trâu Quỳ");

            if (Model.Account.CInfo.PhoneNumber != null) phoneNumber.Text = Model.Account.CInfo.PhoneNumber;
            if (Model.Account.CInfo.Email != null) email.Text = Model.Account.CInfo.Email;
            if (Model.Account.CInfo.PermanentAddress != null) permanentAddress.Text = Model.Account.CInfo.PermanentAddress;
            if (Model.Account.CInfo.CurrentAddress != null) currentAddress.Text = Model.Account.CInfo.CurrentAddress;

            var info = new ContactInfo();
            Button btnSubmit = new Button { Text = "Submit", VerticalOptions = LayoutOptions.End };
            btnSubmit.Clicked += (s, e) =>
            {
                var contactInfo = new ContactInfo();
                if (phoneNumber.Text != null)       contactInfo.PhoneNumber = phoneNumber.Text;
                if (email.Text != null)             contactInfo.Email = email.Text;
                if (permanentAddress.Text != null)  contactInfo.PermanentAddress = permanentAddress.Text;
                if (currentAddress.Text != null)    contactInfo.CurrentAddress = currentAddress.Text;
                var message = new Dictionary<string, object>();
                message.Add("Token", Model.Token);
                message.Add("Info", contactInfo);
                Engine.Execute("User/Publish", "User/ContactInfoUpdate", message);
            };
            MainContent.Children.Add(phoneNumberLb);
            MainContent.Children.Add(phoneNumber);
            MainContent.Children.Add(emailLb);
            MainContent.Children.Add(email);
            MainContent.Children.Add(permanentAddressLb);
            MainContent.Children.Add(permanentAddress);
            MainContent.Children.Add(currentAddressLb);
            MainContent.Children.Add(currentAddress);
            MainContent.Children.Add(btnSubmit);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }
}
