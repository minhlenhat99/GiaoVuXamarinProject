using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MinhMVC;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace XamarinUser.Views.User
{
    class MainPage : FlyoutPage
    {
        internal static MainPage mainPage = new MainPage();
        FlyoutMenuPage flyoutPage;
        public MainPage()
        {
            flyoutPage = new FlyoutMenuPage();
            this.Flyout = flyoutPage;
            flyoutPage.ListView.ItemSelected += (s, e) =>
            {
                var item = e.SelectedItem as Models.FlyoutPageItem;
                if (item != null)
                {
                    Engine.Execute("User/" + item.Name);
                    flyoutPage.ListView.SelectedItem = null;
                    this.IsPresented = false;
                }
            };
        }
        
    }
    class Grouping<K, T> : ObservableCollection<T>
    {
        //This is the GroupDisplayBinding above for displaying the header
        public K GroupKey { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            GroupKey = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
