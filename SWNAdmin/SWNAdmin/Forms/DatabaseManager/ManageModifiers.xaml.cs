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
            LoadComboBoxGroups();
        }
        public void InitForm()
        {
            var Context = new Utility.Db1Entities();
            cbExistingModifier.ItemsSource = (from c in Context.Modifier select c).ToList().OrderBy(Modifier => Modifier.Name);
            cbExistingModifier.DisplayMemberPath = "Name";
        }

        public void LoadComboBoxGroups()
        {
            var context = new Db1Entities();
            cbGroup.ItemsSource = (from c in context.StatGroup select c).ToList().OrderBy(StatGroup => StatGroup.Name);
            cbGroup.DisplayMemberPath = "Name";
        }

        public void LoadComboBoxSubGroups()
        {
            var context = new Db1Entities();
            StatGroup sg = (cbGroup.SelectedItem as StatGroup);
            cbSubGroup.ItemsSource = (from c in context.StatSubGroup where c.GroupId == sg.Id select c).ToList().OrderBy(StatSubGroup => StatSubGroup.Name);
            cbSubGroup.DisplayMemberPath = "Name";
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
                tbModifierName.Text = SelectedModifier.Name?.ToString();
                tbNotes.Text = SelectedModifier.Notes?.ToString();
                tbDiscription.Text = SelectedModifier.Description?.ToString();
                tbModProp.Text = SelectedModifier.Modifying_Property;
                cbGroup.Text = SelectedModifier.Group;
                cbSubGroup.Text = SelectedModifier.SubGroup.Replace(" ","");
            }
            else
            {
                btDelete.IsEnabled = false;
            }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Utility.Db1Entities();
            int id = Int32.Parse(tbId.Text);
            var queryModifier = from c in Context.Modifier where c.Id == id select c;
            
            using (Context)
            {
                Modifier UpdateModifier = queryModifier.FirstOrDefault();
                UpdateModifier.Name = tbModifierName.Text;
                UpdateModifier.Notes = tbNotes.Text;
                UpdateModifier.Description = tbDiscription.Text;
                UpdateModifier.Modifying_Property = tbModProp.Text;
                UpdateModifier.Group = cbGroup.Text.ToString();
                UpdateModifier.SubGroup = cbSubGroup.Text.ToString();
                Context.Entry(UpdateModifier).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
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
            btClear_Click(this, null);
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
            tbDiscription.Text = "";
            tbModProp.Text = "";
            cbGroup.SelectedItem = null;
            cbSubGroup.SelectedItem = null;            
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                Modifier AddModifier = new Modifier();
                AddModifier.Name = tbModifierName.Text;
                AddModifier.Notes = tbNotes.Text;
                AddModifier.Description = tbDiscription.Text;
                AddModifier.Modifying_Property = tbModProp.Text;
                AddModifier.Group = cbGroup.Text.ToString();
                AddModifier.SubGroup = cbSubGroup.Text.ToString();
                Context.Modifier.Add(AddModifier);
                Context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }
        private void btOpenGroups_Click(object sender, RoutedEventArgs e)
        {
            ManageGroups mg = new ManageGroups();
            mg.ShowDialog();
            LoadComboBoxGroups();
        }

        private void cbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGroup.SelectedItem != null)
            {
                cbSubGroup.IsEnabled = true;
                LoadComboBoxSubGroups();
            }
            else
            {
                cbSubGroup.IsEnabled = false;
            }
        }
    }
}

