using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.Account
{
    class BaseView<MView> : ContentPage, IView
        where MView : View, new()
    {
        protected MView MainContent { get; set; }
        //public MModel Model { get; set; }
        public void Render(ControllerContext context)
        {
            //Model = (MModel)context.Model;
            // Main Thread
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
            App.Current.MainPage = (Xamarin.Forms.Page)page;
        }
    }
}
