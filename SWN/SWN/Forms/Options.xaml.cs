using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using SWN.Controller;
using MessageBox = System.Windows.MessageBox;

namespace SWN.Forms
{
    /// <summary>
    ///     Interaction logic for Options.xaml
    /// </summary>
    public partial class Options
    {
        private XDocument _cfgfile;

        public Options()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            _cfgfile = SettingHandler.GrabSettingFile();
            TbFileDirectory.Text = XmlHandler.GrabXmlValue(_cfgfile, "DataFilePath");
            TbImgDirectory.Text = XmlHandler.GrabXmlValue(_cfgfile, "PicFilePath");
            CbStartMusic.IsChecked = !bool.Parse(XmlHandler.GrabXmlValue(_cfgfile, "TurnOffMusic"));
        }

        private void btSave_Click( object sender, RoutedEventArgs e )
        {
            //TODOLOW Needs Sanity Checks for Correct Directory
            var value = CbStartMusic.IsChecked != null && (bool) CbStartMusic.IsChecked;
            value = !value;
            XmlHandler.SetXmlValue(_cfgfile, "DataFilePath", TbFileDirectory.Text);
            XmlHandler.SetXmlValue(_cfgfile, "PicFilePath", TbImgDirectory.Text);
            XmlHandler.SetXmlValue(_cfgfile, "TurnOffMusic", value.ToString());
            MessageBox.Show("Saved to Config-File");
        }

        private void btFileBrowse_Click( object sender, RoutedEventArgs e )
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TbFileDirectory.Text = fbd.SelectedPath;
            }
        }

        private void btImageBrowse_Click( object sender, RoutedEventArgs e )
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TbImgDirectory.Text = fbd.SelectedPath;
            }
        }
    }
}