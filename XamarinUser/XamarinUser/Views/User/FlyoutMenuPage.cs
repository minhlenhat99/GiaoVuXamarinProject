using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    //Side menu View
    public class FlyoutMenuPage : ContentPage
    {
        // List view chua thong tin cac item cua side menu
        public ListView ListView { get; set; }
        public FlyoutMenuPage()
        {
            // itemSource cho List item
            var flyoutPageItems = new List<FlyoutPageItem>();
            flyoutPageItems.Add(new FlyoutPageItem{ Title = "Test" });
            flyoutPageItems.Add(new FlyoutPageItem{ Title = "Test2" });

            // Tao content cho ListView
            ListView = new ListView
            {
                ItemsSource = flyoutPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });

                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
            };

            Title = "Personal Organiser";
            Padding = new Thickness(0, 40, 0, 0);
            // Day la content cua Page 
            Content = new StackLayout
            {
                Children = { ListView }
            };
        }
    }
}
