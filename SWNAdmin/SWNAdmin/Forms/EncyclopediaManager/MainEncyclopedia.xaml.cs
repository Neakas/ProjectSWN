using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using SWNAdmin.Forms.EncyclopediaManager.ViewModels;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.EncyclopediaManager
{
    /// <summary>
    ///     Interaction logic for MainEncyclopedia.xaml
    /// </summary>
    public partial class MainEncyclopedia
    {
        private Encyclopedia _loadedEncyclopedia;
        private string _loadedItem;
        private string _loadedParent;
        private TextRange _loadedRange;
        private Window _newEncyclopediaWindow;

        public MainEncyclopedia()
        {
            InitializeComponent();
            LoadEncyclopedia();
            var n1 = new TreeNodeViewModel("General");
            n1.PropertyChanged += UpdateTextBox;

            using (var context = new Db1Entities())
            {
                var raceList = ( from c in context.Aliens select c ).ToList();
                var raceNodeList = new List<TreeNodeViewModel>();
                foreach (var t1 in raceList.Select(item => new TreeNodeViewModel(item.Name)))
                {
                    t1.PropertyChanged += UpdateTextBox;
                    raceNodeList.Add(t1);
                }
                var n2 = new TreeNodeViewModel("Races", raceNodeList, null);
                n2.PropertyChanged += UpdateTextBox;
                var settings = new SettingsViewModel(new[]
                {
                    n1, n2
                });

                DataContext = settings;
            }
        }

        private void UpdateTextBox( object sender, PropertyChangedEventArgs e )
        {
            _loadedRange = new TextRange(RtbContent.Document.ContentStart, RtbContent.Document.ContentEnd);

            if (( sender as TreeNodeViewModel )?.Parent?.Name == "Races")
            {
                using (var context = new Db1Entities())
                {
                    var name = ( (TreeNodeViewModel) sender ).Name;
                    var race = ( from c in context.encycloRace where c.RaceName == name select c ).FirstOrDefault();
                    if (race != null)
                    {
                        _loadedItem = race.RaceName;
                        _loadedParent = ( (TreeNodeViewModel) sender ).Parent?.Name;
                        RtbContent.TextChanged -= RichTextBox_TextChanged;
                        _loadedRange.Text = race.RaceDiscription;
                    }
                    RtbContent.TextChanged += RichTextBox_TextChanged;
                }
            }
            BtSave.IsEnabled = false;
        }

        private void LoadEncyclopedia()
        {
            using (var context = new Db1Entities())
            {
                _loadedEncyclopedia = ( from c in context.Encyclopedia select c ).FirstOrDefault();
                RefreshEncyclopedia(_loadedEncyclopedia);
            }
            //Type t = typeof(encycloPerson);
            //Type z = typeof(encycloFactions);
            //List < Type> typelist = new List<Type>();
            //typelist.Add(t);
            //typelist.Add(z);
            //ccMainCollection.NewItemTypes = typelist;
        }

        private void subMenuNewEncy_Click( object sender, RoutedEventArgs e )
        {
            _newEncyclopediaWindow = new Window
            {
                Title = "Input Encyclopedia Name",
                Width = 300,
                Height = 80
            };

            var newLabel = new Label
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = "Encyclopedia Name:",
                Margin = new Thickness(10, 10, 0, 0)
            };
            var newTextbox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 23,
                Width = 150,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(132, 14, 0, 0)
            };

            newTextbox.KeyDown += newTextbox_KeyDown;
            var newGrid = new Grid();
            newGrid.Children.Add(newLabel);
            newGrid.Children.Add(newTextbox);
            _newEncyclopediaWindow.Content = newGrid;
            _newEncyclopediaWindow.ShowDialog();
        }

        private static void newTextbox_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            using (var context = new Db1Entities())
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    var encyclopedia = new Encyclopedia
                    {
                        Name = textBox.Text
                    };
                    context.Encyclopedia.Add(encyclopedia);
                }
                context.SaveChanges();
                var box = sender as TextBox;
                if (box != null)
                {
                    MessageBox.Show("The Encyclopedia '" + box.Text + "' has been added to the Database");
                }
            }
            var grid = ( (TextBox) sender ).Parent as Grid;
            ( grid?.Parent as Window )?.Close();
        }

        private static void RefreshEncyclopedia( Encyclopedia enc )
        {
            using (var context = new Db1Entities())
            {
                //Races
                var alienlist = ( from c in context.Aliens select c ).ToList();
                var encyclorace = ( from c in context.encycloRace select c ).ToList();

                var result = alienlist.Where(p => encyclorace.All(p2 => p2.RaceName != p.Name)).ToList();
                foreach (var item in result)
                {
                    var insertRace = new encycloRace
                    {
                        EncyclopediaId = enc.Id,
                        RaceName = item.Name,
                        RaceDiscription = "Fill Me!!!"
                    };
                    context.encycloRace.Add(insertRace);
                    context.SaveChanges();
                }
            }
        }

        private void btSave_Click( object sender, RoutedEventArgs e )
        {
            if (_loadedParent == "Races")
            {
                using (var context = new Db1Entities())
                {
                    var updateRace = ( from c in context.encycloRace where c.RaceName == _loadedItem select c ).FirstOrDefault();
                    if (updateRace != null)
                    {
                        updateRace.RaceDiscription = _loadedRange.Text;
                        context.Entry(updateRace).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            BtSave.IsEnabled = false;
        }

        private void RichTextBox_TextChanged( object sender, EventArgs e )
        {
            BtSave.IsEnabled = true;
        }
    }
}