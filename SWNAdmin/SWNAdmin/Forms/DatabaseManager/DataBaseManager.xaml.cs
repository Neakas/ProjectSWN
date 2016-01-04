﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
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

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for DataBaseManager.xaml
    /// </summary>
    public partial class DataBaseManager : Window
    {
        public static DataBaseManager DataBaseManagerWindow;
        public DataBaseManager()
        {
            DataBaseManagerWindow = this;
            InitializeComponent();
            LoadDgMain(null);
            LoadDatabaseSelector();
        }

        private void MenuManageAdvantage_Click(object sender, RoutedEventArgs e)
        {
            ManageAdvantage MA = new ManageAdvantage();
            MA.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageStat_Click(object sender, RoutedEventArgs e)
        {
            ManageStat MS = new ManageStat();
            MS.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageBoni_Click(object sender, RoutedEventArgs e)
        {
            ManageBoni MB = new ManageBoni();
            MB.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageMali_Click(object sender, RoutedEventArgs e)
        {
            ManageMalus MM = new ManageMalus();
            MM.ShowDialog();
            LoadDgMain(null);
        }

        private void MenuManageDisadvantage_Click(object sender, RoutedEventArgs e)
        {
            ManageDisadvantage MD = new ManageDisadvantage();
            MD.ShowDialog();
            LoadDgMain(null);
        }

        private void LoadDgMain(string SelectedItem)
        {
            if (SelectedItem == null)
            {
                var context = new Utility.Db1Entities();
                var query = from c in context.Advantages select c;
                var advlist = query.ToList();
                dgMain.ItemsSource = advlist;
            }
            else
            {
                if (SelectedItem == "Advantages")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Advantages select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "Disadvantages")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Disadvantages select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "CharacterBonus")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.CharacterBonus select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "CharacterMalus")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.CharacterMalus select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "Characters")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Characters select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "InnerMoonlets")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.InnerMoonlets select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "MajorMoons")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.MajorMoons select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "OuterMoonlets")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.OuterMoonlets select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "Planets")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Planets select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                    dgMain.Columns[44].Visibility = Visibility.Collapsed;
                }
                if (SelectedItem == "Registration")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Registration select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "Stars")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Stars select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                    dgMain.Columns[26].Visibility = Visibility.Collapsed;
                }
                if (SelectedItem == "StarSystems")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.StarSystems select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "Stats")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.Stats select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "UsedBonus")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.UsedBonus select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }
                if (SelectedItem == "UsedMalus")
                {
                    var context = new Utility.Db1Entities();
                    var query = from c in context.UsedMalus select c;
                    var advlist = query.ToList();
                    dgMain.ItemsSource = advlist;
                }



            }
        }



        private void LoadDatabaseSelector()
        {
            List<string> ItemList = new List<string>();
            ItemList.AddRange(new string[] { "Advantages", "Disadvantages", "CharacterBonus", "CharacterMalus","Characters","InnerMoonlets","MajorMoons","OuterMoonlets","Planets","Registration","Stars","StarSystems","Stats","UsedBonus","UsedMalus"});
            cbDatabaseSelector.ItemsSource = ItemList;
        }

        private void cbDatabaseSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDgMain(cbDatabaseSelector.SelectedItem.ToString());
        }
    }
}
