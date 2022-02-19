using MinhMVC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinUser.Controllers;

namespace XamarinUser.Views.User
{
    class ExtendClassMainPage : BaseView<Models.User, StackLayout>
    {
        //static string ViewStudent { get; set; }
        protected override void RenderCore()
        {
            int cmp1 = DateTime.Compare(DateTime.Now, Model.Account.Duration.Start);
            int cmp2 = DateTime.Compare(DateTime.Now, Model.Account.Duration.End);
            if(cmp1 < 0 || cmp2 > 0)
            {
                MainContent.Children.Add(NotRegisterDurationLabel());
            }
            else
            {
                var title = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center
                };
                MainContent.Children.Add(title);
                // Sinh vien
                if (Model.Account.Role.Id == 1)
                {
                    Title = "Student Register Main";
                    title.Text = "Danh sách lớp đăng ký";
                    ListView classRegisterView = new ListView();
                    classRegisterView.ItemsSource = Model.Account.ClassList;
                    classRegisterView.ItemTemplate = new DataTemplate(() =>
                    {
                        TextCell textCell = new TextCell();
                        textCell.SetBinding(TextCell.TextProperty, "Subject");
                        textCell.TextColor = Color.Black;
                        textCell.SetBinding(TextCell.DetailProperty, "Status.Value");
                        textCell.SetBinding(TextCell.DetailColorProperty, "Status.Color");
                        return textCell;
                    });
                    classRegisterView.ItemSelected += (s, e) =>
                    {
                        var item = e.SelectedItem as Models.ExtendClass;
                        var index = e.SelectedItemIndex;
                        if (item != null)
                        {
                            Model.ItemProcessing = index;
                            Engine.Execute("User/ExtendClassRegister");
                            classRegisterView.SelectedItem = null;
                            Model.ItemProcessing = -1;
                        }
                    };
                    MainContent.Children.Add(classRegisterView);
                    Button add = new Button { Text = "Thêm đăng ký mới", VerticalOptions = LayoutOptions.End };
                    add.Clicked += (s, e) =>
                    {
                        Engine.Execute("User/ExtendClassRegister");
                    };
                    MainContent.Children.Add(add);
                    Button register = new Button { Text = "Gửi đăng ký", VerticalOptions = LayoutOptions.End };
                    register.Clicked += async (s, e) =>
                    {
                        await DisplayAlert("Warning", "Bạn chỉ có một lần đăng ký duy nhất. Bạn có chắc không?", "Có", "Không").ContinueWith(
                        t =>
                        {
                            if (t.Result)
                            {
                                var message = new Dictionary<string, object>();
                                message.Add("Token", Model.Token);
                                Engine.Execute("User/Publish", "User/ExtendClassRegister", message);
                                Engine.Execute("User/ExtendClassMainPage");
                            }
                        });
                    };
                    MainContent.Children.Add(register);
                    if (Model.Account.ClassList.Count == 3) add.IsEnabled = false;
                    if (Model.Account.HadSendRegister)
                    {
                        add.IsEnabled = false;
                        register.IsEnabled = false;
                    }
                }
                // Nhan vien giao vu
                else
                {
                    Title = "Register List";
                    title.Text = "Danh sách học sinh đăng ký";
                    var registerView = new CarouselView
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };
                    registerView.ItemsSource = Model.Account.AllRegisterClassList;
                    registerView.ItemTemplate = new DataTemplate(() =>
                    {
                        var mssv = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black, };
                        mssv.SetBinding(Label.TextProperty, "Username");
                        var classRegisterList = new ListView();
                        classRegisterList.SetBinding(ListView.ItemsSourceProperty, "RegisterClassList");
                        classRegisterList.ItemTemplate = new DataTemplate(() =>
                        {
                            TextCell textCell = new TextCell();
                            textCell.SetBinding(TextCell.TextProperty, "Subject");
                            textCell.TextColor = Color.Black;
                            textCell.SetBinding(TextCell.DetailProperty, "Status.Value");
                            textCell.SetBinding(TextCell.DetailColorProperty, "Status.Color");
                            return textCell;
                        });
                        classRegisterList.ItemSelected += (s, e) =>
                        {
                            var item = e.SelectedItem as Models.ExtendClass;
                            var index = e.SelectedItemIndex;
                            if (item != null)
                            {
                                Model.ItemProcessing = index;
                                Engine.Execute("User/ExtendClassRegister");
                                classRegisterList.SelectedItem = null;
                                Model.ItemProcessing = -1;
                            }
                        };
                        Button finish = new Button { Text = "Hoàn thành" };
                        finish.Clicked += (s, e) =>
                        {
                            var data = Model.Account.AllRegisterClassList.Find(c => c.Username == mssv.Text);
                            var stuClassRegister = data.RegisterClassList;
                            var isFinished = true;
                            foreach (var c in stuClassRegister)
                            {
                                if (c.Status.ID == 1)
                                {
                                    isFinished = false;
                                    break;
                                }
                            }
                            if (isFinished)
                            {
                                var message = new Dictionary<string, object>();
                                message.Add("Token", Model.Token);
                                message.Add("Data", data);
                                Engine.Execute("Giaovu/Publish", "Giaovu/ExtendClassRegisterFinish", message);
                            }
                            else
                            {
                                Engine.Execute("Base/Alert", "Error", "Chưa duyệt hết các lớp");
                            }

                        };
                        var test = new StackLayout
                        {
                            Padding = new Thickness(15, 15),
                        };
                        test.Children.Add(mssv);
                        test.Children.Add(classRegisterList);
                        test.Children.Add(finish);
                        return test;
                    });
                    if(Model.StudentProcessing == null || Model.Account.AllRegisterClassList.Find(s => s.Username == Model.StudentProcessing) == null)
                    {
                        registerView.Position = 0;
                        if(Model.Account.AllRegisterClassList.Count > 0) Model.StudentProcessing = Model.Account.AllRegisterClassList[registerView.Position].Username;
                    }
                    else
                    {
                        registerView.Position = Model.Account.AllRegisterClassList.FindIndex(s => s.Username == Model.StudentProcessing);
                    }
                    registerView.PositionChanged += (s, e) =>
                    {
                        Model.StudentProcessing = Model.Account.AllRegisterClassList[registerView.Position].Username;
                        //Engine.Execute("User/UpdateUser", Model);
                        BaseController.User = Model;
                    };
                    MainContent.Children.Add(registerView);
                }
            }
            
        }
        protected override void SetMainPage(object page)
        {
            if(Model.Account.Role.Id == 1)
            {
                _pageContainer = null;
                PageContainter.PushAsync(this);
                base.SetMainPage(PageContainter);
            }
            else
            {
                if (!Giaovu.ExtendClassMainPage.PageContainter.CurrentPage.GetType().Equals(typeof(Giaovu.ExtendClassMainPage)))
                {
                    Engine.Execute("Giaovu/ExtendClassMainPage");
                }
                Giaovu.ExtendClassMainPage.PageContainter.PushAsync(this);
                base.SetMainPage(Giaovu.ExtendClassMainPage.PageContainter);
            }
            
        }
    }
}
