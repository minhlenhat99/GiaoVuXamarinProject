using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    using MinhMVC;
    using Models;
    using System.Globalization;

    internal class PersonalInfoUpdate : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Personal Information";
            Padding = new Thickness(5, 10, 5, 0);
            // Name section
            var nameLb = CreateLabel("Họ tên");
            var nameEntry = CreateEntry("Nguyễn Văn A");
            // Gender section
            var genderLb = CreateLabel("Giới tính");
            Picker genderPicker = new Picker
            {
                Title = "Select a gender",
                FontSize = entryFontSize,
                Margin = entryMargin
            };
            genderPicker.Items.Add("Nam");
            genderPicker.Items.Add("Nữ");
            // Birthday section
            var birthdayLb = CreateLabel("Ngày sinh");
            DatePicker birthdayPicker = new DatePicker
            {
                Date = new DateTime(1999, 1, 1),
                FontSize = entryFontSize,
                Margin = entryMargin
            };
            // Birthplace section
            var birthplaceLb = CreateLabel("Nơi sinh");
            var birthplaceEntry = CreateEntry("Washington, D.C.");
            // Set Value
            if (Model.Account.PInfo.Name != null) nameEntry.Text = Model.Account.PInfo.Name;
            if (Model.Account.PInfo.Birthplace != null) birthplaceEntry.Text = Model.Account.PInfo.Birthplace;
            if (Model.Account.PInfo.Gender != null) genderPicker.SelectedItem = Model.Account.PInfo.Gender;
            if (Model.Account.PInfo.Birthday != null) birthdayPicker.Date = DateTime.Parse(Model.Account.PInfo.Birthday, CultureInfo.InvariantCulture);

            Button submit = new Button { Text = "Submit", VerticalOptions = LayoutOptions.End };
            submit.Clicked += (s, e) =>
            {
                var personalInfo = new PersonalInfo();
                if (nameEntry.Text != null) personalInfo.Name = nameEntry.Text;
                personalInfo.Birthday = birthdayPicker.Date.ToString();
                if (genderPicker.SelectedItem != null) personalInfo.Gender = genderPicker.SelectedItem.ToString();
                if (birthplaceEntry.Text != null) personalInfo.Birthplace = birthplaceEntry.Text;
                var message = new Dictionary<string, object>();
                //message.Add("Token", Model.Token);
                message.Add("Token", Model.Token);
                message.Add("Info", personalInfo);
                Engine.Execute("User/Publish", "User/PersonalInfoUpdate", message);
            };

            MainContent.Children.Add(nameLb);
            MainContent.Children.Add(nameEntry);
            MainContent.Children.Add(genderLb);
            MainContent.Children.Add(genderPicker);
            MainContent.Children.Add(birthdayLb);
            MainContent.Children.Add(birthdayPicker);
            MainContent.Children.Add(birthplaceLb);
            MainContent.Children.Add(birthplaceEntry);
            MainContent.Children.Add(submit);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
        
    }
}
