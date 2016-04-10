using System.Windows;
using SWNAdmin.Forms.DatabaseManager;
using SWNAdmin.Utility;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for Advantage.xaml
    /// </summary>
    public partial class AdvantageControl
    {
        public int AdvantageId;

        public AdvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl( Advantages loadedAdvantage )
        {
            AdvantageId = loadedAdvantage.Id;
            LblAdvLabel.Content = loadedAdvantage.Name;
            TbDiscription.Text = loadedAdvantage.Discription;
            TbDiscription.Text += "\n\nLimitation:";
            TbDiscription.Text += "\n" + loadedAdvantage.Limitation;
            LblAdvPoints.Content = loadedAdvantage.PointCost;
        }

        private void btdel_Click( object sender, RoutedEventArgs e )
        {
            ManageAdvantage.AdvWindow.DeleteAdvantage(this);
        }

        private void btedit_Click( object sender, RoutedEventArgs e )
        {
            ManageAdvantage.AdvWindow.UpdateAdvantage(this);
        }
    }
}