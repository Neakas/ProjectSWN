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
    /// Interaction logic for StatLabel.xaml
    /// </summary>
    public partial class StatLabel : UserControl
    {
        public StatLabel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(StatLabel), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(String), typeof(StatLabel), new FrameworkPropertyMetadata(string.Empty));

        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public String Value
        {
            get { return GetValue(ValueProperty).ToString(); }
            set { SetValue(ValueProperty, value); }
        }

    }
}
