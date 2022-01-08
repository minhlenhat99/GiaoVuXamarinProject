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
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        protected override void OnStart()
        {
            Engine.Register(this);
            //Engine.Execute("Home/Default");
            var test = new Views.User.PersonalInfoUpdate();
            ControllerContext ctTest = new ControllerContext();
            test.Render(ctTest);
        }
        
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
