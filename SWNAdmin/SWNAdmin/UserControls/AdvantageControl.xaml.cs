using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Forms;
using SWNAdmin.Utility;
using ManageAdvantage = SWNAdmin.Forms.DatabaseManager.ManageAdvantage;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for Advantage.xaml
    /// </summary>
    public partial class AdvantageControl : UserControl
    {
        public int AdvantageId;

        public AdvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl(Advantages LoadedAdvantage)
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
            ManageAdvantage.AdvWindow.DeleteAdvantage(this);
        }

        private void btedit_Click(object sender, RoutedEventArgs e)
        {
            ManageAdvantage.AdvWindow.UpdateAdvantage(this);
        }
    }
}