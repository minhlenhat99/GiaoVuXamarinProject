using MinhMVC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.Giaovu
{
    internal class AllowRegisterAgain : User.BaseView<Models.User, StackLayout>
    {
        static ObservableCollection<string> data;
        protected override void RenderCore()
        {
            int cmp1 = DateTime.Compare(DateTime.Now, Model.Account.Duration.Start);
            int cmp2 = DateTime.Compare(DateTime.Now, Model.Account.Duration.End);
            if (cmp1 < 0 || cmp2 > 0)
            {
                MainContent.Children.Add(NotRegisterDurationLabel());
            }
            else
            {
                if (data == null)
                {
                    data = new ObservableCollection<string>();
                    foreach (var a in Model.ListAccount)
                    {
                        string s = $"{a.Username} {a.PInfo.Name}";
                        data.Add(s);
                    }
                }

                Padding = new Thickness(5, 10, 10, 0);
                Title = "Allow Register Again";
                var mssvLb = CreateLabel("Mã số sinh viên");
                var searchEntry = CreateEntry("Nhập MSSV");
                var suggestionList = new ListView(ListViewCachingStrategy.RecycleElement)
                {
                    IsVisible = false,
                };
                var allowBtn = new Button { Text = "Cho phép đăng ký lại", IsEnabled = false };
                searchEntry.TextChanged += (s, e) =>
                {
                    suggestionList.IsVisible = true;
                    suggestionList.BeginRefresh();

                    var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
                    if (string.IsNullOrWhiteSpace(e.NewTextValue))
                        suggestionList.IsVisible = false;
                    else
                        suggestionList.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                    suggestionList.EndRefresh();
                };
                suggestionList.ItemTapped += (s, e) =>
                {
                    String item = e.Item as string;
                    searchEntry.Text = item;
                    suggestionList.IsVisible = false;
                    ((ListView)s).SelectedItem = null;

                    allowBtn.IsEnabled = true;
                };
                suggestionList.ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label { FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    label.SetBinding(Label.TextProperty, ".");
                    return new ViewCell
                    {
                        View = label,
                    };
                });
                allowBtn.Clicked += (s, e) =>
                {
                    string[] words = searchEntry.Text.Split(' ');
                    Engine.Execute("Giaovu/Publish", "Giaovu/AllowRegisterAgain", words[0]);
                    allowBtn.IsEnabled = false;
                };

                //suggestionList.item
                MainContent.Children.Add(mssvLb);
                MainContent.Children.Add(searchEntry);
                MainContent.Children.Add(suggestionList);
                MainContent.Children.Add(allowBtn);
            }
            
        }
        protected override void SetMainPage(object page)
        {
            ExtendClassMainPage.PageContainter.PushAsync(this);
            base.SetMainPage(ExtendClassMainPage.PageContainter);
        }
    }
}
