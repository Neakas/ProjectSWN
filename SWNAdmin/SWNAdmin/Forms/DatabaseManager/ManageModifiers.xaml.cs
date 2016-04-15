using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.DatabaseManager
{
    public partial class ManageModifiers
    {
        private Modifier _selectedModifier;

        public ManageModifiers()
        {
            InitializeComponent();
            InitForm();
            LoadComboBoxGroups();
        }

        private void InitForm()
        {
            var context = new Db1Entities();
            CbExistingModifier.ItemsSource = ( from c in context.Modifier select c ).ToList().OrderBy(modifier => modifier.Name);
            CbExistingModifier.DisplayMemberPath = "Name";
        }

        private void LoadComboBoxGroups()
        {
            var context = new Db1Entities();
            CbGroup.ItemsSource = ( from c in context.StatGroup select c ).ToList().OrderBy(statGroup => statGroup.Name);
            CbGroup.DisplayMemberPath = "Name";
        }

        private void LoadComboBoxSubGroups()
        {
            var context = new Db1Entities();
            var sg = CbGroup.SelectedItem as StatGroup;
            CbSubGroup.ItemsSource = ( from c in context.StatSubGroup where c.GroupId == sg.Id select c ).ToList().OrderBy(statSubGroup => statSubGroup.Name);
            CbSubGroup.DisplayMemberPath = "Name";
        }

        private void cbExistingModifier_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (CbExistingModifier.SelectedItem != null)
            {
                BtAdd.Visibility = Visibility.Hidden;
                BtAdd.IsEnabled = false;
                BtUpdate.Visibility = Visibility.Visible;
                BtUpdate.IsEnabled = true;
                BtDelete.IsEnabled = true;
                _selectedModifier = CbExistingModifier.SelectedItem as Modifier;
                if (_selectedModifier == null)
                {
                    return;
                }
                TbId.Text = _selectedModifier.Id.ToString();
                TbModifierName.Text = _selectedModifier.Name;
                TbNotes.Text = _selectedModifier.Notes;
                TbDiscription.Text = _selectedModifier.Description;
                TbModProp.Text = _selectedModifier.Modifying_Property;
                CbGroup.Text = _selectedModifier.Group;
                CbSubGroup.Text = _selectedModifier.SubGroup.Replace(" ", "");
            }
            else
            {
                BtDelete.IsEnabled = false;
            }
        }

        private void btUpdate_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var id = int.Parse(TbId.Text);
            var queryModifier = from c in context.Modifier where c.Id == id select c;

            using (context)
            {
                var updateModifier = queryModifier.FirstOrDefault();
                if (updateModifier != null)
                {
                    updateModifier.Name = TbModifierName.Text;
                    updateModifier.Notes = TbNotes.Text;
                    updateModifier.Description = TbDiscription.Text;
                    updateModifier.Modifying_Property = TbModProp.Text;
                    updateModifier.Group = CbGroup.Text;
                    updateModifier.SubGroup = CbSubGroup.Text;
                    context.Entry(updateModifier).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        private void btDelete_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var id = int.Parse(TbId.Text);
            var queryModifier = from c in context.Modifier where c.Id == id select c;

            using (context)
            {
                var deleteModifier = queryModifier.FirstOrDefault();
                context.Entry(deleteModifier).State = EntityState.Deleted;
                context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        private void btClear_Click( object sender, RoutedEventArgs e )
        {
            CbExistingModifier.SelectedItem = null;
            BtAdd.Visibility = Visibility.Visible;
            BtAdd.IsEnabled = true;
            BtUpdate.Visibility = Visibility.Hidden;
            BtUpdate.IsEnabled = false;
            _selectedModifier = null;
            TbId.Text = "";
            TbModifierName.Text = "";
            TbNotes.Text = "";
            TbDiscription.Text = "";
            TbModProp.Text = "";
            CbGroup.SelectedItem = null;
            CbSubGroup.SelectedItem = null;
        }

        private void btAdd_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var addModifier = new Modifier
                {
                    Name = TbModifierName.Text,
                    Notes = TbNotes.Text,
                    Description = TbDiscription.Text,
                    Modifying_Property = TbModProp.Text,
                    Group = CbGroup.Text,
                    SubGroup = CbSubGroup.Text
                };
                context.Modifier.Add(addModifier);
                context.SaveChanges();
            }
            InitForm();
            btClear_Click(this, null);
        }

        private void btOpenGroups_Click( object sender, RoutedEventArgs e )
        {
            var mg = new ManageGroups();
            mg.ShowDialog();
            LoadComboBoxGroups();
        }

        private void cbGroup_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (CbGroup.SelectedItem != null)
            {
                CbSubGroup.IsEnabled = true;
                LoadComboBoxSubGroups();
            }
            else
            {
                CbSubGroup.IsEnabled = false;
            }
        }

        private void btCopy_Click( object sender, RoutedEventArgs e )
        {
            TbId.Text = "";
            CbExistingModifier.SelectedItem = null;
            BtAdd.Visibility = Visibility.Visible;
            BtAdd.IsEnabled = true;
            BtUpdate.Visibility = Visibility.Hidden;
            BtUpdate.IsEnabled = false;
            _selectedModifier = null;
        }
    }
}