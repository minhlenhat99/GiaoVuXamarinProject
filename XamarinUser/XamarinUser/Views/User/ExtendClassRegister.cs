using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    using MinhMVC;
    using Models;
    class ExtendClassRegister : BaseView<Models.User, StackLayout>
    {
        protected override void RenderCore()
        {
            int itemSelected = Model.ItemSelected;
            string studentId = Model.studentSelected;

            this.Title = "Register Detail";
            Padding = new Thickness(5, 10, 5, 0);
            
            Label reasonLb = CreateLabel("Lý do");
            Editor entryReason = CreateEditor("");
            Label subjectIdLb = CreateLabel("Mã học phần");
            Entry entrySubjectId = CreateEntry("");
            Label subjectNameLb = CreateLabel("Tên học phần");
            Entry entrySubjectName = CreateEntry("");
            Label newClassIdLb = CreateLabel("Mã lớp đăng ký");
            Entry entryNewClassId = CreateEntry("");
            Label oldClassIdLb = CreateLabel("Mã lớp cũ");
            Entry entryOldClassId = CreateEntry("");
            Button btnSubmit = new Button { VerticalOptions = LayoutOptions.End };
            Button btnDelete = new Button { Text = "Xóa", VerticalOptions = LayoutOptions.End };

            MainContent.Children.Add(reasonLb);
            MainContent.Children.Add(entryReason);
            MainContent.Children.Add(subjectIdLb);
            MainContent.Children.Add(entrySubjectId);
            MainContent.Children.Add(subjectNameLb);
            MainContent.Children.Add(entrySubjectName);
            MainContent.Children.Add(newClassIdLb);
            MainContent.Children.Add(entryNewClassId);
            MainContent.Children.Add(oldClassIdLb);
            MainContent.Children.Add(entryOldClassId);
            // Sinh vien
            if(Model.Account.Role.Id == 1)
            {
                // Dang ky
                if (itemSelected < 0)
                {
                    MainContent.Children.Add(btnSubmit);
                    btnSubmit.Text = "Thêm mới";
                    btnSubmit.Clicked += (s, e) => {
                        var extendClass = new ExtendClass
                        {
                            SubjectId = entrySubjectId.Text,
                            SubjectName = entrySubjectName.Text,
                            NewClassId = entryNewClassId.Text,
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
                    if (data.Reason != null) entryReason.Text = data.Reason;
                    if (data.SubjectId != null) entrySubjectId.Text = data.SubjectId;
                    if (data.SubjectName != null) entrySubjectName.Text = data.SubjectName;
                    if (data.NewClassId != null) entryNewClassId.Text = data.NewClassId;
                    if (data.OldClassId != null) oldClassIdLb.Text = data.OldClassId;
                    if (data.Status.ID == 0)
                    {
                        btnDelete.Clicked += (s, e) => 
                        {
                            Model.Account.ClassList.RemoveAt(itemSelected);
                            Publish();
                            Engine.Execute("User/ExtendClassMainPage");
                        };
                        MainContent.Children.Add(btnDelete);
                        btnSubmit.Text = "Thay đổi";
                        btnSubmit.Clicked += (s, e) =>
                        {
                            var extendClass = new ExtendClass
                            {
                                SubjectId = entrySubjectId.Text,
                                SubjectName = entrySubjectName.Text,
                                NewClassId = entryNewClassId.Text,
                            };
                            Model.Account.ClassList[itemSelected] = extendClass;
                            Publish();
                            Engine.Execute("User/ExtendClassMainPage");
                        };
                        MainContent.Children.Add(btnSubmit);
                    }
                }
            }
            // Giao vu
            else
            {
                var registerClasses = Model.Account.AllRegisterClassList.Find(c => c.Username == studentId);
                var selectedClass = registerClasses.RegisterClassList[itemSelected];
                if (selectedClass.Reason != null) entryReason.Text = selectedClass.Reason;
                if (selectedClass.SubjectId != null) entrySubjectId.Text = selectedClass.SubjectId;
                if (selectedClass.SubjectName != null) entrySubjectName.Text = selectedClass.SubjectName;
                if (selectedClass.NewClassId != null) entryNewClassId.Text = selectedClass.NewClassId;
                if (selectedClass.OldClassId != null) oldClassIdLb.Text = selectedClass.OldClassId;
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
                MainContent.Children.Add(btnApprove);
                MainContent.Children.Add(btnNotApprove);
            }
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
