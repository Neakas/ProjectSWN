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
        public List<Skills> FoundSkills;
       
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
            var query = from c in Context.Skills.Include("SkillSpecialization") select c;
            FoundSkills = query.ToList();
            foreach (Skills Skillitem in FoundSkills)
            {

                SkillTreeViewItem tvi = new SkillTreeViewItem();
                if (Skillitem.SkillSpecialization.Count > 0)
                {
                    foreach (SkillSpecialization skillspec in Skillitem.SkillSpecialization)
                    {
                        SkillTreeViewItem tvi2 = new SkillTreeViewItem();
                        tvi2.Header = skillspec.Name;
                        tvi2.StoredObject = skillspec; 
                        tvi.Items.Add(tvi2);
                    }
                }
                tvi.Header = Skillitem.SkillName;
                tvi.StoredObject = Skillitem;
                tvObjects.Items.Add(tvi);
                tvObjects.DisplayMemberPath = "SkillName";
            }
        }

        public class SkillTreeViewItem : TreeViewItem
        {
            public object StoredObject = new object();
        }
    }
}
