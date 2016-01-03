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
    /// Interaction logic for Disadvantage.xaml
    /// </summary>
    public partial class DisadvantageControl : UserControl
    {
        public int DisadvantageId;
        public DisadvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl(Utility.Disadvantages LoadedDisadvantage)
        {
            DisadvantageId = LoadedDisadvantage.Id;
            lblAdvLabel.Content = LoadedDisadvantage.Name;
            tbDiscription.Text = LoadedDisadvantage.Discription;
            lblAdvPoints.Content = LoadedDisadvantage.PointCost;
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageDisadvantage.AdvWindow.DeleteDisadvantage(this);
        }
    }
}
