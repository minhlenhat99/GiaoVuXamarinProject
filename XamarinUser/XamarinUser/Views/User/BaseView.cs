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
        protected static NavigationPage _pageContainer;
        protected static NavigationPage PageContainter
        {
            get
            {
                if (_pageContainer == null)
                {
                    _pageContainer = new NavigationPage();
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
    }
}
