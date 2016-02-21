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

namespace SWN
{
    /// <summary>
    /// Interaction logic for AdvantageControl.xaml
    /// </summary>
    public partial class AdvantageControl : UserControl
    {
        public int AdvantageId;
        public AdvantageControl()
        {
            InitializeComponent();
        }

        //public void InitControl(SWNAdmin.Utility.Advantages LoadedAdvantage)
        //{
        //    AdvantageId = LoadedAdvantage.Id;
        //    lblAdvLabel.Content = LoadedAdvantage.Name;
        //    tbDiscription.Text = LoadedAdvantage.Discription;
        //    lblAdvPoints.Content = LoadedAdvantage.PointCost;
        //}

    }
}
