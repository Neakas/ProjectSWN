using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.FactionManager
{
    /// <summary>
    ///     Interaction logic for CreateFaction.xaml
    /// </summary>
    public partial class CreateFaction
    {
        private const int LoadedId = 0;
        private ListBox _loadBox;
        private Factions _loadedFaction;
        private Window _loadWindow;

        public CreateFaction()
        {
            InitializeComponent();
            FillRaceComboBox();
        }

        private void FillRaceComboBox()
        {
            using (var context = new Db1Entities())
            {
                CbRace.ItemsSource = ( from c in context.Aliens select c ).ToList();
                CbRace.DisplayMemberPath = "Name";
                CbRace.SelectedValuePath = "Id";
            }
        }

        private void btClear_Click( object sender, RoutedEventArgs e )
        {
            TbName.Text = "";
            CbRace.SelectedItem = null;
            CbRace.SelectedValue = null;
            TbHomeplanet.Text = "";
            IudCunning.Value = null;
            IudForce.Value = null;
            IudWealth.Value = null;
            IudIncome.Value = null;
            IudMaxHp.Value = null;

            BtCreate.IsEnabled = true;
            BtCreate.Visibility = Visibility.Visible;
            BtUpdate.IsEnabled = false;
            BtUpdate.Visibility = Visibility.Hidden;
            BtDelete.IsEnabled = false;
            BtDelete.Visibility = Visibility.Hidden;
        }

        private void btCreate_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var faction = new Factions
                {
                    Name = TbName.Text,
                    HomePlanet = TbHomeplanet.Text,
                    Race = CbRace.SelectedValue.ToString(),
                    Cunning = IudCunning.Value,
                    Force = IudForce.Value,
                    Wealth = IudWealth.Value,
                    Income = IudIncome.Value,
                    MaxHp = IudMaxHp.Value
                };
                context.Factions.Add(faction);
                context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + TbName.Text + "' has been saved to the Database");
            btClear_Click(this, null);
        }

        private void btUpdate_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var updateFaction = ( from c in context.Factions where c.Id == LoadedId select c ).FirstOrDefault();
                if (updateFaction != null)
                {
                    updateFaction.Name = TbName.Text;
                    updateFaction.HomePlanet = TbName.Text;
                    updateFaction.Race = CbRace.SelectedValue.ToString();
                    updateFaction.Cunning = IudCunning.Value;
                    updateFaction.Force = IudForce.Value;
                    updateFaction.Wealth = IudWealth.Value;
                    updateFaction.Income = IudIncome.Value;
                    updateFaction.MaxHp = IudMaxHp.Value;
                    context.Entry(updateFaction).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + TbName.Text + "' has been Updated");
            btClear_Click(this, null);
        }

        private void btLoad_Click( object sender, RoutedEventArgs e )
        {
            _loadWindow = new Window
            {
                Width = 200,
                Height = 200
            };
            _loadBox = new ListBox();
            _loadWindow.Content = _loadBox;
            using (var context = new Db1Entities())
            {
                _loadBox.ItemsSource = ( from c in context.Factions select c ).ToList();
                _loadBox.DisplayMemberPath = "Name";
                _loadBox.MouseDoubleClick += LoadboxSelectionChanged;
            }
            _loadWindow.ShowDialog();
        }

        private void LoadboxSelectionChanged( object sender, RoutedEventArgs e )
        {
            if (_loadBox.SelectedItem == null)
            {
                return;
            }
            _loadedFaction = (Factions) _loadBox.SelectedItem;
            _loadWindow.Close();
            LoadFaction();
        }

        private void LoadFaction()
        {
            TbName.Text = _loadedFaction.Name;
            TbHomeplanet.Text = _loadedFaction.HomePlanet;
            CbRace.SelectedValue = _loadedFaction.Race;
            IudCunning.Value = _loadedFaction.Cunning;
            IudForce.Value = _loadedFaction.Force;
            IudIncome.Value = _loadedFaction.Income;
            IudWealth.Value = _loadedFaction.Wealth;
            IudMaxHp.Value = _loadedFaction.MaxHp;
            BtCreate.IsEnabled = false;
            BtCreate.Visibility = Visibility.Hidden;
            BtUpdate.IsEnabled = true;
            BtUpdate.Visibility = Visibility.Visible;
            BtDelete.IsEnabled = true;
            BtDelete.Visibility = Visibility.Visible;
        }

        private void btDelete_Click( object sender, RoutedEventArgs e )
        {
            using (var context = new Db1Entities())
            {
                var delFaction = ( from c in context.Factions where c.Id == LoadedId select c ).FirstOrDefault();
                context.Entry(delFaction).State = EntityState.Deleted;
                context.SaveChanges();
            }
            MessageBox.Show("The Faction '" + TbName.Text + "' has been deleted from the Database");
            btClear_Click(this, null);
        }
    }
}