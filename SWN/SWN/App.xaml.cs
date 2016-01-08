using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SWN
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SettingHandler.InitSettingFile();
            SettingHandler.GrabSettingFile();
            SettingHandler.PreloadImages();
            if (SWN.Properties.Settings.Default.LoggedIn == true)
            {
                StartupUri = new Uri("Forms/Login.xaml", UriKind.Relative);
            }
            else
            {
                StartupUri = new Uri("Forms/Registration.xaml", UriKind.Relative);
            }
        }
    }
}
