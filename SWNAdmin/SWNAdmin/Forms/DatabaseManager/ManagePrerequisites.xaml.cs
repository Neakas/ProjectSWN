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
    /// Interaction logic for ManagePrerequisites.xaml
    /// </summary>
    public partial class ManagePrerequisites : Window
    {
        public ManagePrerequisites()
        {
            InitializeComponent();
            List<string> Conditions = new List<string> { "<", "<=", "==",">",">=","Needs" };
            LoadTreeViewContent();
        }

        public void LoadTreeViewContent()
        {
            //Load Skills
            var Context = new Utility.Db1Entities();
            var query = from c in Context.Skills.Include("SkillSPecialization") select c;
            List<Skills> FoundSkills = query.ToList();
            foreach (Skills Skillitem in FoundSkills)
            {
                tvItemList.Items.Add(Skillitem);
            }
        }
    }
}
