using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    using MinhMVC;
    using Models;
    using System.Collections.ObjectModel;
    using System.Linq;

    class ExtendClassRegister : BaseView<Models.User, ScrollView>
    {
        static ObservableCollection<string> subjectData;
        protected override void RenderCore()
        {
            int itemSelected = Model.ItemSelected;
            string studentId = Model.StudentProcessing;
            if(subjectData == null)
            {
                subjectData = new ObservableCollection<string>();
                foreach (var s in Controllers.BaseController.SubjectList)
                {
                    string data = $"{s.ID} {s.Name}";
                    if (s.RequiredTN) data += " (TN)";
                    subjectData.Add(data);
                };
            }

            this.Title = "Register Detail";
            Padding = new Thickness(5, 10, 5, 0);
            var stackLayout = new StackLayout { };
            Label reasonLb = CreateLabel("Lý do");
            Picker reasonPicker = new Picker { Title = "Lý do" };
            reasonPicker.ItemsSource = new List<string> { "Lớp đầy", "Trùng lịch", "Bị hạn chế tín chỉ", "Bị cảnh cáo học tập", "Lý do khác" };
            Label subjectNameLb = CreateLabel("Tên học phần");
            Label newClassIdLb = CreateLabel("Mã lớp đăng ký");
            Entry entryNewClassId = CreateEntry("");
            Label oldClassIdLb = CreateLabel("Mã lớp cũ");
            Entry entryOldClassId = CreateEntry("");
            Label tnClassIDLb = CreateLabel("Mã lớp TN");
            Entry entryTNClassID = CreateEntry("");
            entryTNClassID.IsEnabled = false;
            Button btnSubmit = new Button { VerticalOptions = LayoutOptions.End };
            Button btnDelete = new Button { Text = "Xóa", VerticalOptions = LayoutOptions.End };

            Label subjectLb = CreateLabel("Học phần");
            Entry entrySubject = CreateEntry("");
            var suggestionList = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                IsVisible = false,
            };
            entrySubject.TextChanged += (s, e) =>
            {
                suggestionList.IsVisible = true;
                suggestionList.BeginRefresh();
                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    suggestionList.IsVisible = false;
                    entryTNClassID.IsEnabled = false;
                    entryTNClassID.Text = null;
                }
                else
                    suggestionList.ItemsSource = subjectData.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
                suggestionList.EndRefresh();
            };
            suggestionList.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                label.SetBinding(Label.TextProperty, ".");
                return new ViewCell
                {
                    View = label,
                };
            });
            suggestionList.ItemTapped += (s, e) =>
            {
                String item = e.Item as string;
                entrySubject.Text = item;
                if (item.Contains("(TN)")) entryTNClassID.IsEnabled = true;
                suggestionList.IsVisible = false;
                ((ListView)s).SelectedItem = null;
            };

            stackLayout.Children.Add(reasonLb);
            stackLayout.Children.Add(reasonPicker);
            stackLayout.Children.Add(subjectLb);
            stackLayout.Children.Add(entrySubject);
            stackLayout.Children.Add(suggestionList);
            stackLayout.Children.Add(newClassIdLb);
            stackLayout.Children.Add(entryNewClassId);
            stackLayout.Children.Add(tnClassIDLb);
            stackLayout.Children.Add(entryTNClassID);
            stackLayout.Children.Add(oldClassIdLb);
            stackLayout.Children.Add(entryOldClassId);
            // Sinh vien
            if (Model.Account.Role.Id == 1)
            {
                stackLayout.Children.Add(suggestionList);
                // Dang ky
                if (itemSelected < 0)
                {
                    stackLayout.Children.Add(btnSubmit);
                    btnSubmit.Text = "Thêm mới";
                    btnSubmit.Clicked += (s, e) => {
                        var extendClass = new ExtendClass
                        {
                            Subject = entrySubject.Text,
                            NewClassId = entryNewClassId.Text,
                            TNClassId = entryTNClassID.Text,
                        };
                        extendClass.Status.ID = 0;
                        Model.Account.ClassList.Add(extendClass);
                        Publish();
                        Engine.Execute("User/ExtendClassMainPage");
                    };
                   
                }
                // Chinh sua, xoa
                else
                {
                    var data = Model.Account.ClassList[itemSelected];
                    if (data.Reason != null) reasonPicker.SelectedItem = data.Reason;
                    if (data.Subject != null)
                    {
                        entrySubject.Text = data.Subject;
                        suggestionList.IsVisible = false;
                    }
                    if (data.NewClassId != null) entryNewClassId.Text = data.NewClassId;
                    if (data.OldClassId != null) oldClassIdLb.Text = data.OldClassId;
                    if (data.TNClassId != null) { entryTNClassID.Text = data.TNClassId; entryTNClassID.IsEnabled = true; }
                    if (!Model.Account.HadSendRegister)
                    {
                        btnDelete.Clicked += (s, e) => 
                        {
                            Model.Account.ClassList.RemoveAt(itemSelected);
                            Publish();
                            Engine.Execute("User/ExtendClassMainPage");
                        };
                        stackLayout.Children.Add(btnDelete);
                        btnSubmit.Text = "Thay đổi";
                        btnSubmit.Clicked += (s, e) =>
                        {
                            var extendClass = new ExtendClass
                            {
                                Subject = entrySubject.Text,
                                NewClassId = entryNewClassId.Text,
                                TNClassId = entryTNClassID.Text,
                            };
                            Model.Account.ClassList[itemSelected] = extendClass;
                            Publish();
                            Engine.Execute("User/ExtendClassMainPage");
                        };
                        stackLayout.Children.Add(btnSubmit);
                    }
                    else
                    {
                        stackLayout.Children.Remove(suggestionList);
                    }
                }
            }
            // Giao vu
            else
            {
                stackLayout.Children.Remove(suggestionList);
                var registerClasses = Model.Account.AllRegisterClassList.Find(c => c.Username == studentId);
                var selectedClass = registerClasses.RegisterClassList[itemSelected];
                if (selectedClass.Reason != null)
                {
                    reasonPicker.SelectedItem = selectedClass.Reason;
                    //reasonPicker.IsEnabled = false;
                }
                if (selectedClass.Subject != null)
                {
                    entrySubject.Text = selectedClass.Subject;
                    //entrySubject.IsEnabled = false;
                }
                if (selectedClass.NewClassId != null)
                {
                    entryNewClassId.Text = selectedClass.NewClassId;
                    //entryNewClassId.IsEnabled = false;
                }
                if (selectedClass.OldClassId != null)
                {
                    entryOldClassId.Text = selectedClass.OldClassId;
                    //entryOldClassId.IsEnabled = false;
                }
                if (selectedClass.TNClassId != null) { entryTNClassID.Text = selectedClass.TNClassId; entryTNClassID.IsEnabled = true;}
                Button btnApprove = new Button { Text = "Duyệt", VerticalOptions = LayoutOptions.End, BackgroundColor = Color.LightGreen };
                Button btnNotApprove = new Button { Text = "Không Duyệt", VerticalOptions = LayoutOptions.End, BackgroundColor = Color.PaleVioletRed };
                btnApprove.Clicked += (s, e) =>
                {
                    registerClasses.RegisterClassList[itemSelected].Status.ID = 2;
                    var message = new Dictionary<string, object>();
                    message.Add("Token", Model.Token);
                    message.Add("ClassList", registerClasses);
                    Engine.Execute("Giaovu/Publish", "Giaovu/ExtendClassRegisterUpdate", message);
                };
                btnNotApprove.Clicked += async (s, e) =>
                {
                    await DisplayPromptAsync("Info", "Lý do không duyệt:", "OK", "Cancel").ContinueWith(
                    t =>
                    {
                        if (t.Result != null && t.Result != "")
                        {
                            registerClasses.RegisterClassList[itemSelected].Status.ID = 3;
                            registerClasses.RegisterClassList[itemSelected].Status.Value = t.Result;
                            var message = new Dictionary<string, object>();
                            message.Add("Token", Model.Token);
                            message.Add("ClassList", registerClasses);
                            Engine.Execute("Giaovu/Publish", "Giaovu/ExtendClassRegisterUpdate", message);
                        }
                    });
                };
                stackLayout.Children.Add(btnApprove);
                stackLayout.Children.Add(btnNotApprove);
            }
            MainContent.Content = stackLayout;
        }
        protected override void SetMainPage(object page)
        {
            if(Model.Account.Role.Id == 1)
            {
                ExtendClassMainPage.PageContainter.PushAsync(this);
                base.SetMainPage(ExtendClassMainPage.PageContainter);
            }
            else
            {
                Giaovu.ExtendClassMainPage.PageContainter.PushAsync(this);
                base.SetMainPage(Giaovu.ExtendClassMainPage.PageContainter);
            }
        }
        void Publish()
        {
            var message = new Dictionary<string, object>();
            message.Add("Token", Model.Token);
            message.Add("Info", Model.Account.ClassList);
            Engine.Execute("User/Publish", "User/ExtendClassModify", message);
        }
    }
}
