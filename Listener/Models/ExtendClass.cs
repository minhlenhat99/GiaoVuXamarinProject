using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class ExtendClass
    {
        private Status _status;

        public string Reason { get; set; }
        public string Subject { get; set; }
        public string NewClassId { get; set; }
        public string OldClassId { get; set; }
        public string TNClassId { get; set; }
        public Status Status
        {
            get
            {
                if (_status == null) _status = new Status();
                return _status;
            }
            set { _status = value; }
        }
    }

    public class Status
    {
        private int _id;
        public int ID
        {
            get
            {
                if(_id == 0)
                {
                    Value = "Chưa gửi";
                    Color = Color.DarkGray;
                }
                else if (_id == 1)
                {
                    Value = "Chờ duyệt";
                    Color = Color.SandyBrown;
                }
                else if (_id == 2)
                {
                    Value = "Đã duyệt";
                    Color = Color.DarkGreen;
                }
                else
                {
                    Color = Color.DarkRed;
                }
                return _id;
            }
            set { _id = value; }
        }
        public string Value { get; set; }
        public Color Color { get; set; }
    }
}
