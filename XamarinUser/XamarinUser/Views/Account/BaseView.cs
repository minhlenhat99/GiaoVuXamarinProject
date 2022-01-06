using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.Account
{
    class BaseView<TView>: BaseView<object, TView> where TView: View, new()
    {

    }
    class BaseView<TModel, TView> : ContentPage, IView
        where TView : View, new()
    {
        protected static NavigationPage _pageContainer;
        public static NavigationPage PageContainter
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
        protected TModel Model { get; set; }
        public void Render(ControllerContext context)
        {
            MainContent = new TView();
            Model = (TModel) context.Model;
            this.Content = MainContent;
            RenderCore();
            SetMainPage(this);
        }
        protected virtual void RenderCore()
        {

        }
        protected virtual void SetMainPage(object page)
        {
            App.Current.MainPage = (Xamarin.Forms.Page)page;
        }
    }
}
