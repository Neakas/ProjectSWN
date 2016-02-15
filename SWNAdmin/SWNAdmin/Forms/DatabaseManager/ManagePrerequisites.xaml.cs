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
            LoadTreeViewContent(tvObjects);
            LoadTreeViewContent(tvTargets);
        }

        public void LoadTreeViewContent(TreeView treeview)
        {
            //Load Skills
            var Context = new Utility.Db1Entities();
            var query = from c in Context.Skills.Include("SkillSpecialization") select c;
            FoundSkills = query.ToList();
            TreeViewItem MainSkillItem = new TreeViewItem();
            foreach (Skills Skillitem in FoundSkills)
            {
                SkillTreeViewItem tvi = new SkillTreeViewItem();
                if (Skillitem.SkillSpecialization.Count > 0)
                {
                    foreach (SkillSpecialization skillspec in Skillitem.SkillSpecialization)
                    {
                        SkillTreeViewItem tvi2 = new SkillTreeViewItem();
                        tvi2.Header = skillspec.Name;
                        tvi2.Foreground = Brushes.White;
                        tvi2.StoredObject = skillspec; 
                        tvi.Items.Add(tvi2);
                    }
                }
                tvi.Header = Skillitem.SkillName;
                tvi.StoredObject = Skillitem;
                tvi.Foreground = Brushes.White;
                MainSkillItem.Items.Add(tvi);
                MainSkillItem.Header = "Skills";
            }
            treeview.Items.Add(MainSkillItem);
            treeview.DisplayMemberPath = "Header";
        }

        public class SkillTreeViewItem : TreeViewItem
        {
            public object StoredObject = new object();
        }

        private void tvObjects_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(SkillTreeViewItem))
            {
                tbObject.Text = ((e.NewValue as SkillTreeViewItem).StoredObject as Skills).SkillName;
            }
        }

        private void tvTargets_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(SkillTreeViewItem))
            {
                if ((e.NewValue as SkillTreeViewItem).StoredObject.GetType() == typeof(Skills))
                {
                    tbTarget.Text = ((e.NewValue as SkillTreeViewItem).StoredObject as Skills).SkillName;
                }
                
            }
            
        }
    }
}
