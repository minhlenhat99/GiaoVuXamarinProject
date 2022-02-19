using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using System.Collections.ObjectModel;

namespace XamarinUser.Views.User
{
    internal class Begin : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            Padding = new Thickness(5, 0, 10, 0);
            Title = "Home";
            
            var groups = new ObservableCollection<Grouping<string, Models.DetailPageItem>>();

            // Chuc nang dang ky nhan giay to
            var paperOptions = new List<Models.DetailPageItem>();
            paperOptions.Add(new Models.DetailPageItem { Title = "Chứng nhận sinh viên" });
            paperOptions.Add(new Models.DetailPageItem { Title = "Giấy giới thiệu" });
            paperOptions.Add(new Models.DetailPageItem { Title = "Bảng điểm" });
            groups.Add(new Grouping<string, Models.DetailPageItem>("Cấp giấy tờ", paperOptions));
            // Chuc nang dang ky lop
            var classOptions = new List<Models.DetailPageItem>();
            var extendClassOption = new Models.DetailPageItem { Title = "Đăng ký mở rộng lớp", Name = "ExtendClassMainPage" };
            classOptions.Add(extendClassOption);
            groups.Add(new Grouping<string, Models.DetailPageItem>("Mở rộng lớp", classOptions));

            var optionsView = new ListView();
            optionsView.IsGroupingEnabled = true;
            optionsView.ItemsSource = groups;
            optionsView.GroupHeaderTemplate = new DataTemplate(() =>
            {
                Padding = new Thickness(15, 10, 5, 15);
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                };
                label.SetBinding(Label.TextProperty, "GroupKey");
                return new ViewCell
                {
                    View = label
                };
            });
            optionsView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
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
                    if (Model.Account.Role.Id == 0) Engine.Execute("User/UpdateUserAccount", "Giaovu/" + item.Name);
                    if (Model.Account.Role.Id == 1) Engine.Execute("User/UpdateUserAccount", "User/" + item.Name);
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
