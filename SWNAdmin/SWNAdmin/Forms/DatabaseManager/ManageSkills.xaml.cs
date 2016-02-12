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
using System.Windows.Shapes;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for ManageSkills.xaml
    /// </summary>
    public partial class ManageSkills : Window
    {
        private skill_list SkillList;
        public ManageSkills()
        {
            InitializeComponent();
            SkillList = skill_list.Load();
        }
    }
}
