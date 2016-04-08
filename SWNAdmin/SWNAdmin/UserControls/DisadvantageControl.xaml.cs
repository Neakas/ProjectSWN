using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Utility;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for Disadvantage.xaml
    /// </summary>
    public partial class DisadvantageControl : UserControl
    {
        public int DisadvantageId;

        public DisadvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl(Disadvantages LoadedDisadvantage)
        {
            DisadvantageId = LoadedDisadvantage.Id;
            lblAdvLabel.Content = LoadedDisadvantage.Name;
            tbDiscription.Text = LoadedDisadvantage.Discription;
            lblAdvPoints.Content = LoadedDisadvantage.PointCost;
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}