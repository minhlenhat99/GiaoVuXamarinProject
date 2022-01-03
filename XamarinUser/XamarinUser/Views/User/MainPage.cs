using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;

namespace XamarinUser.Views.User
{
    class MainPage : FlyoutPage
    {
        static int? roleId;
        FlyoutMenuPage flyoutPage;
        public MainPage(int? role)
        {
            if (roleId == null) { roleId = role; }
            flyoutPage = new FlyoutMenuPage();
            Flyout = flyoutPage;

            flyoutPage.ListView.ItemSelected += OnItemSelected;

            // ???
            //if (Device.RuntimePlatform == Device.UWP)
            //{
            //    FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
            //}
            
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null)
            {
                Engine.Execute("User/" + item.Title, roleId);
                this.IsPresented = false;
                //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //flyoutPage.ListView.SelectedItem = null;
                //IsPresented = false;
            }
        }
    }
}
