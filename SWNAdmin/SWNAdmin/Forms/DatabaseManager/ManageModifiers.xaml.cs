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
    /// Interaction logic for ManageModifier.xaml
    /// </summary>
    public partial class ManageModifiers : Window
    {
        public List<Modifier> LoadedModifier;
        public Modifier SelectedModifier;
        private string TestString;

        public string teststring
        {
            get { return TestString; }
            set { TestString = value; }
        }

        public ManageModifiers()
        {
            InitializeComponent();
            InitForm();
        }
        public void InitForm()
        {
            var Context = new Utility.Db1Entities();
            var query = from c in Context.Modifier select c;
            LoadedModifier = query.ToList();
            cbExistingModifier.ItemsSource = LoadedModifier.OrderBy(Modifier => Modifier.Id);
            cbExistingModifier.DisplayMemberPath = "ModifierName";
        }

        private void cbExistingModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbExistingModifier.SelectedItem != null)
            {
                btAdd.Visibility = Visibility.Hidden;
                btAdd.IsEnabled = false;
                btUpdate.Visibility = Visibility.Visible;
                btUpdate.IsEnabled = true;
                btDelete.IsEnabled = true;
                SelectedModifier = cbExistingModifier.SelectedItem as Modifier;
                tbId.Text = SelectedModifier.Id.ToString();
                //tbModifierName.Text = SelectedModifier.ModifierName?.ToString();
                //tbDifficultyLevel.Text = SelectedModifier.DifficultyLevel?.ToString();
                //tbNotes.Text = SelectedModifier.notes?.ToString();
                //tbPoints.Text = SelectedModifier.points?.ToString();
                //tbReference.Text = SelectedModifier.reference?.ToString();
                //tbSpecialization.Text = SelectedModifier.specialization?.ToString();
                //tbTechLevel.Text = SelectedModifier.tech_level?.ToString();
                //tbControllingAttribute.Text = SelectedModifier.ControllingAttribute?.ToString();
                //tbDiscription.Text = SelectedModifier.Description?.ToString();
                //tbModifiers.Text = SelectedModifier.Modifiers?.ToString();
                //tbDefault.Text = SelectedModifier.Defaults?.ToString();
                //tbPrerequisite.Text = SelectedModifier.Prerequisites?.ToString();

                PreparePage1();
                PreparePage2();
            }
            else
            {
                btDelete.IsEnabled = false;
            }
        }

        private void PreparePage2()
        {
            //tbModifierName2.Text = tbModifierName.Text = SelectedModifier.ModifierName?.ToString();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            int id = Int32.Parse(tbId.Text);
            var queryModifier = from c in Context.Modifier where c.Id == id select c;
            
            using (Context)
            {
                Modifier UpdateModifier = queryModifier.FirstOrDefault();
                //UpdateModifier.ModifierName = tbModifierName.Text;
                //UpdateModifier.DifficultyLevel = tbDifficultyLevel.Text;
                //UpdateModifier.notes = tbNotes.Text;
                //UpdateModifier.points = Int32.Parse(tbPoints.Text);
                //UpdateModifier.reference = tbReference.Text;
                ////UpdateModifier.specialization = tbSpecialization.Text;
                //UpdateModifier.tech_level = tbTechLevel.Text;
                //UpdateModifier.ControllingAttribute = tbControllingAttribute.Text;
                //UpdateModifier.Description = tbDiscription.Text;
                ////UpdateModifier.Modifiers = tbModifiers.Text;
                //UpdateModifier.Defaults = tbDefault.Text;
                //UpdateModifier.Prerequisites = tbPrerequisite.Text;
                Context.Entry(UpdateModifier).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            InitForm();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            int id = Int32.Parse(tbId.Text);
            var queryModifier = from c in Context.Modifier where c.Id == id select c;

            using (Context)
            {
                Modifier DeleteModifier = queryModifier.FirstOrDefault();
                Context.Entry(DeleteModifier).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            InitForm();
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            cbExistingModifier.SelectedItem = null;
            btAdd.Visibility = Visibility.Visible;
            btAdd.IsEnabled = true;
            btUpdate.Visibility = Visibility.Hidden;
            btUpdate.IsEnabled = false;
            SelectedModifier = null;
            tbId.Text = "";
            tbModifierName.Text = "";           
            tbNotes.Text = "";
            tbPoints.Text = "";
            tbReference.Text = "";
            //tbSpecialization.Text = "";
            tbControllingAttribute.Text = "";
            tbDiscription.Text = "";
            //tbModifiers.Text = "";
            tbDefault.Text = "";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            int result;
            Int32.TryParse(tbPoints.Text, out result);
            using (var Context = new Db1Entities())
            {
                Modifier AddModifier = new Modifier();
                //AddModifier.ModifierName = tbModifierName.Text;
                //AddModifier.DifficultyLevel = tbDifficultyLevel.Text;
                //AddModifier.notes = tbNotes.Text;
                //AddModifier.points = result;
                //AddModifier.reference = tbReference.Text;
                ////AddModifier.specialization = tbSpecialization.Text;
                //AddModifier.tech_level = tbTechLevel.Text;
                //AddModifier.ControllingAttribute = tbControllingAttribute.Text;
                //AddModifier.Description = tbDiscription.Text;
                ////AddModifier.Modifiers = tbModifiers.Text;
                //AddModifier.Defaults = tbDefault.Text;
                //AddModifier.Prerequisites = tbPrerequisite.Text;
                Context.Modifier.Add(AddModifier);
                Context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        //Specialization TAB
        private void PreparePage1()
        {
            //tbModifierNameP1.Text = tbModifierName.Text = SelectedModifier.ModifierName?.ToString();
            //int id = Int32.Parse(tbId.Text);
            //var Context = new Utility.Db1Entities();
            //var query = from c in Context.Modifierpecialization where c.ModifierId == id select c;
            //List<Modifierpecialization> FoundSpecializations = query.ToList();
            //if (FoundSpecializations.Count > 0)
            //{
            //    tabItemSpecializations.Foreground = Brushes.Green;
            //}
            //else
            //{
            //    tabItemSpecializations.Foreground = Brushes.White;
            //}
            //lbSpecializations.ItemsSource = FoundSpecializations;
            //lbSpecializations.DisplayMemberPath = "Name";
        }

        //private void btAddSpec_Click(object sender, RoutedEventArgs e)
        //{
        //    if (tbSpecName.Text != "")
        //    {
        //        using (var Context = new Db1Entities())
        //        {
        //            Modifierpecialization ss = new Modifierpecialization();
        //            ss.Name = tbSpecName.Text;
        //            ss.Prerequisites = tbSpecPrereq.Text;
        //            ss.ModifierId = Int32.Parse(tbId.Text);
        //            ss.Default = tbSpecDefault.Text;
        //            ss.Discription = tbSpecDiscription.Text;
        //            ss.Modifiers = tbSpecModifiers.Text;
        //            ss.IsOptional = cbSpecIsOptional.IsChecked;
        //            Context.Modifierpecialization.Add(ss);
        //            Context.SaveChanges();
        //        }
        //        CleanControls();
        //    }
        //    PreparePage1();
        //}

        //private void btDerlSpec_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lbSpecializations.SelectedItem != null)
        //    {
        //        var Context = new Utility.Db1Entities();
        //        var queryspec = from c in Context.Modifierpecialization where c.Id == ((Modifierpecialization)lbSpecializations.SelectedItem).Id select c;

        //        using (Context)
        //        {
        //            Modifierpecialization DeleteSpec = queryspec.FirstOrDefault();
        //            Context.Entry(DeleteSpec).State = System.Data.Entity.EntityState.Deleted;
        //            Context.SaveChanges();
        //        }
        //        CleanControls();
        //    }
        //    PreparePage1();
        //}

        //private void lbSpecializations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (lbSpecializations.SelectedItem != null)
        //    {
        //        btAddSpec.IsEnabled = false;
        //        btAddSpec.Visibility = Visibility.Hidden;
        //        btDerlSpec.IsEnabled = true;
        //        btUpdateSpec.Visibility = Visibility.Visible;
        //        btUpdateSpec.IsEnabled = true;
        //        Modifierpecialization LoadedSpec = lbSpecializations.SelectedItem as Modifierpecialization;
        //        tbSpecName.Text = LoadedSpec.Name;
        //        tbSpecDefault.Text = LoadedSpec.Default;
        //        tbSpecDiscription.Text = LoadedSpec.Discription;
        //        tbSpecPrereq.Text = LoadedSpec.Prerequisites;
        //        tbSpecModifiers.Text = LoadedSpec.Modifiers;
        //        cbSpecIsOptional.IsChecked = LoadedSpec.IsOptional;
        //    }
        //    else
        //    {
        //        btAddSpec.IsEnabled = true;
        //        btAddSpec.Visibility = Visibility.Visible;
        //        btDerlSpec.IsEnabled = false;
        //        btUpdateSpec.Visibility = Visibility.Hidden;
        //        btUpdateSpec.IsEnabled = false;
        //        CleanControls();
        //    }
        //}

        //private void CleanControls()
        //{
        //    tbSpecName.Text = "";
        //    tbSpecDefault.Text = "";
        //    tbSpecDiscription.Text = "";
        //    tbSpecPrereq.Text = "";
        //    tbSpecModifiers.Text = "";
        //    cbSpecIsOptional.IsChecked = false;
        //}

        //private void btUpdateSpec_Click(object sender, RoutedEventArgs e)
        //{
        //    var Context = new Utility.Db1Entities();
        //    var queryspec = from c in Context.Modifierpecialization where c.Id == ((Modifierpecialization)lbSpecializations.SelectedItem).Id select c;

        //    using (Context)
        //    {
        //        Modifierpecialization UpdateSpec = queryspec.FirstOrDefault();
        //        UpdateSpec.Discription = tbSpecDiscription.Text;
        //        UpdateSpec.Name = tbSpecName.Text;
        //        UpdateSpec.Prerequisites = tbSpecPrereq.Text;
        //        UpdateSpec.Default = tbSpecDefault.Text;
        //        UpdateSpec.Modifiers = tbSpecModifiers.Text;
        //        UpdateSpec.IsOptional = cbSpecIsOptional.IsChecked;
        //        Context.Entry(UpdateSpec).State = System.Data.Entity.EntityState.Modified;
        //        Context.SaveChanges();
        //    }
        //    lbSpecializations.SelectedItem = null;
        //    PreparePage1();
        //}
    }
}

