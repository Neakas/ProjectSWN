using System;
using System.Windows;
using System.Windows.Media;
using SWN.Controller;
using SWN.Properties;

namespace SWN
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static bool Musicplaying;
        public static MediaPlayer Mplayer = new MediaPlayer();

        private void Application_Startup( object sender, StartupEventArgs e )
        {
            SettingHandler.InitSettingFile();
            SettingHandler.GrabSettingFile();
            SettingHandler.PreloadImages();
            var audioclip = new Uri(@"pack://siteoforigin:,,,/Assets/Popskyy - Popskyy MEGA PACK 1 - 26 Dragon's Fire.mp3");
            if (!SettingHandler.GetTurnOffMusic())
            {
                if (!Mplayer.HasAudio)
                {
                    Mplayer.Open(audioclip);
                }
                if (!Musicplaying)
                {
                    Mplayer.Play();
                    Musicplaying = true;
                }
            }
            StartupUri = Settings.Default.LoggedIn ? new Uri("Forms/Login.xaml", UriKind.Relative) : new Uri("Forms/Registration.xaml", UriKind.Relative);
        }
    }
}