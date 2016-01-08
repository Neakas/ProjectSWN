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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWNAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for Advantage.xaml
    /// </summary>
    public partial class AdvantageControl : UserControl
    {
        public int AdvantageId;
        public AdvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl(Utility.Advantages LoadedAdvantage)
        {
            AdvantageId = LoadedAdvantage.Id;
            lblAdvLabel.Content = LoadedAdvantage.Name;
            tbDiscription.Text = LoadedAdvantage.Discription;
            tbDiscription.Text += "\n\nLimitation:";
            tbDiscription.Text += "\n" + LoadedAdvantage.Limitation;
            lblAdvPoints.Content = LoadedAdvantage.PointCost;
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageAdvantage.AdvWindow.DeleteAdvantage(this);
        }

        private void btedit_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageAdvantage.AdvWindow.UpdateAdvantage(this);
        }
    }
}
