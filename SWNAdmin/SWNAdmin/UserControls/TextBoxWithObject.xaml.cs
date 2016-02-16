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

namespace SWNAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for TextBoxWithObject.xaml
    /// </summary>
    public partial class TextBoxWithObject : UserControl
    {
        public object StoredObject { get; set; }
        public TextBoxWithObject()
        {
            InitializeComponent();
        }
    }
}
