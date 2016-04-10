using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for ManageGroups.xaml
    /// </summary>
    public partial class ManageGroups
    {
        public ManageGroups()
        {
            InitializeComponent();
            LoadListBoxContent();
            LoadSubListBoxContent();
        }

        private void btAdd_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var sg = new StatGroup
                {
                    Name = TbGroupName.Text
                };
                context.StatGroup.Add(sg);
                context.SaveChanges();
            }
            LoadListBoxContent();
            TbGroupName.Text = "";
            TbGroupName1.Text = "";
        }

        private void btDel_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var delGroup = LbGroups.SelectedItem as StatGroup;
            var @group = delGroup;
            var query = from c in context.StatGroup where @group.Id == c.Id select c;
            delGroup = query.FirstOrDefault();

            using (context)
            {
                var sg = delGroup;
                context.Entry(sg).State = EntityState.Deleted;
                context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void btUpd_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var updateGroup = LbGroups.SelectedItem as StatGroup;
            var @group = updateGroup;
            var query = from c in context.StatGroup where @group.Id == c.Id select c;
            updateGroup = query.FirstOrDefault();

            using (context)
            {
                var sg = updateGroup;
                if (sg != null)
                {
                    sg.Name = TbGroupName.Text;
                    context.Entry(sg).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void LoadListBoxContent()
        {
            var context = new Db1Entities();
            var query = from c in context.StatGroup select c;
            var groupList = query.ToList();
            LbGroups.ItemsSource = groupList;
            LbGroups.DisplayMemberPath = "Name";
        }

        private void lbGroups_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (LbGroups.SelectedItem != null)
            {
                var statGroup = LbGroups.SelectedItem as StatGroup;
                if (statGroup != null)
                {
                    TbGroupName.Text = statGroup.Name;
                }
                TbGroupName1.Text = TbGroupName.Text;
                BtAdd.Visibility = Visibility.Hidden;
                BtUpd.Visibility = Visibility.Visible;
                BtAdd.IsEnabled = false;
                BtDel.IsEnabled = true;
                BtUpd.IsEnabled = true;
                TiSubGroups.IsEnabled = true;
            }
            else
            {
                TbGroupName.Text = "";
                TbGroupName1.Text = "";
                BtAdd.Visibility = Visibility.Visible;
                BtUpd.Visibility = Visibility.Hidden;
                BtAdd.IsEnabled = true;
                BtDel.IsEnabled = false;
                BtUpd.IsEnabled = false;
                TiSubGroups.IsEnabled = false;
            }
            LoadSubListBoxContent();
        }

        //SubGroups

        private void lbGroups1_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (LbSubGroups.SelectedItem != null)
            {
                var statSubGroup = LbSubGroups.SelectedItem as StatSubGroup;
                if (statSubGroup != null)
                {
                    TbSubGroupName.Text = statSubGroup.Name;
                }
                BtAdd1.Visibility = Visibility.Hidden;
                BtUpd1.Visibility = Visibility.Visible;
                BtAdd1.IsEnabled = false;
                BtDel1.IsEnabled = true;
                BtUpd1.IsEnabled = true;
            }
            else
            {
                TbSubGroupName.Text = "";
                BtAdd1.Visibility = Visibility.Visible;
                BtUpd1.Visibility = Visibility.Hidden;
                BtAdd1.IsEnabled = true;
                BtDel1.IsEnabled = false;
                BtUpd1.IsEnabled = false;
            }
        }

        private void btAdd1_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var statGroup = LbGroups.SelectedItem as StatGroup;
                if (statGroup != null)
                {
                    var ssg = new StatSubGroup
                    {
                        Name = TbSubGroupName.Text,
                        GroupId = statGroup.Id
                    };
                    context.StatSubGroup.Add(ssg);
                }
                context.SaveChanges();
            }
            LoadSubListBoxContent();
            TbSubGroupName.Text = "";
        }

        private void btDel1_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var groups = LbGroups.SelectedItem as StatGroup;
            var query = from c in context.StatSubGroup where groups.Id == c.GroupId select c;
            var delSubGroup = query.FirstOrDefault();

            using (context)
            {
                var ssg = delSubGroup;
                context.Entry(ssg).State = EntityState.Deleted;
                context.SaveChanges();
            }
            LoadSubListBoxContent();
        }

        private void btUpd1_Click( object sender, RoutedEventArgs e )
        {
            var context = new Db1Entities();
            var updateSubGroup = LbSubGroups.SelectedItem as StatSubGroup;
            var @group = updateSubGroup;
            var query = from c in context.StatSubGroup where @group.Id == c.Id select c;
            updateSubGroup = query.FirstOrDefault();

            using (context)
            {
                var ssg = updateSubGroup;
                if (ssg != null)
                {
                    ssg.Name = TbSubGroupName.Text;
                    context.Entry(ssg).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            LoadListBoxContent();
        }

        private void LoadSubListBoxContent()
        {
            try
            {
                var context = new Db1Entities();
                var loadedSg = LbGroups.SelectedItem as StatGroup;
                var query = from c in context.StatSubGroup where c.GroupId == loadedSg.Id select c;
                var subGroupList = query.ToList();
                LbSubGroups.ItemsSource = subGroupList;
                LbSubGroups.DisplayMemberPath = "Name";
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}