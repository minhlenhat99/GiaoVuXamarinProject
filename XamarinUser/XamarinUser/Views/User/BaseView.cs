using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    public class BaseView<TView> : BaseView<object, TView> where TView : View, new() { }
    public class BaseView<TModel, TView> : ContentPage, IView where TView : View, new()
    {
        protected static double entryFontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry));
        protected static double labelFontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
        protected static Thickness entryMargin = new Thickness(0, 0, 0, 15);
        protected static NavigationPage _pageContainer;
        protected static NavigationPage PageContainter
        {
            get
            {
                if (_pageContainer == null)
                {
                    _pageContainer = new NavigationPage();
                    var logout = new ToolbarItem() { Text = "Logout" };
                    logout.Clicked += (s, e) => { Engine.Execute("User/Logout"); };
                    var home = new ToolbarItem() { Text = "Home" };
                    home.Clicked += (s, e) => { Engine.Execute("User/Begin"); };
                    _pageContainer.ToolbarItems.Add(home);
                    _pageContainer.ToolbarItems.Add(logout);
                }
                return _pageContainer;
            }
        }
        protected TView MainContent { get; set; }
        public TModel Model { get; set; }
        public void Render(ControllerContext context)
        {
            Model = (TModel)context.Model;
            MainContent = new TView();
            this.Content = MainContent;
            RenderCore();
            SetMainPage(this);
        }
        protected virtual void RenderCore()
        {

        }
        protected virtual void SetMainPage(object page)
        {
            MainPage.mainPage.Detail = (Page)page;
            App.Current.MainPage = MainPage.mainPage;
        }
        protected Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                FontSize = labelFontSize,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.LightBlue,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                Padding = new Thickness(5, 2, 5, 2)
            };
        }
        protected Entry CreateEntry(string text)
        {
            return new Entry
            {
                Placeholder = text,
                FontSize = entryFontSize,
                Margin = entryMargin
            };
        }
        protected Editor CreateEditor(string text)
        {
            return new Editor
            {
                Placeholder = text,
                FontSize = entryFontSize,
                Margin = entryMargin,
                AutoSize = EditorAutoSizeOption.TextChanges
            };
        }
        protected Label NotRegisterDurationLabel()
        {
            var remind = new Label
            {
                Text = "Chưa đến thời hạn đăng ký",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };
            return remind;
        }
    }
}
