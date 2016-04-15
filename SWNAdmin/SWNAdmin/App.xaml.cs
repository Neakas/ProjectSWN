using System;
using System.IO;

namespace SWNAdmin
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", string.Empty), "Utility"));
        }
    }
}