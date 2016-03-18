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

namespace SWNAdmin
{
    /// <summary>
    /// Interaction logic for CreateFaction.xaml
    /// </summary>
    public partial class CreateFaction : Window
    {
        int LoadedId = 0;
        Window LoadWindow;
        ListBox LoadBox;
        Factions LoadedFaction;

        public CreateFaction()
        {
            InitializeComponent();
            FillRaceComboBox();
        }

        private void FillRaceComboBox()
        {
            using (var Context = new Utility.Db1Entities())
            {
                cbRace.ItemsSource = (from c in Context.Aliens select c).ToList();
                cbRace.DisplayMemberPath = "Name";
                cbRace.SelectedValuePath = "Id";
            }
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            cbRace.SelectedItem = null;
            cbRace.SelectedValue = null;
            tbHomeplanet.Text = "";
            iudCunning.Value = null;
            iudForce.Value = null;
            iudWealth.Value = null;
            iudIncome.Value = null;
            iudMaxHp.Value = null;

            btCreate.IsEnabled = true;
            btCreate.Visibility = Visibility.Visible;
            btUpdate.IsEnabled = false;
            btUpdate.Visibility = Visibility.Hidden;
            btDelete.IsEnabled = false;
            btDelete.Visibility = Visibility.Hidden;
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                Factions faction = new Factions();
                faction.Name = tbName.Text;
                faction.HomePlanet = tbHomeplanet.Text;
                faction.Race = cbRace.SelectedValue.ToString();
                faction.Cunning = iudCunning.Value;
                faction.Force = iudForce.Value;
                faction.Wealth = iudWealth.Value;
                faction.Income = iudIncome.Value;
                faction.MaxHp = iudMaxHp.Value;
                Context.Factions.Add(faction);
                Context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + tbName.Text + "' has been saved to the Database");
            btClear_Click(this, null);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                Factions UpdateFaction = (from c in Context.Factions where c.Id == LoadedId select c).FirstOrDefault();
                UpdateFaction.Name = tbName.Text;
                UpdateFaction.HomePlanet = tbName.Text;
                UpdateFaction.Race = cbRace.SelectedValue.ToString();
                UpdateFaction.Cunning = iudCunning.Value;
                UpdateFaction.Force = iudForce.Value;
                UpdateFaction.Wealth = iudWealth.Value;
                UpdateFaction.Income = iudIncome.Value;
                UpdateFaction.MaxHp = iudMaxHp.Value;
                Context.Entry(UpdateFaction).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + tbName.Text + "' has been Updated");
            btClear_Click(this, null);
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow = new Window();
            LoadWindow.Width = 200;
            LoadWindow.Height = 200;
            LoadBox = new ListBox();
            LoadWindow.Content = LoadBox;
            using (var Context = new Utility.Db1Entities())
            {
                LoadBox.ItemsSource = (from c in Context.Factions select c).ToList();
                LoadBox.DisplayMemberPath = "Name";
                LoadBox.MouseDoubleClick += LoadboxSelectionChanged;
            }
            LoadWindow.ShowDialog();
        }

        private void LoadboxSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (LoadBox.SelectedItem != null)
            {
                LoadedFaction = (Utility.Factions)LoadBox.SelectedItem;
                LoadWindow.Close();
                LoadFaction();
            }
        }

        private void LoadFaction()
        {
            tbName.Text = LoadedFaction.Name;
            tbHomeplanet.Text = LoadedFaction.HomePlanet;
            cbRace.SelectedValue = LoadedFaction.Race;
            iudCunning.Value = LoadedFaction.Cunning;
            iudForce.Value = LoadedFaction.Force;
            iudIncome.Value = LoadedFaction.Income;
            iudWealth.Value = LoadedFaction.Wealth;
            iudMaxHp.Value = LoadedFaction.MaxHp;
            btCreate.IsEnabled = false;
            btCreate.Visibility = Visibility.Hidden;
            btUpdate.IsEnabled = true;
            btUpdate.Visibility = Visibility.Visible;
            btDelete.IsEnabled = true;
            btDelete.Visibility = Visibility.Visible;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Db1Entities())
            {
                Factions delFaction = (from c in Context.Factions where c.Id == LoadedId select c).FirstOrDefault();
                Context.Entry(delFaction).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + tbName.Text + "' has been deleted from the Database");
            btClear_Click(this, null);
        }
    }
}
