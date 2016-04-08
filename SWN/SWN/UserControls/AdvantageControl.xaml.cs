using System.Windows.Controls;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for AdvantageControl.xaml
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