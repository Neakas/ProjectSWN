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

namespace SWN.UserControls
{
    /// <summary>
    /// Interaction logic for SecondaryStatsLabel.xaml
    /// </summary>
    public partial class SecondaryStatsLabel : UserControl
    {
        public SecondaryStatsLabel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LiftProperty = DependencyProperty.Register("Lift", typeof(String), typeof(SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(String), typeof(SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty MoveProperty = DependencyProperty.Register("Move", typeof(String), typeof(SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));

        public String Lift
        {
            get { return GetValue(LiftProperty).ToString(); }
            set { SetValue(LiftProperty, value); }
        }
        public String Speed
        {
            get { return GetValue(SpeedProperty).ToString(); }
            set { SetValue(SpeedProperty, value); }
        }
        public String Move
        {
            get { return GetValue(MoveProperty).ToString(); }
            set { SetValue(MoveProperty, value); }
        }

    }
}
