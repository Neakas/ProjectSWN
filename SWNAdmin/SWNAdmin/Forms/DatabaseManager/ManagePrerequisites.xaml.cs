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
        public List<SWNAdmin.Utility.Attribute> FoundAttributes;
        public List<Requirements> FoundPrereqs;
        Dictionary<String, Requirements> dictPrereq;
        int SourceItemID;
        int TargetItemID;
        string SourceType;
        string TargetType;

        public ManagePrerequisites()
        {
            InitializeComponent();
            List<string> Conditions = new List<string> { "<", "<=", "==",">",">=","Needs","Or" };
            LoadTreeViewContent(tvObjects);
            LoadTreeViewContent(tvTargets);
            LoadSetPrereqList();
            cbConditions.ItemsSource = Conditions;
        }

        public void LoadTreeViewContent(TreeView treeview)
        {
            //Load Skills and SkillSpecialitations
            var SkillContext = new Utility.Db1Entities();
            var skillquery = from c in SkillContext.Skills.Include("SkillSpecialization") select c;
            FoundSkills = skillquery.ToList();
            TreeViewItem SkillMain = new TreeViewItem();
            SkillMain.Header = "Skills";
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
                        tvi2.Foreground = Brushes.White;
                        if (skillspec.RequirementSet == true)
                        {
                            tvi2.Foreground = Brushes.Green;
                        }
                        tvi.Items.Add(tvi2);
                    }
                }
                tvi.Header = Skillitem.SkillName;
                tvi.StoredObject = Skillitem;
                tvi.Foreground = Brushes.White;
                if (Skillitem.RequirementSet == true)
                {
                    tvi.Foreground = Brushes.Green;
                }
                SkillMain.Items.Add(tvi);
            }
            treeview.Items.Add(SkillMain);

            //Load Attributes
            TreeViewItem AttributeMain = new TreeViewItem();
            AttributeMain.Header = "Attributes";

            var AttributeContext = new Utility.Db1Entities();
            var attquery = from c in AttributeContext.Attribute select c;
            FoundAttributes = attquery.ToList();
            foreach (SWNAdmin.Utility.Attribute AttItem in FoundAttributes)
            {
                SkillTreeViewItem tvi = new SkillTreeViewItem();
                tvi.Header = AttItem.Name;
                tvi.StoredObject = AttItem;
                tvi.Foreground = Brushes.White;
                if (AttItem.RequirementSet == true)
                {
                    tvi.Foreground = Brushes.Green;
                }
                AttributeMain.Items.Add(tvi);
            }
            treeview.Items.Add(AttributeMain);
        }

        private void tvObjects_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(Skills))
            {
                Skills s = (((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject) as Skills);
                tbObject.Text = s.SkillName;
                SourceItemID = s.Id;
                SourceType = "Skills";
            }

            if ((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(SkillSpecialization))
            {
                SkillSpecialization ss = (((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject) as SkillSpecialization);
                tbObject.Text = ss.Name;
                SourceItemID = ss.Id;
                SourceType = "SkillSpecialization";
            }

            if ((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(SWNAdmin.Utility.Attribute))
            {
                SWNAdmin.Utility.Attribute a = (((tvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject) as SWNAdmin.Utility.Attribute);
                tbObject.Text = a.Name;
                SourceItemID = a.Id;
                SourceType = "Attribute";
            }
        }

        private void tvTargets_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(Skills))
            {
                Skills s = (((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject) as Skills);
                tbTarget.Text = s.SkillName;
                TargetItemID = s.Id;
                TargetType = "Skills";
            }

            if ((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(SkillSpecialization))
            {
                SkillSpecialization ss = (((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject) as SkillSpecialization);
                tbTarget.Text = ss.Name;
                TargetItemID = ss.Id;
                TargetType = "SkillSpecialization";
            }

            if ((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof(SWNAdmin.Utility.Attribute))
            {
                SWNAdmin.Utility.Attribute a = (((tvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject) as SWNAdmin.Utility.Attribute);
                tbTarget.Text = a.Name;
                TargetItemID = a.Id;
                TargetType = "Attribute";
            }
        }

        private void tbObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbTarget.Text != "" && tbTarget.Text != null)
            {
                if (tbObject.Text != "" && tbObject.Text != null)
                {
                    cbConditions.IsEnabled = true;
                }
            }
            else
            {
                cbConditions.IsEnabled = false;
            }
        }

        private void tbTarget_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbTarget.Text != "" && tbTarget.Text != null)
            {
                if (tbObject.Text != "" && tbObject.Text != null)
                {
                    cbConditions.IsEnabled = true;
                }
            }
            else
            {
                cbConditions.IsEnabled = false;
            }
        }

        private void cbConditions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbTarget.Text != "" && tbTarget.Text != null)
            {
                if (tbObject.Text != "" && tbObject.Text != null)
                {
                    if (cbConditions.SelectedValue.ToString() != "Needs" && cbConditions.SelectedValue.ToString() != "Or")
                    {
                        tbConditionValue.IsEnabled = true;
                        btAdd.IsEnabled = true;
                    }
                    if (cbConditions.SelectedValue.ToString() == "Needs" || cbConditions.SelectedValue.ToString() == "Or")
                    {
                            btAdd.IsEnabled = true;
                            tbConditionValue.IsEnabled = false;
                    }
                    else
                    {
                        btAdd.IsEnabled = false;
                    }
                }
            }
        }

        private void tbConditionValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbConditionValue.Text != "" && tbConditionValue.Text != null)
            {
                btAdd.IsEnabled = true;
            }
            else
            {
                btAdd.IsEnabled = false;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Utility.Db1Entities())
            {
                Requirements req = new Requirements();
                req.SourceItemID = SourceItemID;
                req.TargetRequirementID = TargetItemID;
                req.TargetType = TargetType;
                req.SourceType = SourceType;
                req.Condition = cbConditions.SelectedValue.ToString();
                req.SourceName = tbObject.Text;
                req.TargetName = tbTarget.Text;
                req.ConditionValue = tbConditionValue.Text;
                context.Requirements.Add(req);
                context.SaveChanges();
            }
            (tvObjects.SelectedItem as TreeViewItem).IsSelected = false;
            (tvTargets.SelectedItem as TreeViewItem).IsSelected = false;
            tbConditionValue.Text = "";
            tbObject.Text = "";
            tbTarget.Text = "";
            LoadSetPrereqList();
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            string item = lbSetPrerequisites.SelectedItem.ToString();
            Requirements reqitem;
            dictPrereq.TryGetValue(item, out reqitem);

            if (reqitem != null)
            {
                var DelContext = new Utility.Db1Entities();
                var DelQuery = from c in DelContext.Requirements where c.Id == reqitem.Id select c;

                using (var cnt = new Utility.Db1Entities())
                {
                    Requirements Deletereq = DelQuery.FirstOrDefault();
                    cnt.Entry(Deletereq).State = System.Data.Entity.EntityState.Deleted;
                    cnt.SaveChanges();
                }
            }
            LoadSetPrereqList();
        }

        private void LoadSetPrereqList()
        {
            var RequirementContext = new Utility.Db1Entities();
            var prereqquery = from c in RequirementContext.Requirements select c;
            FoundPrereqs = prereqquery.ToList();

            List<String> prereqList = new List<string>();
            dictPrereq = new Dictionary<string, Requirements>();

            foreach (Requirements item in FoundPrereqs)
            {
                string prereqString = "(" + item.SourceType + ":" + item.SourceName + ")-(" + item.TargetType + ":" + item.TargetName + ":" + item.Condition.Replace(" ", "") + item.ConditionValue.Replace(" ","") + ")";
                prereqList.Add(prereqString);
                dictPrereq.Add(prereqString, item);
            }
            lbSetPrerequisites.ItemsSource = prereqList;
        }

        private void lbSetPrerequisites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSetPrerequisites.SelectedItem != null)
            {
                btDel.IsEnabled = true;
                btAdd.IsEnabled = false;
                btAdd.Visibility = Visibility.Hidden;
            }
            else
            {
                btDel.IsEnabled = false;
                btAdd.IsEnabled = true;
                btAdd.Visibility = Visibility.Visible;
            }
        }
    }

    public class SkillTreeViewItem : TreeViewItem
    {
        public object StoredObject = new object();
    }


}
