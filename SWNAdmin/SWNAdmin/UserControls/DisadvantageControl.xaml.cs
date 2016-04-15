using System.Windows;
using SWNAdmin.Utility;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for Disadvantage.xaml
    /// </summary>
    public partial class DisadvantageControl
    {
        public int DisadvantageId;

        public DisadvantageControl()
        {
            InitializeComponent();
        }

        public void InitControl( Disadvantages loadedDisadvantage )
        {
            DisadvantageId = loadedDisadvantage.Id;
            LblAdvLabel.Content = loadedDisadvantage.Name;
            TbDiscription.Text = loadedDisadvantage.Discription;
            LblAdvPoints.Content = loadedDisadvantage.PointCost;
        }

        private void btdel_Click( object sender, RoutedEventArgs e )
        {
        }
    }
}