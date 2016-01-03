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
    /// Interaction logic for LanguageControl.xaml
    /// </summary>
    public partial class LanguageControl : UserControl
    {
        public LanguageControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LanguageProperty1 = DependencyProperty.Register("SpokenLanguage", typeof(String), typeof(LanguageControl), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SpokenProperty = DependencyProperty.Register("Spoken", typeof(String), typeof(LanguageControl), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty WrittenProperty = DependencyProperty.Register("Written", typeof(String), typeof(LanguageControl), new FrameworkPropertyMetadata(string.Empty));

        public String SpokenLanguage
        {
            get { return GetValue(LanguageProperty1).ToString(); }
            set { SetValue(LanguageProperty1, value); }
        }
        public String Spoken
        {
            get { return GetValue(SpokenProperty).ToString(); }
            set { SetValue(SpokenProperty, value); }
        }
        public String Written
        {
            get { return GetValue(WrittenProperty).ToString(); }
            set { SetValue(WrittenProperty, value); }
        }
    }
}
