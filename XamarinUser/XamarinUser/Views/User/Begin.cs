using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using System.Collections.ObjectModel;

namespace XamarinUser.Views.User
{
    internal class Begin : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            Padding = new Thickness(5, 10, 10, 0);

            var groups = new ObservableCollection<Grouping<string, Models.DetailPageItem>>();

            var paperOptions = new List<Models.DetailPageItem>();
            paperOptions.Add(new Models.DetailPageItem { Title = "Chứng nhận sinh viên" });
            paperOptions.Add(new Models.DetailPageItem { Title = "Giấy giới thiệu" });
            paperOptions.Add(new Models.DetailPageItem { Title = "Bảng điểm" });
            groups.Add(new Grouping<string, Models.DetailPageItem>("Cấp giấy tờ", paperOptions));
            var classOptions = new List<Models.DetailPageItem>();
            classOptions.Add(new Models.DetailPageItem { Title = "Đăng ký mở rộng lớp", Name = "ExtendClassRegister" });
            groups.Add(new Grouping<string, Models.DetailPageItem>("Mở rộng lớp", classOptions));

            var optionsView = new ListView();
            optionsView.IsGroupingEnabled = true;
            optionsView.GroupHeaderTemplate = new DataTemplate(() =>
            {
                Padding = new Thickness(15, 0, 0, 15);
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    FontSize = 20,
                    TextColor = Color.Black,
                    VerticalTextAlignment = TextAlignment.Center,
                };
                label.SetBinding(Label.TextProperty, "GroupKey");
                return new ViewCell
                {
                    View = label
                };
            });
            optionsView.ItemsSource = groups;
            optionsView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    FontSize = 15,
                    TextColor = Color.DarkBlue,
                    VerticalTextAlignment = TextAlignment.Center,
                };
                label.SetBinding(Label.TextProperty, "Title");
                return new ViewCell
                {
                    View = label
                };
            });

            optionsView.ItemSelected += (s, e) =>
            {
                var item = e.SelectedItem as Models.DetailPageItem;
                if (item != null)
                {
                    Engine.Execute("User/" + item.Name);
                    optionsView.SelectedItem = null;
                }
            };
            MainContent.Children.Add(optionsView);
        }
        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }
    

}
