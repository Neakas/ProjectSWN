using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWNAdmin.Utility;
using System.Linq;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for ManagePrerequisites.xaml
    /// </summary>
    public partial class ManagePrerequisites
    {
        private Dictionary<string, Requirements> _dictPrereq;
        private List<Advantages> _foundAdvantages;
        private List<Attribute> _foundAttributes;
        private List<Requirements> _foundPrereqs;
        private List<Skills> _foundSkills;
        private int _sourceItemId;
        private string _sourceType;
        private int _targetItemId;
        private string _targetType;

        public ManagePrerequisites()
        {
            InitializeComponent();
            var conditions = new List<string> {"<", "<=", "==", ">", ">=", "Needs", "Or"};
            LoadTreeViewContent(TvObjects);
            LoadTreeViewContent(TvTargets);
            LoadSetPrereqList();
            CbConditions.ItemsSource = conditions;
        }

        private void LoadTreeViewContent(TreeView treeview)
        {
            //Load Skills and SkillSpecialitations
            var skillContext = new Db1Entities();
            _foundSkills = (from c in skillContext.Skills.Include("SkillSpecialization") select c).ToList();
            var skillMain = new TreeViewItem {Header = "Skills"};
            foreach (var skillitem in _foundSkills)
            {
                var tvi = new SkillTreeViewItem();
                if (skillitem.SkillSpecialization.Count > 0)
                {
                    foreach (var skillspec in skillitem.SkillSpecialization)
                    {
                        var tvi2 = new SkillTreeViewItem
                        {
                            Header = skillspec.Name,
                            StoredObject = skillspec,
                            Foreground = Brushes.White
                        };
                        if (skillspec.RequirementSet == true)
                        {
                            tvi2.Foreground = Brushes.Green;
                        }
                        tvi.Items.Add(tvi2);
                    }
                }
                tvi.Header = skillitem.SkillName;
                tvi.StoredObject = skillitem;
                tvi.Foreground = Brushes.White;
                if (skillitem.RequirementSet == true)
                {
                    tvi.Foreground = Brushes.Green;
                }
                skillMain.Items.Add(tvi);
            }
            treeview.Items.Add(skillMain);

            //Load Attributes
            var attributeMain = new TreeViewItem {Header = "Attributes"};

            var attributeContext = new Db1Entities();
            _foundAttributes = (from c in attributeContext.Attribute select c).ToList();
            foreach (var attItem in _foundAttributes)
            {
                var tvi = new SkillTreeViewItem
                {
                    Header = attItem.Name,
                    StoredObject = attItem,
                    Foreground = Brushes.White
                };
                if (attItem.RequirementSet == true)
                {
                    tvi.Foreground = Brushes.Green;
                }
                attributeMain.Items.Add(tvi);
            }
            treeview.Items.Add(attributeMain);

            //Load Advantages
            var advantagesMain = new TreeViewItem {Header = "Advantages"};

            var advantageContext = new Db1Entities();
            _foundAdvantages = (from c in advantageContext.Advantages select c).ToList();
            foreach (var advItem in _foundAdvantages)
            {
                var tvi = new SkillTreeViewItem
                {
                    Header = advItem.Name,
                    StoredObject = advItem,
                    Foreground = Brushes.White
                };
                if (advItem.RequirementSet == true)
                {
                    tvi.Foreground = Brushes.Green;
                }
                advantagesMain.Items.Add(tvi);
            }
            treeview.Items.Add(advantagesMain);
        }

        private void tvObjects_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((TvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (Skills))
            {
                var s = ((SkillTreeViewItem) TvObjects.SelectedItem)?.StoredObject as Skills;
                if (s != null)
                {
                    TbObject.Text = s.SkillName;
                    _sourceItemId = s.Id;
                }
                _sourceType = "Skills";
            }

            if ((TvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (SkillSpecialization))
            {
                var ss = ((SkillTreeViewItem) TvObjects.SelectedItem)?.StoredObject as SkillSpecialization;
                if (ss != null)
                {
                    TbObject.Text = ss.Name;
                    _sourceItemId = ss.Id;
                }
                _sourceType = "SkillSpecialization";
            }

            if ((TvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (Attribute))
            {
                var a = ((SkillTreeViewItem) TvObjects.SelectedItem)?.StoredObject as Attribute;
                if (a != null)
                {
                    TbObject.Text = a.Name;
                    _sourceItemId = a.Id;
                }
                _sourceType = "Attribute";
            }

            if ((TvObjects.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() != typeof (Advantages)) return;
            {
                var a = ((SkillTreeViewItem) TvObjects.SelectedItem)?.StoredObject as Advantages;
                if (a != null)
                {
                    TbObject.Text = a.Name;
                    _sourceItemId = a.Id;
                }
                _sourceType = "Advantage";
            }
        }

        private void tvTargets_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((TvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (Skills))
            {
                var s = ((SkillTreeViewItem) TvTargets.SelectedItem)?.StoredObject as Skills;
                if (s != null)
                {
                    TbTarget.Text = s.SkillName;
                    _targetItemId = s.Id;
                }
                _targetType = "Skills";
            }

            if ((TvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (SkillSpecialization))
            {
                var ss = ((SkillTreeViewItem) TvTargets.SelectedItem)?.StoredObject as SkillSpecialization;
                if (ss != null)
                {
                    TbTarget.Text = ss.Name;
                    _targetItemId = ss.Id;
                }
                _targetType = "SkillSpecialization";
            }

            if ((TvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() == typeof (Attribute))
            {
                var a = ((SkillTreeViewItem) TvTargets.SelectedItem)?.StoredObject as Attribute;
                if (a != null)
                {
                    TbTarget.Text = a.Name;
                    _targetItemId = a.Id;
                }
                _targetType = "Attribute";
            }

            if ((TvTargets.SelectedItem as SkillTreeViewItem)?.StoredObject.GetType() != typeof (Advantages)) return;
            {
                var a = ((SkillTreeViewItem) TvTargets.SelectedItem)?.StoredObject as Advantages;
                if (a != null)
                {
                    TbTarget.Text = a.Name;
                    _targetItemId = a.Id;
                }
                _targetType = "Advantage";
            }
        }

        private void tbObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbTarget.Text))
            {
                if (!string.IsNullOrEmpty(TbObject.Text))
                {
                    CbConditions.IsEnabled = true;
                }
            }
            else
            {
                CbConditions.IsEnabled = false;
            }
        }

        private void tbTarget_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbTarget.Text))
            {
                if (!string.IsNullOrEmpty(TbObject.Text))
                {
                    CbConditions.IsEnabled = true;
                }
            }
            else
            {
                CbConditions.IsEnabled = false;
            }
        }

        private void cbConditions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbTarget.Text) || string.IsNullOrEmpty(TbObject.Text)) return;
            if (CbConditions.SelectedValue.ToString() != "Needs" &&
                CbConditions.SelectedValue.ToString() != "Or")
            {
                TbConditionValue.IsEnabled = true;
                BtAdd.IsEnabled = true;
            }
            if (CbConditions.SelectedValue.ToString() == "Needs" ||
                CbConditions.SelectedValue.ToString() == "Or")
            {
                BtAdd.IsEnabled = true;
                TbConditionValue.IsEnabled = false;
            }
            else
            {
                BtAdd.IsEnabled = false;
            }
        }

        private void tbConditionValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtAdd.IsEnabled = !string.IsNullOrEmpty(TbConditionValue.Text);
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Db1Entities())
            {
                var req = new Requirements
                {
                    SourceItemID = _sourceItemId,
                    TargetRequirementID = _targetItemId,
                    TargetType = _targetType,
                    SourceType = _sourceType,
                    Condition = CbConditions.SelectedValue.ToString(),
                    SourceName = TbObject.Text,
                    TargetName = TbTarget.Text,
                    ConditionValue = TbConditionValue.Text
                };
                context.Requirements.Add(req);
                context.SaveChanges();
            }
            var treeViewItem = TvObjects.SelectedItem as TreeViewItem;
            if (treeViewItem != null)
                treeViewItem.IsSelected = false;
            var viewItem = TvTargets.SelectedItem as TreeViewItem;
            if (viewItem != null)
                viewItem.IsSelected = false;
            TbConditionValue.Text = "";
            TbObject.Text = "";
            TbTarget.Text = "";
            LoadSetPrereqList();
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            var item = LbSetPrerequisites.SelectedItem.ToString();
            Requirements reqitem;
            _dictPrereq.TryGetValue(item, out reqitem);

            if (reqitem != null)
            {
                var delContext = new Db1Entities();
                var delQuery = from c in delContext.Requirements where c.Id == reqitem.Id select c;

                using (var context = new Db1Entities())
                {
                    var deletereq = delQuery.FirstOrDefault();
                    context.Entry(deletereq).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            LoadSetPrereqList();
        }

        private void LoadSetPrereqList()
        {
            var requirementContext = new Db1Entities();
            var prereqquery = from c in requirementContext.Requirements select c;
            _foundPrereqs = prereqquery.ToList();

            var prereqList = new List<string>();
            _dictPrereq = new Dictionary<string, Requirements>();

            foreach (var item in _foundPrereqs)
            {
                var prereqString = "(" + item.SourceType + ":" + item.SourceName + ")-(" + item.TargetType + ":" +
                                   item.TargetName + ":" + item.Condition.Replace(" ", "") +
                                   item.ConditionValue.Replace(" ", "") + ")";
                prereqList.Add(prereqString);
                _dictPrereq.Add(prereqString, item);
            }
            LbSetPrerequisites.ItemsSource = prereqList;
        }

        private void lbSetPrerequisites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbSetPrerequisites.SelectedItem != null)
            {
                BtDel.IsEnabled = true;
                BtAdd.IsEnabled = false;
                BtAdd.Visibility = Visibility.Hidden;
            }
            else
            {
                BtDel.IsEnabled = false;
                BtAdd.IsEnabled = true;
                BtAdd.Visibility = Visibility.Visible;
            }
        }
    }

    public class SkillTreeViewItem : TreeViewItem
    {
        public object StoredObject = new object();
    }
}