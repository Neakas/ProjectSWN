using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SWN
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        public static bool musicplaying = false;
        public static MediaPlayer mplayer = new MediaPlayer();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SettingHandler.InitSettingFile();
            SettingHandler.GrabSettingFile();
            SettingHandler.PreloadImages();
            Uri audioclip = new Uri(@"pack://siteoforigin:,,,/Assets/Popskyy - Popskyy MEGA PACK 1 - 26 Dragon's Fire.mp3");
            if (!SettingHandler.GetTurnOffMusic())
            {
                if (!mplayer.HasAudio)
                {
                    mplayer.Open(audioclip);

                }
                if (!musicplaying)
                {
                    mplayer.Play();
                    musicplaying = true;
                }
            }
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
