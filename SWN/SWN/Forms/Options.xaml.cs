using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SWN.Forms
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        XDocument cfgfile;
        public Options()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            //TODOHIGH Make the Load Work. Check which Config it Grabs
            cfgfile = SettingHandler.GrabSettingFile();
            tbFileDirectory.Text = XmlHandler.GrabXMLValue(cfgfile, "DataFilePath");
            tbImgDirectory.Text = XmlHandler.GrabXMLValue(cfgfile, "PicFilePath");
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            //TODOLOW Needs Sanity Checks for Corret Directory
            XmlHandler.SetXmlValue(cfgfile, "DataFilePath", tbFileDirectory.Text);
            XmlHandler.SetXmlValue(cfgfile, "PicFilePath", tbImgDirectory.Text);
            System.Windows.MessageBox.Show("Saved to Config-File");
        }

        private void btFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult Result = fbd.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                tbFileDirectory.Text = fbd.SelectedPath.ToString();
            }
        }

        private void btImageBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult Result = fbd.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                tbImgDirectory.Text = fbd.SelectedPath.ToString();
            }
        }
    }
}
