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
    /// Interaction logic for ManageGroups.xaml
    /// </summary>
    public partial class ManageGroups : Window
    {
        public ManageGroups()
        {
            InitializeComponent();
            LoadListBoxContent();
            LoadSubListBoxContent();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                StatGroup sg = new StatGroup();
                sg.Name = tbGroupName.Text;
                Context.StatGroup.Add(sg);
                Context.SaveChanges();
            }
            LoadListBoxContent();
            tbGroupName.Text = "";
            tbGroupName1.Text = "";
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Db1Entities();
            StatGroup DelGroup = (lbGroups.SelectedItem as StatGroup);
            var Query = from c in Context.StatGroup where DelGroup.Id == c.Id select c;
            DelGroup = Query.FirstOrDefault();

            using (Context)
            {
                StatGroup sg = DelGroup;
                Context.Entry(sg).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void btUpd_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Db1Entities();
            StatGroup UpdateGroup = (lbGroups.SelectedItem as StatGroup);
            var Query = from c in Context.StatGroup where UpdateGroup.Id == c.Id select c;
            UpdateGroup = Query.FirstOrDefault();

            using (Context)
            {
                StatGroup sg = UpdateGroup;
                sg.Name = tbGroupName.Text;
                Context.Entry(sg).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void LoadListBoxContent()
        {
            var Context = new Db1Entities();
            var query = from c in Context.StatGroup select c;
            List<StatGroup> GroupList = query.ToList();
            lbGroups.ItemsSource = GroupList;
            lbGroups.DisplayMemberPath = "Name";
        }

        private void lbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbGroups.SelectedItem != null)
            {
                tbGroupName.Text = (lbGroups.SelectedItem as StatGroup).Name;
                tbGroupName1.Text = tbGroupName.Text;
                btAdd.Visibility = Visibility.Hidden;
                btUpd.Visibility = Visibility.Visible;
                btAdd.IsEnabled = false;
                btDel.IsEnabled = true;
                btUpd.IsEnabled = true;
                tiSubGroups.IsEnabled = true;
            }
            else
            {
                tbGroupName.Text = "";
                tbGroupName1.Text = "";
                btAdd.Visibility = Visibility.Visible;
                btUpd.Visibility = Visibility.Hidden;
                btAdd.IsEnabled = true;
                btDel.IsEnabled = false;
                btUpd.IsEnabled = false;
                tiSubGroups.IsEnabled = false;
            }
        }

        //SubGroups

        private void lbGroups1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSubGroups.SelectedItem != null)
            {
                tbSubGroupName.Text = (lbSubGroups.SelectedItem as StatSubGroup).Name;
                btAdd1.Visibility = Visibility.Hidden;
                btUpd1.Visibility = Visibility.Visible;
                btAdd1.IsEnabled = false;
                btDel1.IsEnabled = true;
                btUpd1.IsEnabled = true;
            }
            else
            {
                tbSubGroupName.Text = "";                
                btAdd1.Visibility = Visibility.Visible;
                btUpd1.Visibility = Visibility.Hidden;
                btAdd1.IsEnabled = true;
                btDel1.IsEnabled = false;
                btUpd1.IsEnabled = false;
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                StatSubGroup ssg = new StatSubGroup();
                ssg.Name = tbSubGroupName.Text;
                ssg.GroupId = (lbGroups.SelectedItem as StatGroup).Id;
                Context.StatSubGroup.Add(ssg);
                Context.SaveChanges();
            }
            LoadSubListBoxContent();
            tbSubGroupName.Text = "";
        }

        private void btDel1_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Db1Entities();
            StatGroup Group = (lbGroups.SelectedItem as StatGroup);
            var Query = from c in Context.StatSubGroup where Group.Id == c.Id select c;
            StatSubGroup DelSubGroup = Query.FirstOrDefault();

            using (Context)
            {
                StatSubGroup ssg = DelSubGroup;
                Context.Entry(ssg).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            LoadSubListBoxContent();
        }

        private void btUpd1_Click(object sender, RoutedEventArgs e)
        {
            var Context = new Db1Entities();
            StatSubGroup UpdateSubGroup = (lbSubGroups.SelectedItem as StatSubGroup);
            var Query = from c in Context.StatSubGroup where UpdateSubGroup.Id == c.Id select c;
            UpdateSubGroup = Query.FirstOrDefault();

            using (Context)
            {
                StatSubGroup ssg = UpdateSubGroup;
                ssg.Name = tbSubGroupName.Text;
                Context.Entry(ssg).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void LoadSubListBoxContent()
        {
            var Context = new Db1Entities();
            var query = from c in Context.StatSubGroup select c;
            List<StatSubGroup> SubGroupList = query.ToList();
            lbSubGroups.ItemsSource = SubGroupList;
            lbSubGroups.DisplayMemberPath = "Name";
        }
    }
}
