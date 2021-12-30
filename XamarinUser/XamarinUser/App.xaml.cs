using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MinhMVC;
using System.Threading;

namespace XamarinUser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            Engine.Register(this);
            Engine.Execute("Home/Default");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
