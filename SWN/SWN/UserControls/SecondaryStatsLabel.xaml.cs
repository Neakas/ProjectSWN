using System.Windows;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for SecondaryStatsLabel.xaml
    /// </summary>
    public partial class SecondaryStatsLabel
    {
        public static readonly DependencyProperty LiftProperty = DependencyProperty.Register("Lift", typeof (string), typeof (SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof (string), typeof (SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty MoveProperty = DependencyProperty.Register("Move", typeof (string), typeof (SecondaryStatsLabel), new FrameworkPropertyMetadata(string.Empty));

        public SecondaryStatsLabel()
        {
            InitializeComponent();
        }

        public string Lift
        {
            get
            {
                return GetValue(LiftProperty).ToString();
            }
            set
            {
                SetValue(LiftProperty, value);
            }
        }

        public string Speed
        {
            get
            {
                return GetValue(SpeedProperty).ToString();
            }
            set
            {
                SetValue(SpeedProperty, value);
            }
        }

        public string Move
        {
            get
            {
                return GetValue(MoveProperty).ToString();
            }
            set
            {
                SetValue(MoveProperty, value);
            }
        }
    }
}