using System;

namespace SWNAdmin.Classes
{
    public class FileMessage
    {
        public string Sender { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public DateTime Time { get; set; }
    }
}