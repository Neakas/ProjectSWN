using System.Windows;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for LanguageControl.xaml
    /// </summary>
    public partial class LanguageControl
    {
        public static readonly DependencyProperty LanguageProperty1 = DependencyProperty.Register("SpokenLanguage", typeof (string), typeof (LanguageControl), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty SpokenProperty = DependencyProperty.Register("Spoken", typeof (string), typeof (LanguageControl), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty WrittenProperty = DependencyProperty.Register("Written", typeof (string), typeof (LanguageControl), new FrameworkPropertyMetadata(string.Empty));

        public LanguageControl()
        {
            InitializeComponent();
        }

        public string SpokenLanguage
        {
            get
            {
                return GetValue(LanguageProperty1).ToString();
            }
            set
            {
                SetValue(LanguageProperty1, value);
            }
        }

        public string Spoken
        {
            get
            {
                return GetValue(SpokenProperty).ToString();
            }
            set
            {
                SetValue(SpokenProperty, value);
            }
        }

        public string Written
        {
            get
            {
                return GetValue(WrittenProperty).ToString();
            }
            set
            {
                SetValue(WrittenProperty, value);
            }
        }
    }
}