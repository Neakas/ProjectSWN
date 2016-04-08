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
    public partial class Options : Window
    {
        private XDocument cfgfile;

        public Options()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            cfgfile = SettingHandler.GrabSettingFile();
            tbFileDirectory.Text = XmlHandler.GrabXmlValue(cfgfile, "DataFilePath");
            tbImgDirectory.Text = XmlHandler.GrabXmlValue(cfgfile, "PicFilePath");
            cbStartMusic.IsChecked = !bool.Parse(XmlHandler.GrabXmlValue(cfgfile, "TurnOffMusic"));
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            //TODOLOW Needs Sanity Checks for Correct Directory
            var value = (bool) cbStartMusic.IsChecked;
            value = !value;
            XmlHandler.SetXmlValue(cfgfile, "DataFilePath", tbFileDirectory.Text);
            XmlHandler.SetXmlValue(cfgfile, "PicFilePath", tbImgDirectory.Text);
            XmlHandler.SetXmlValue(cfgfile, "TurnOffMusic", value.ToString());
            MessageBox.Show("Saved to Config-File");
        }

        private void btFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var Result = fbd.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                tbFileDirectory.Text = fbd.SelectedPath;
            }
        }

        private void btImageBrowse_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var Result = fbd.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                tbImgDirectory.Text = fbd.SelectedPath;
            }
        }
    }
}