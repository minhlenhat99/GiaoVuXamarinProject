using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    class BaseView<MView> : ContentPage, IView
        where MView : View, new()
    {
        protected static int? RoleId { get; set; }

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
        // FlyoutPage
        protected static MainPage _mainPage;
        public static MainPage MainPage
        {
            get
            {
                if(_mainPage == null)
                {
                    _mainPage = new MainPage(RoleId);
                }
                return _mainPage;
            }
        }
        protected MView MainContent { get; set; }
        //public MModel Model { get; set; }
        public void Render(ControllerContext context)
        {
            if(RoleId == null)
            {
                RoleId = (int)context.Arguments[0];
            }
            //Model = (MModel)context.Model;
            MainContent = new MView();
            this.Content = MainContent;
            RenderCore();
            SetMainPage(this);
        }
        protected virtual void RenderCore()
        {

        }
        protected virtual void SetMainPage(object page)
        {
            //App.Current.MainPage = (Xamarin.Forms.Page)page;
            MainPage.Detail = (Page)page;
            App.Current.MainPage = MainPage;
        }
    }
}
