using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    using MinhMVC;
    using Models;
    internal class PersonalInfoUpdate : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            double fontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry));
            this.Title = "Personal Information";
            Padding = new Thickness(5, 0, 5, 0);
            Grid infoGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) }
                }
            };
            // Name section
            var nameLb = new Label { Text = "Họ tên", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black, FontFamily = "" };
            var nameEntry = new Entry { Placeholder = "Nguyễn Văn A", FontSize = fontSize };
            // Gender section
            var genderLb = new Label { Text = "Giới tính", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black };
            Picker genderPicker = new Picker
            {
                Title = "Select a gender",
                FontSize = fontSize,
            };
            genderPicker.Items.Add("Nam");
            genderPicker.Items.Add("Nữ");
            // Birthday section
            var birthdayLb = new Label { Text = "Ngày sinh", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black };
            DatePicker birthdayPicker = new DatePicker
            {
                Date = new DateTime(1999, 1, 1)
            };
            // Birthplace section
            var birthplaceLb = new Label { Text = "Nơi sinh", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black };
            var birthplaceEntry = new Entry { Placeholder = "Washington, D.C.", FontSize = fontSize };

            // Set Value
            nameEntry.Text = Model.Account.PInfo.Name;
            birthplaceEntry.Text = Model.Account.PInfo.Birthplace;
            genderPicker.SelectedItem = Model.Account.PInfo.Gender;
            birthdayPicker.Date = DateTime.Parse(Model.Account.PInfo.Birthday);

            Button submit = new Button { Text = "Submit" };
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

            infoGrid.Children.Add(nameLb, 0, 0);
            infoGrid.Children.Add(genderLb, 0, 1);
            infoGrid.Children.Add(birthdayLb, 0, 2);
            infoGrid.Children.Add(birthplaceLb, 0, 3);
            infoGrid.Children.Add(nameEntry, 1, 0);
            infoGrid.Children.Add(genderPicker, 1, 1);
            infoGrid.Children.Add(birthdayPicker, 1, 2);
            infoGrid.Children.Add(birthplaceEntry, 1, 3);

            MainContent.Children.Add(infoGrid);
            MainContent.Children.Add(submit);
        }
        protected override void SetMainPage(object page)
        {
            Begin.PageContainter.PushAsync(this);
            base.SetMainPage(Begin.PageContainter);
        }
    }
}
