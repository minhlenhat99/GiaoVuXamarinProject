using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinUser.Models;
namespace XamarinUser.Views.Giaovu
{
    internal class ExtendClassMainPage : User.BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            Padding = new Thickness(5, 10, 10, 0);
            Title = "Giaovu Register Main";
            // item source
            var options = new List<DetailPageItem>();
            options.Add(new DetailPageItem { Title = "Cài đặt thời hạn đăng ký", Name = "SetRegisterDuration" });
            options.Add(new DetailPageItem { Title = "Danh sách đăng ký", Name = "ExtendClassMainPage" });
            options.Add(new DetailPageItem { Title = "Cho phép đăng ký lại", Name = "AllowRegisterAgain" });
            // list view
            ListView listView = new ListView
            {
                ItemsSource = options,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label label = new Label
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                        Padding = new Thickness(15, 0, 0, 0)
                    };
                    label.SetBinding(Label.TextProperty, "Title");
                    return new ViewCell
                    {
                        View = label
                    };
                }),
            };
            listView.ItemSelected += (s, e) =>
            {
                var item = e.SelectedItem as Models.DetailPageItem;
                if (item != null)
                {
                    if (item.Title == "Danh sách đăng ký") Engine.Execute("User/UpdateUserAccount", "User/" + item.Name);
                    else Engine.Execute("User/UpdateUserAccount", "Giaovu/" + item.Name);
                    listView.SelectedItem = null;
                }
            };

            MainContent.Children.Add(listView);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }
}
