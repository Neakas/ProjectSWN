using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin
{
    public class FileMessage
    {
        private string sender;
        private string fileName;
        private byte[] data;
        private DateTime time;

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
