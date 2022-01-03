using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    class Test2 : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            MainContent.Children.Add(new Label { Text = "Chi la test thoi" });
        }

        protected override void SetMainPage(object page)
        {
            _pageContainer = null;
            PageContainter.PushAsync(this);
            base.SetMainPage(PageContainter);
        }
    }
}
