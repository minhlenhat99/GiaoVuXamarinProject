using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
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

            Picker gender = new Picker
            {
                Title = "Select a gender",
                FontSize = fontSize,
            };
            gender.Items.Add("Nam");
            gender.Items.Add("Nữ");

            DatePicker birthday = new DatePicker
            {
                Date = new DateTime(1999, 1, 1)
            };

            Button submit = new Button { Text = "Submit" };
            submit.Clicked += (s, e) =>
            {

            };

            infoGrid.Children.Add(new Label { Text = "Họ tên", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black, FontFamily = "" }, 0, 0);
            infoGrid.Children.Add(new Label { Text = "Giới tính", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black }, 0, 1);
            infoGrid.Children.Add(new Label { Text = "Ngày sinh", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black }, 0, 2);
            infoGrid.Children.Add(new Label { Text = "Nơi sinh", FontSize = fontSize, VerticalOptions = LayoutOptions.Center, TextColor = Color.Black }, 0, 3);
            infoGrid.Children.Add(new Entry { Placeholder = "Nguyễn Văn A", FontSize = fontSize }, 1, 0);
            infoGrid.Children.Add(gender, 1, 1);
            infoGrid.Children.Add(birthday, 1, 2);
            infoGrid.Children.Add(new Entry { Placeholder = "Washington, D.C.", FontSize = fontSize }, 1, 3);

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
