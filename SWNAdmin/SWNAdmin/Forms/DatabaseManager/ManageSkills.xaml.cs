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
        public List<Skills> LoadedSkills;
        public Skills SelectedSkill;
        private string TestString;

        public string teststring
        {
            get { return TestString; }
            set { TestString = value; }
        }

        public ManageSkills()
        {
            InitializeComponent();
            InitForm();
        }
        public void InitForm()
        {
            var Context = new Utility.Db1Entities();
            var query = from c in Context.Skills select c;
            LoadedSkills = query.ToList();
            cbExistingSkill.ItemsSource = LoadedSkills.OrderBy(Skills => Skills.SkillName);
            cbExistingSkill.DisplayMemberPath = "SkillName";
        }

        private void cbExistingSkill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbExistingSkill.SelectedItem != null)
            {
                btAdd.Visibility = Visibility.Hidden;
                btAdd.IsEnabled = false;
                btUpdate.Visibility = Visibility.Visible;
                btUpdate.IsEnabled = true;
                btDelete.IsEnabled = true;
                SelectedSkill = cbExistingSkill.SelectedItem as Skills;
                tbId.Text = SelectedSkill.Id.ToString();
                tbSkillName.Text = SelectedSkill.SkillName?.ToString();
                tbDifficultyLevel.Text = SelectedSkill.DifficultyLevel?.ToString();
                tbNotes.Text = SelectedSkill.notes?.ToString();
                tbPoints.Text = SelectedSkill.points?.ToString();
                tbReference.Text = SelectedSkill.reference?.ToString();
                //tbSpecialization.Text = SelectedSkill.specialization?.ToString();
                tbTechLevel.Text = SelectedSkill.tech_level?.ToString();
                tbControllingAttribute.Text = SelectedSkill.ControllingAttribute?.ToString();
                tbDiscription.Text = SelectedSkill.Description?.ToString();
                //tbModifiers.Text = SelectedSkill.Modifiers?.ToString();
                tbDefault.Text = SelectedSkill.Defaults?.ToString();
                tbPrerequisite.Text = SelectedSkill.Prerequisites?.ToString();

                PreparePage1();
                PreparePage2();
                PreparePage3();
            }
            else
            {
                btDelete.IsEnabled = false;
            }
        }

        
        private void PreparePage2()
        {
            tbSkillNameP2.Text = tbSkillName.Text = SelectedSkill.SkillName?.ToString();
        }
        private void PreparePage3()
        {
            tbSkillNameP3.Text = tbSkillName.Text = SelectedSkill.SkillName?.ToString();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            int id = Int32.Parse(tbId.Text);
            var queryskill = from c in Context.Skills where c.Id == id select c;
            
            using (Context)
            {
                Skills UpdateSkill = queryskill.FirstOrDefault();
                UpdateSkill.SkillName = tbSkillName.Text;
                UpdateSkill.DifficultyLevel = tbDifficultyLevel.Text;
                UpdateSkill.notes = tbNotes.Text;
                UpdateSkill.points = Int32.Parse(tbPoints.Text);
                UpdateSkill.reference = tbReference.Text;
                //UpdateSkill.specialization = tbSpecialization.Text;
                UpdateSkill.tech_level = tbTechLevel.Text;
                UpdateSkill.ControllingAttribute = tbControllingAttribute.Text;
                UpdateSkill.Description = tbDiscription.Text;
                //UpdateSkill.Modifiers = tbModifiers.Text;
                UpdateSkill.Defaults = tbDefault.Text;
                UpdateSkill.Prerequisites = tbPrerequisite.Text;
                Context.Entry(UpdateSkill).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            InitForm();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            int id = Int32.Parse(tbId.Text);
            var queryskill = from c in Context.Skills where c.Id == id select c;

            using (Context)
            {
                Skills DeleteSkill = queryskill.FirstOrDefault();
                Context.Entry(DeleteSkill).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            InitForm();
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            cbExistingSkill.SelectedItem = null;
            btAdd.Visibility = Visibility.Visible;
            btAdd.IsEnabled = true;
            btUpdate.Visibility = Visibility.Hidden;
            btUpdate.IsEnabled = false;
            SelectedSkill = null;
            tbId.Text = "";
            tbSkillName.Text = "";
            tbSkillNameP1.Text = "";
            tbSkillNameP2.Text = "";
            tbSkillNameP3.Text = "";
            tbDifficultyLevel.Text = "";
            tbNotes.Text = "";
            tbPoints.Text = "";
            tbReference.Text = "";
            //tbSpecialization.Text = "";
            tbTechLevel.Text = "";
            tbControllingAttribute.Text = "";
            tbDiscription.Text = "";
            //tbModifiers.Text = "";
            tbDefault.Text = "";
            tbPrerequisite.Text = "";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            int result;
            Int32.TryParse(tbPoints.Text, out result);
            using (var Context = new Db1Entities())
            {
                Skills AddSkill = new Skills();
                AddSkill.SkillName = tbSkillName.Text;
                AddSkill.DifficultyLevel = tbDifficultyLevel.Text;
                AddSkill.notes = tbNotes.Text;
                AddSkill.points = result;
                AddSkill.reference = tbReference.Text;
                //AddSkill.specialization = tbSpecialization.Text;
                AddSkill.tech_level = tbTechLevel.Text;
                AddSkill.ControllingAttribute = tbControllingAttribute.Text;
                AddSkill.Description = tbDiscription.Text;
                //AddSkill.Modifiers = tbModifiers.Text;
                AddSkill.Defaults = tbDefault.Text;
                AddSkill.Prerequisites = tbPrerequisite.Text;
                Context.Skills.Add(AddSkill);
                Context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        private void tbId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbId.Text != "")
            {
                tabItemSpecializations.IsEnabled = true;
                tabItemModifiers.IsEnabled = true;
                tabItemRequierements.IsEnabled = true;
            }
            else
            {
                tabItemSpecializations.IsEnabled = false;
                tabItemModifiers.IsEnabled = false;
                tabItemRequierements.IsEnabled = false;
            }
        }


        //Specialization TAB
        private void PreparePage1()
        {
            tbSkillNameP1.Text = tbSkillName.Text = SelectedSkill.SkillName?.ToString();
            int id = Int32.Parse(tbId.Text);
            var Context = new Utility.Db1Entities();
            var query = from c in Context.SkillSpecialization where c.SkillId == id select c;
            List<SkillSpecialization> FoundSpecializations = query.ToList();
            if (FoundSpecializations.Count > 0)
            {
                tabItemSpecializations.Foreground = Brushes.Green;
            }
            else
            {
                tabItemSpecializations.Foreground = Brushes.White;
            }
            lbSpecializations.ItemsSource = FoundSpecializations;
            lbSpecializations.DisplayMemberPath = "Name";
        }

        private void btAddSpec_Click(object sender, RoutedEventArgs e)
        {
            if (tbSpecName.Text != "")
            {
                using (var Context = new Db1Entities())
                {
                    SkillSpecialization ss = new SkillSpecialization();
                    ss.Name = tbSpecName.Text;
                    ss.Prerequisites = tbSpecPrereq.Text;
                    ss.SkillId = Int32.Parse(tbId.Text);
                    ss.Default = tbSpecDefault.Text;
                    ss.Discription = tbSpecDiscription.Text;
                    ss.Modifiers = tbSpecModifiers.Text;
                    ss.IsOptional = cbSpecIsOptional.IsChecked;
                    Context.SkillSpecialization.Add(ss);
                    Context.SaveChanges();
                }
                CleanControls();
            }
            PreparePage1();
        }

        private void btDerlSpec_Click(object sender, RoutedEventArgs e)
        {
            if (lbSpecializations.SelectedItem != null)
            {
                var Context = new Utility.Db1Entities();
                var queryspec = from c in Context.SkillSpecialization where c.Id == ((SkillSpecialization)lbSpecializations.SelectedItem).Id select c;

                using (Context)
                {
                    SkillSpecialization DeleteSpec = queryspec.FirstOrDefault();
                    Context.Entry(DeleteSpec).State = System.Data.Entity.EntityState.Deleted;
                    Context.SaveChanges();
                }
                CleanControls();
            }
            PreparePage1();
        }

        private void lbSpecializations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSpecializations.SelectedItem != null)
            {
                btAddSpec.IsEnabled = false;
                btAddSpec.Visibility = Visibility.Hidden;
                btDerlSpec.IsEnabled = true;
                btUpdateSpec.Visibility = Visibility.Visible;
                btUpdateSpec.IsEnabled = true;
                SkillSpecialization LoadedSpec = lbSpecializations.SelectedItem as SkillSpecialization;
                tbSpecName.Text = LoadedSpec.Name;
                tbSpecDefault.Text = LoadedSpec.Default;
                tbSpecDiscription.Text = LoadedSpec.Discription;
                tbSpecPrereq.Text = LoadedSpec.Prerequisites;
                tbSpecModifiers.Text = LoadedSpec.Modifiers;
                cbSpecIsOptional.IsChecked = LoadedSpec.IsOptional;
            }
            else
            {
                btAddSpec.IsEnabled = true;
                btAddSpec.Visibility = Visibility.Visible;
                btDerlSpec.IsEnabled = false;
                btUpdateSpec.Visibility = Visibility.Hidden;
                btUpdateSpec.IsEnabled = false;
                CleanControls();
            }
        }

        private void CleanControls()
        {
            tbSpecName.Text = "";
            tbSpecDefault.Text = "";
            tbSpecDiscription.Text = "";
            tbSpecPrereq.Text = "";
            tbSpecModifiers.Text = "";
            cbSpecIsOptional.IsChecked = false;
        }

        private void btUpdateSpec_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            var queryspec = from c in Context.SkillSpecialization where c.Id == ((SkillSpecialization)lbSpecializations.SelectedItem).Id select c;

            using (Context)
            {
                SkillSpecialization UpdateSpec = queryspec.FirstOrDefault();
                UpdateSpec.Discription = tbSpecDiscription.Text;
                UpdateSpec.Name = tbSpecName.Text;
                UpdateSpec.Prerequisites = tbSpecPrereq.Text;
                UpdateSpec.Default = tbSpecDefault.Text;
                UpdateSpec.Modifiers = tbSpecModifiers.Text;
                UpdateSpec.IsOptional = cbSpecIsOptional.IsChecked;
                Context.Entry(UpdateSpec).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            lbSpecializations.SelectedItem = null;
            PreparePage1();
        }
    }
}

