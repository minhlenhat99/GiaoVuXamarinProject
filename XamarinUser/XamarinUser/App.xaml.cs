using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MinhMVC;
using System.Threading;
using System.Text;
using System.Security.Cryptography;

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
            //var test = new Views.User.PersonalInfoUpdate();
            //ControllerContext ctTest = new ControllerContext();
            //test.Render(ctTest);
        }
        
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
