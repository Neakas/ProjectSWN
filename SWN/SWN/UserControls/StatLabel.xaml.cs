using System.Windows;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for StatLabel.xaml
    /// </summary>
    public partial class StatLabel
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (StatLabel), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (string), typeof (StatLabel), new FrameworkPropertyMetadata(string.Empty));

        public StatLabel()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return GetValue(TextProperty).ToString();
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string Value
        {
            get
            {
                return GetValue(ValueProperty).ToString();
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
    }
}