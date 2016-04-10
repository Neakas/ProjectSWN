using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for ManageSkills.xaml
    /// </summary>
    public partial class ManageSkills
    {
        private List<Skills> _loadedSkills;
        private Skills _selectedSkill;

        public ManageSkills()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            var context = new Db1Entities();
            var query = from c in context.Skills select c;
            _loadedSkills = query.ToList();
            CbExistingSkill.ItemsSource = _loadedSkills.OrderBy(skills => skills.SkillName);
            CbExistingSkill.DisplayMemberPath = "SkillName";
        }

        private void cbExistingSkill_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (CbExistingSkill.SelectedItem != null)
            {
                BtAdd.Visibility = Visibility.Hidden;
                BtAdd.IsEnabled = false;
                BtUpdate.Visibility = Visibility.Visible;
                BtUpdate.IsEnabled = true;
                BtDelete.IsEnabled = true;
                _selectedSkill = CbExistingSkill.SelectedItem as Skills;
                if (_selectedSkill != null)
                {
                    TbId.Text = _selectedSkill.Id.ToString();
                    TbSkillName.Text = _selectedSkill.SkillName;
                    TbDifficultyLevel.Text = _selectedSkill.DifficultyLevel;
                    TbNotes.Text = _selectedSkill.notes;
                    TbPoints.Text = _selectedSkill.points?.ToString();
                    TbReference.Text = _selectedSkill.reference;
                    //tbSpecialization.Text = _selectedSkill.specialization?.ToString();
                    TbTechLevel.Text = _selectedSkill.tech_level;
                    TbControllingAttribute.Text = _selectedSkill.ControllingAttribute;
                    TbDiscription.Text = _selectedSkill.Description;
                    //tbModifiers.Text = _selectedSkill.Modifiers?.ToString();
                    TbDefault.Text = _selectedSkill.Defaults;
                    TbPrerequisite.Text = _selectedSkill.Prerequisites;
                }

                PreparePage1();
                PreparePage2();
            }
            else
            {
                BtDelete.IsEnabled = false;
            }
        }

        private void PreparePage2()
        {
            //tbSkillName2.Text = TbSkillName.Text = _selectedSkill.SkillName?.ToString();
        }

        private void btUpdate_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var id = int.Parse(TbId.Text);
            var queryskill = from c in context.Skills where c.Id == id select c;

            using (context)
            {
                var updateSkill = queryskill.FirstOrDefault();
                if (updateSkill != null)
                {
                    updateSkill.SkillName = TbSkillName.Text;
                    updateSkill.DifficultyLevel = TbDifficultyLevel.Text;
                    updateSkill.notes = TbNotes.Text;
                    updateSkill.points = int.Parse(TbPoints.Text);
                    updateSkill.reference = TbReference.Text;
                    //UpdateSkill.specialization = tbSpecialization.Text;
                    updateSkill.tech_level = TbTechLevel.Text;
                    updateSkill.ControllingAttribute = TbControllingAttribute.Text;
                    updateSkill.Description = TbDiscription.Text;
                    //UpdateSkill.Modifiers = tbModifiers.Text;
                    updateSkill.Defaults = TbDefault.Text;
                    updateSkill.Prerequisites = TbPrerequisite.Text;
                    context.Entry(updateSkill).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            InitForm();
        }

        private void btDelete_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var id = int.Parse(TbId.Text);
            var queryskill = from c in context.Skills where c.Id == id select c;

            using (context)
            {
                var deleteSkill = queryskill.FirstOrDefault();
                context.Entry(deleteSkill).State = EntityState.Deleted;
                context.SaveChanges();
            }
            InitForm();
        }

        private void btClear_Click( object sender, RoutedEventArgs e )
        {
            CbExistingSkill.SelectedItem = null;
            BtAdd.Visibility = Visibility.Visible;
            BtAdd.IsEnabled = true;
            BtUpdate.Visibility = Visibility.Hidden;
            BtUpdate.IsEnabled = false;
            _selectedSkill = null;
            TbId.Text = "";
            TbSkillName.Text = "";
            TbSkillNameP1.Text = "";
            TbDifficultyLevel.Text = "";
            TbNotes.Text = "";
            TbPoints.Text = "";
            TbReference.Text = "";
            //tbSpecialization.Text = "";
            TbTechLevel.Text = "";
            TbControllingAttribute.Text = "";
            TbDiscription.Text = "";
            //tbModifiers.Text = "";
            TbDefault.Text = "";
            TbPrerequisite.Text = "";
        }

        private void btAdd_Click( object sender, RoutedEventArgs e )
        {
            int result;
            int.TryParse(TbPoints.Text, out result);
            using (var context = new Db1Entities())
            {
                var addSkill = new Skills
                {
                    SkillName = TbSkillName.Text,
                    DifficultyLevel = TbDifficultyLevel.Text,
                    notes = TbNotes.Text,
                    points = result,
                    reference = TbReference.Text,
                    tech_level = TbTechLevel.Text,
                    ControllingAttribute = TbControllingAttribute.Text,
                    Description = TbDiscription.Text,
                    Defaults = TbDefault.Text,
                    Prerequisites = TbPrerequisite.Text
                };
                //AddSkill.specialization = tbSpecialization.Text;
                //AddSkill.Modifiers = tbModifiers.Text;
                context.Skills.Add(addSkill);
                context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        private void tbId_TextChanged( object sender, TextChangedEventArgs e )
        {
            if (TbId.Text != "")
            {
                TabItemSpecializations.IsEnabled = true;
                TabItemModifiers.IsEnabled = true;
            }
            else
            {
                TabItemSpecializations.IsEnabled = false;
                TabItemModifiers.IsEnabled = false;
            }
        }

        //Specialization TAB
        private void PreparePage1()
        {
            TbSkillNameP1.Text = TbSkillName.Text = _selectedSkill.SkillName;
            var id = int.Parse(TbId.Text);
            var context = new Db1Entities();
            var query = from c in context.SkillSpecialization where c.SkillId == id select c;
            var foundSpecializations = query.ToList();
            TabItemSpecializations.Foreground = foundSpecializations.Count > 0 ? Brushes.Green : Brushes.White;
            LbSpecializations.ItemsSource = foundSpecializations;
            LbSpecializations.DisplayMemberPath = "Name";
        }

        private void btAddSpec_Click( object sender, RoutedEventArgs e )
        {
            if (TbSpecName.Text != "")
            {
                using (var context = new Db1Entities())
                {
                    var ss = new SkillSpecialization
                    {
                        Name = TbSpecName.Text,
                        Prerequisites = TbSpecPrereq.Text,
                        SkillId = int.Parse(TbId.Text),
                        Default = TbSpecDefault.Text,
                        Discription = TbSpecDiscription.Text,
                        Modifiers = TbSpecModifiers.Text,
                        IsOptional = CbSpecIsOptional.IsChecked
                    };
                    context.SkillSpecialization.Add(ss);
                    context.SaveChanges();
                }
                CleanControls();
            }
            PreparePage1();
        }

        private void btDerlSpec_Click( object sender, RoutedEventArgs e )
        {
            if (LbSpecializations.SelectedItem != null)
            {
                var context = new Db1Entities();
                var queryspec = from c in context.SkillSpecialization where c.Id == ( (SkillSpecialization) LbSpecializations.SelectedItem ).Id select c;

                using (context)
                {
                    var deleteSpec = queryspec.FirstOrDefault();
                    context.Entry(deleteSpec).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                CleanControls();
            }
            PreparePage1();
        }

        private void lbSpecializations_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (LbSpecializations.SelectedItem != null)
            {
                BtAddSpec.IsEnabled = false;
                BtAddSpec.Visibility = Visibility.Hidden;
                BtDerlSpec.IsEnabled = true;
                BtUpdateSpec.Visibility = Visibility.Visible;
                BtUpdateSpec.IsEnabled = true;
                var loadedSpec = LbSpecializations.SelectedItem as SkillSpecialization;
                if (loadedSpec != null)
                {
                    TbSpecName.Text = loadedSpec.Name;
                    TbSpecDefault.Text = loadedSpec.Default;
                    TbSpecDiscription.Text = loadedSpec.Discription;
                    TbSpecPrereq.Text = loadedSpec.Prerequisites;
                    TbSpecModifiers.Text = loadedSpec.Modifiers;
                    CbSpecIsOptional.IsChecked = loadedSpec.IsOptional;
                }
            }
            else
            {
                BtAddSpec.IsEnabled = true;
                BtAddSpec.Visibility = Visibility.Visible;
                BtDerlSpec.IsEnabled = false;
                BtUpdateSpec.Visibility = Visibility.Hidden;
                BtUpdateSpec.IsEnabled = false;
                CleanControls();
            }
        }

        private void CleanControls()
        {
            TbSpecName.Text = "";
            TbSpecDefault.Text = "";
            TbSpecDiscription.Text = "";
            TbSpecPrereq.Text = "";
            TbSpecModifiers.Text = "";
            CbSpecIsOptional.IsChecked = false;
        }

        private void btUpdateSpec_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var queryspec = from c in context.SkillSpecialization where c.Id == ( (SkillSpecialization) LbSpecializations.SelectedItem ).Id select c;

            using (context)
            {
                var updateSpec = queryspec.FirstOrDefault();
                if (updateSpec != null)
                {
                    updateSpec.Discription = TbSpecDiscription.Text;
                    updateSpec.Name = TbSpecName.Text;
                    updateSpec.Prerequisites = TbSpecPrereq.Text;
                    updateSpec.Default = TbSpecDefault.Text;
                    updateSpec.Modifiers = TbSpecModifiers.Text;
                    updateSpec.IsOptional = CbSpecIsOptional.IsChecked;
                    context.Entry(updateSpec).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            LbSpecializations.SelectedItem = null;
            PreparePage1();
        }
    }
}