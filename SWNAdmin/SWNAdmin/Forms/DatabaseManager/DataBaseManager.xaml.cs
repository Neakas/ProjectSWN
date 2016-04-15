using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for DataBaseManager.xaml
    /// </summary>
    public partial class DataBaseManager
    {
        public DataBaseManager()
        {
            InitializeComponent();
            //LoadDgMain(null);
            LoadDatabaseSelector();
            //LoadDgMain("Advantages");
        }

        private void MenuManageAdvantage_Click( object sender, RoutedEventArgs e )
        {
            var ma = new ManageAdvantage();
            ma.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageStat_Click( object sender, RoutedEventArgs e )
        {
            var ms = new ManageStat();
            ms.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageDisadvantage_Click( object sender, RoutedEventArgs e )
        {
            LoadDgMain(null);
        }

        private void LoadDgMain( string selectedItem )
        {
            if (selectedItem == null)
            {
                var context = new Db1Entities();
                var query = from c in context.Advantages select c;
                var advlist = query.ToList();
                DgMain.ItemsSource = advlist;
                //
            }
            else
            {
                if (selectedItem == "Advantages")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Advantages select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                    DgMain.Columns[4].Visibility = Visibility.Collapsed;
                }
                if (selectedItem == "Disadvantages")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Disadvantages select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "Characters")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Character select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "InnerMoonlets")
                {
                    var context = new Db1Entities();
                    var query = from c in context.InnerMoonlets select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "MajorMoons")
                {
                    var context = new Db1Entities();
                    var query = from c in context.MajorMoons select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "OuterMoonlets")
                {
                    var context = new Db1Entities();
                    var query = from c in context.OuterMoonlets select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "Planets")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Planets select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                    DgMain.Columns[44].Visibility = Visibility.Collapsed;
                }
                if (selectedItem == "Registration")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Registration select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "Stars")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Stars select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                    DgMain.Columns[26].Visibility = Visibility.Collapsed;
                }
                if (selectedItem == "StarSystems")
                {
                    var context = new Db1Entities();
                    var query = from c in context.StarSystems select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "Stats")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Attribute select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
                if (selectedItem == "Skills")
                {
                    var context = new Db1Entities();
                    var query = from c in context.Skills select c;
                    var advlist = query.ToList();
                    DgMain.ItemsSource = advlist;
                }
            }
        }

        private void LoadDatabaseSelector()
        {
            var itemList = new List<string>();
            itemList.AddRange(new[]
            {
                "Advantages", "Disadvantages", "CharacterBonus", "CharacterMalus", "Characters", "InnerMoonlets", "MajorMoons", "OuterMoonlets", "Planets", "Registration", "Stars", "StarSystems", "Stats", "UsedBonus", "UsedMalus", "Skills"
            });
            CbDatabaseSelector.ItemsSource = itemList;
        }

        private void cbDatabaseSelector_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            LoadDgMain(CbDatabaseSelector.SelectedItem.ToString());
        }

        private void MenuManageSkills_Click( object sender, RoutedEventArgs e )
        {
            var msk = new ManageSkills();
            msk.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManagePrerequisites_Click( object sender, RoutedEventArgs e )
        {
            var mpr = new ManagePrerequisites();
            mpr.ShowDialog();
        }

        private void MenuManageGroups_Click( object sender, RoutedEventArgs e )
        {
            var mg = new ManageGroups();
            mg.ShowDialog();
        }

        private void MenuManageModifiers_Click( object sender, RoutedEventArgs e )
        {
            var mmods = new ManageModifiers();
            mmods.ShowDialog();
        }

        private void MenuManageAlienRace_Click( object sender, RoutedEventArgs e )
        {
            var mar = new ManageAlienRaces();
            mar.ShowDialog();
        }
    }
}