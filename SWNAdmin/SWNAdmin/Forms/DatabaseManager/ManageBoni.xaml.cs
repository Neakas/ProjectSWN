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
    public partial class ManageBoni : Window
    {
        public ManageBoni()
        {
            InitializeComponent();
            FillListbox();
            FillCombobox();
        }

        private void FillListbox()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.CharacterBonus select c;
            var CharacterBoni = query.ToList();
            lbBoni.ItemsSource = CharacterBoni;
            lbBoni.DisplayMemberPath = "BonusName";
            lbBoni.SelectedValuePath = "Id";
        }

        private void FillCombobox()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Attribute select c;
            var Stats = query.ToList();
            cbStats.ItemsSource = Stats;
            cbStats.DisplayMemberPath = "StatName";
            cbStats.SelectedValuePath = "Id";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var query = from c in findcontext.CharacterBonus where c.BonusName == tbBonusName.Text select c;
            var foundCharacterBoni = query.FirstOrDefault();
            if (foundCharacterBoni == null)
            {
                if (tbBonusName.Text == "")
                {
                    MessageBox.Show("Please input Bonus Name!");
                }
                else
                {
                    if (tbValue.Text == "")
                    {
                        MessageBox.Show("Please input Bonus Value!");
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
                                    Utility.CharacterBonus newCharacterBonus = new Utility.CharacterBonus();
                                    newCharacterBonus.BonusName = tbBonusName.Text;
                                    newCharacterBonus.Discription = tbDiscription.Text;
                                    //Insert Stat here
                                    newCharacterBonus.Value = Convert.ToInt32(tbValue.Text);
                                    context.CharacterBonus.Add(newCharacterBonus);
                                    context.SaveChanges();
                                }
                                MessageBox.Show("'" + tbBonusName.Text + "' added to the Database");
                                FillListbox();
                                tbBonusName.Text = "";
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
            var query = from c in context.CharacterBonus where c.BonusName == tbBonusName.Text select c;
            var foundbonus = query.FirstOrDefault();
            if (tbBonusName.Text == "")
            {
                MessageBox.Show("Please input Character Bonus!");
            }
            else
            {
                if (foundbonus.Id != 0)
                {
                    context.CharacterBonus.Remove(foundbonus);
                    context.SaveChanges();
                    FillListbox();
                    MessageBox.Show("'" + tbBonusName.Text + "' deleted from the Database");
                    tbBonusName.Text = "";
                }
                else
                {
                    MessageBox.Show("No CharacterBonus with that Name found in the Database");
                }
            }
        }

        private void lbStats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbBoni.SelectedItem != null)
            {
                Utility.CharacterBonus selectedBonus = new Utility.CharacterBonus();
                selectedBonus = lbBoni.SelectedItem as Utility.CharacterBonus;
                tbBonusName.Text = selectedBonus.BonusName;
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
