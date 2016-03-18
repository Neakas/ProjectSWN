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
using SWNAdmin.UI.ViewModels;
using System.ComponentModel;

namespace SWNAdmin.UI
{
    /// <summary>
    /// Interaction logic for MainEncyclopedia.xaml
    /// </summary>
    public partial class MainEncyclopedia : Window
    {
        Window NewEncyclopediaWindow;
        Encyclopedia LoadedEncyclopedia;
        SettingsViewModel settings;
        TextRange LoadedRange;
        string LoadedItem;
        string LoadedParent;

        public MainEncyclopedia()
        {
            InitializeComponent();
            LoadEncyclopedia();
            var n1 = new TreeNodeViewModel("General");
            n1.PropertyChanged += UpdateTextBox;

            using (var Context = new Utility.Db1Entities())
            {
                List<Utility.Aliens> raceList = (from c in Context.Aliens select c).ToList();
                List<TreeNodeViewModel> raceNodeList = new List<TreeNodeViewModel>();
                foreach (Aliens item in raceList)
                {
                    TreeNodeViewModel t1 = new TreeNodeViewModel(item.Name);
                    t1.PropertyChanged += UpdateTextBox;
                    raceNodeList.Add(t1);
                }
                TreeNodeViewModel n2 = new TreeNodeViewModel("Races", raceNodeList,null);
                n2.PropertyChanged += UpdateTextBox;
                settings = new SettingsViewModel(new[] { n1, n2 });

                DataContext = settings;
            }
        }

        private void UpdateTextBox(object sender, PropertyChangedEventArgs e)
        {
            LoadedRange = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd);
            
            if ((sender as TreeNodeViewModel)?.Parent?.Name == "Races")
            {
                using (var Context = new Db1Entities())
                {
                    string Name = (sender as TreeNodeViewModel).Name;
                    encycloRace Race = (from c in Context.encycloRace where c.RaceName == Name select c).FirstOrDefault();
                    LoadedItem = Race.RaceName;
                    LoadedParent = (sender as TreeNodeViewModel)?.Parent?.Name;
                    rtbContent.TextChanged -= RichTextBox_TextChanged;
                    LoadedRange.Text = Race.RaceDiscription;
                    rtbContent.TextChanged += RichTextBox_TextChanged;
                }
            }
            btSave.IsEnabled = false;
        }

        private void LoadEncyclopedia()
        {
            using (var Context = new Db1Entities())
            {
                LoadedEncyclopedia = (from c in Context.Encyclopedia select c).FirstOrDefault();
                RefreshEncyclopedia(LoadedEncyclopedia);
            }
            //Type t = typeof(encycloPerson);
            //Type z = typeof(encycloFactions);
            //List < Type> typelist = new List<Type>();
            //typelist.Add(t);
            //typelist.Add(z);
            //ccMainCollection.NewItemTypes = typelist;
        }

        private void subMenuNewEncy_Click(object sender, RoutedEventArgs e)
        {
            NewEncyclopediaWindow = new Window();
            NewEncyclopediaWindow.Title = "Input Encyclopedia Name";
            NewEncyclopediaWindow.Width = 300;
            NewEncyclopediaWindow.Height = 80;

            Label newLabel = new Label();
            newLabel.VerticalAlignment = VerticalAlignment.Top;
            newLabel.HorizontalAlignment = HorizontalAlignment.Left;
            newLabel.Content = "Encyclopedia Name:";
            newLabel.Margin = new Thickness(10, 10, 0, 0);
            TextBox newTextbox = new TextBox();
            newTextbox.HorizontalAlignment = HorizontalAlignment.Left;
            newTextbox.VerticalAlignment = VerticalAlignment.Top;
            newTextbox.Height = 23;
            newTextbox.Width = 150;
            newTextbox.TextWrapping = TextWrapping.Wrap;
            newTextbox.Margin = new Thickness(132, 14, 0, 0);
            newTextbox.KeyDown += newTextbox_KeyDown;
            Grid newGrid = new Grid();
            newGrid.Children.Add(newLabel);
            newGrid.Children.Add(newTextbox);
            NewEncyclopediaWindow.Content = newGrid;
            NewEncyclopediaWindow.ShowDialog();
        }

        private void newTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var Context = new Db1Entities())
                {
                    Encyclopedia encyclopedia = new Encyclopedia();
                    encyclopedia.Name = (sender as TextBox).Text;
                    Context.Encyclopedia.Add(encyclopedia);
                    Context.SaveChanges();
                    MessageBox.Show("The Encyclopedia '" + (sender as TextBox).Text + "' has been added to the Database");
                }
                (((sender as TextBox).Parent as Grid).Parent as Window).Close();
            }
        }

        private void RefreshEncyclopedia(Encyclopedia enc)
        {
            using (var Context = new Db1Entities())
            {
                //Races
                List<Aliens> alienlist = (from c in Context.Aliens select c).ToList();
                List<encycloRace> encyclorace = (from c in Context.encycloRace select c).ToList();

                List<Aliens> result = alienlist.Where(p => !encyclorace.Any(p2 => p2.RaceName == p.Name)).ToList();
                foreach (Aliens item in result)
                {
                    encycloRace InsertRace = new encycloRace();
                    InsertRace.EncyclopediaId = enc.Id;
                    InsertRace.RaceName = item.Name;
                    InsertRace.RaceDiscription = "Fill Me!!!";
                    Context.encycloRace.Add(InsertRace);
                    Context.SaveChanges();
                }
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (LoadedParent == "Races")
            {
                using (var Context = new Db1Entities())
                {
                    encycloRace UpdateRace = (from c in Context.encycloRace where c.RaceName == LoadedItem select c).FirstOrDefault();
                    UpdateRace.RaceDiscription = LoadedRange.Text;
                    Context.Entry(UpdateRace).State = System.Data.Entity.EntityState.Modified;
                    Context.SaveChanges();
                }
            }
            btSave.IsEnabled = false;
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            btSave.IsEnabled = true;
        }
    }
}
