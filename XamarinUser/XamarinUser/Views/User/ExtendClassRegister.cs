using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinUser.Views.User
{
    class ExtendClassRegister : BaseView<StackLayout>
    {
        protected override void RenderCore()
        {
            this.Title = "Register Class";
            Padding = new Thickness(5, 0, 5, 0);
            Entry entryReason = new Entry { Placeholder = "Lý do" };
            Entry entrySubjectId = new Entry { Placeholder = "Mã học phần" };
            Entry entrySubjectName = new Entry { Placeholder = "Tên học phần" };
            Entry entryNewClassId = new Entry { Placeholder = "Mã lớp đăng ký" };
            Entry entryOldClassId = new Entry { Placeholder = "Mã lớp cũ" };
            Button btnSubmit = new Button { Text = "Submit" };
            btnSubmit.Clicked += (s, e) => {
            };
            MainContent.Children.Add(entryReason);
            MainContent.Children.Add(entrySubjectId);
            MainContent.Children.Add(entrySubjectName);
            MainContent.Children.Add(entryNewClassId);
            MainContent.Children.Add(entryOldClassId);
            MainContent.Children.Add(btnSubmit);
        }
        protected override void SetMainPage(object page)
        {
            Begin.PageContainter.PushAsync(this);
            base.SetMainPage(Begin.PageContainter);
        }
    }
}
