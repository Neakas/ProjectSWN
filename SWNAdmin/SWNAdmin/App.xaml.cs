using System;
using System.IO;
using System.Windows;

namespace SWNAdmin
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", string.Empty), "Utility"));
        }
    }
}