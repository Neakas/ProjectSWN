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

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for AddStat.xaml
    /// </summary>
    public partial class ManageMalus : Window
    {
        public ManageMalus()
        {
            InitializeComponent();
            FillListbox();
            FillCombobox();
        }

        private void FillListbox()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.CharacterMalus select c;
            var CharacterBoni = query.ToList();
            lbBoni.ItemsSource = CharacterBoni;
            lbBoni.DisplayMemberPath = "MalusName";
            lbBoni.SelectedValuePath = "Id";
        }

        private void FillCombobox()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Stats select c;
            var Stats = query.ToList();
            cbStats.ItemsSource = Stats;
            cbStats.DisplayMemberPath = "StatName";
            cbStats.SelectedValuePath = "Id";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var query = from c in findcontext.CharacterMalus where c.MalusName == tbMalusName.Text select c;
            var foundCharacterBoni = query.FirstOrDefault();
            if (foundCharacterBoni == null)
            {
                if (tbMalusName.Text == "")
                {
                    MessageBox.Show("Please input Malus Name!");
                }
                else
                {
                    if (tbValue.Text == "")
                    {
                        MessageBox.Show("Please input Malus Value!");
                    }
                    else
                    {
                        if (cbStats.SelectedValue == null)
                        {
                            MessageBox.Show("Please Select a Affected Stat!");
                        }
                        else
                        {
                            if (tbDiscription.Text == "")
                            {
                                MessageBox.Show("Please Insert a Discription!");
                            }
                            else
                            {
                                using (var context = new Utility.Db1Entities())
                                {
                                    Utility.CharacterMalus newCharacterMalus = new Utility.CharacterMalus();
                                    newCharacterMalus.MalusName = tbMalusName.Text;
                                    newCharacterMalus.Discription = tbDiscription.Text;
                                    //Insert Stat here
                                    newCharacterMalus.Value = Convert.ToInt32(tbValue.Text);
                                    context.CharacterMalus.Add(newCharacterMalus);
                                    context.SaveChanges();
                                }
                                MessageBox.Show("'" + tbMalusName.Text + "' added to the Database");
                                FillListbox();
                                tbMalusName.Text = "";
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("That Stat allready exists in the Database!");
            }
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.CharacterMalus where c.MalusName == tbMalusName.Text select c;
            var foundMalus = query.FirstOrDefault();
            if (tbMalusName.Text == "")
            {
                MessageBox.Show("Please input Character Malus!");
            }
            else
            {
                if (foundMalus.Id != 0)
                {
                    context.CharacterMalus.Remove(foundMalus);
                    context.SaveChanges();
                    FillListbox();
                    MessageBox.Show("'" + tbMalusName.Text + "' deleted from the Database");
                    tbMalusName.Text = "";
                }
                else
                {
                    MessageBox.Show("No CharacterMalus with that Name found in the Database");
                }
            }
        }

        private void lbStats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbBoni.SelectedItem != null)
            {
                Utility.CharacterMalus selectedMalus = new Utility.CharacterMalus();
                selectedMalus = lbBoni.SelectedItem as Utility.CharacterMalus;
                tbMalusName.Text = selectedMalus.MalusName;
            }
        }
    }
}
