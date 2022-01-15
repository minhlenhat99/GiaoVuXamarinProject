using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.Giaovu
{
    internal class SetRegisterDuration : User.BaseView<Models.User, StackLayout>
    {
        static int flag = -1;
        protected override void RenderCore()
        {
            DateTime start = Model.Account.Duration.Start;
            DateTime end = Model.Account.Duration.End;

            Padding = new Thickness(5, 10, 10, 0);
            Title = "Set Register Duration";

            var startLb = CreateLabel("Ngày bắt đầu");
            var startBtn = new Button { Text = start.ToString(), BackgroundColor = Color.White };
            var endLb = CreateLabel("Ngày kết thúc");
            var endBtn = new Button { Text = end.ToString(), BackgroundColor = Color.White };
            var setBtn = new Button { Text = "Đặt thời hạn", VerticalOptions = LayoutOptions.End };
            setBtn.Clicked += async (s, e) =>
            {
                int result = DateTime.Compare(start, end);
                if(result < 0) 
                {
                    string option = await DisplayActionSheet("Lựa chọn", "Cancel", null, "Điều chỉnh thời hạn đăng ký hiện tại", "Mở thời hạn đăng ký mới");
                    if(option != null && option != "Cancel")
                    {
                        var continueSend = true;
                        var isNewRegisterDuration = false;
                        if (option.Equals("Mở thời hạn đăng ký mới"))
                        {
                            bool isContinue = await DisplayAlert("Alert", "Lựa chọn này sẽ xóa sạch dữ liệu đăng ký cũ của sinh viên. Bạn có chắc muốn tiếp tục không?", "Continue", "Cancel");
                            if (isContinue) isNewRegisterDuration = true;
                            else continueSend = false;
                        }
                        if (continueSend)
                        {
                            var message = new Dictionary<string, object>();
                            message.Add("Token", Model.Token);
                            message.Add("Duration", new Models.RegisterDuration
                            {
                                Start = start,
                                End = end,
                            });
                            message.Add("IsNewRegisterDuration", isNewRegisterDuration);
                            Engine.Execute("Giaovu/Publish", "Giaovu/SetRegisterDuration", message);
                        }
                    }
                }
                else
                {
                    Engine.Execute("Base/Alert", "Error", "Ngày kết thúc sớm hơn ngày bắt đầu");
                }
            };
            var datePick = new DatePicker
            {
                IsVisible = false,
                IsEnabled = false,
            };
            var timePick = new TimePicker
            {
                IsVisible = false,
                IsEnabled = false,
            };
            startBtn.Clicked += (s, e) =>
            {
                flag = 0;
                datePick.IsEnabled = true;
                datePick.Focus();
            };
            endBtn.Clicked += (s, e) =>
            {
                flag = 1;
                datePick.IsEnabled = true;
                datePick.Focus();
            };
            datePick.Unfocused += (s, e) =>
            {
                switch (flag)
                {
                    case 0:
                        start = datePick.Date;
                        break;
                    case 1:
                        end = datePick.Date;
                        break;
                }
                timePick.IsEnabled = true;
                timePick.Focus();
            };
            timePick.Unfocused += (s, e) =>
            {
                if (flag == 0)
                {
                    start += timePick.Time;
                    startBtn.Text = start.ToString();
                    flag = -1;
                }
                else if (flag == 1)
                {
                    end += timePick.Time;
                    endBtn.Text = end.ToString();
                    flag = -1;
                }
            };

            MainContent.Children.Add(startLb);
            MainContent.Children.Add(startBtn);
            MainContent.Children.Add(endLb);
            MainContent.Children.Add(endBtn);
            MainContent.Children.Add(setBtn);
            MainContent.Children.Add(datePick);
            MainContent.Children.Add(timePick);
        }
        protected override void SetMainPage(object page)
        {
            ExtendClassMainPage.PageContainter.PushAsync(this);
            base.SetMainPage(ExtendClassMainPage.PageContainter);
        }
    }
}
