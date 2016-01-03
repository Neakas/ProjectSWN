using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for AddDisadvantage.xaml
    /// </summary>
    public partial class ManageDisadvantage : Window
    {
        public static ManageDisadvantage AdvWindow;
        public ManageDisadvantage()
        {
            AdvWindow = this;
            InitializeComponent();
            LoadStackPanelContent();
            FillCBBonus();
            FillCBMalus();
        }

        public void FillCBBonus()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.CharacterBonus select c;
            var CharacterBoni = query.ToList();
            context.Dispose();
            cbBoni.ItemsSource = CharacterBoni;
            cbBoni.DisplayMemberPath = "BonusName";
            cbBoni.SelectedValuePath = "Id";
        }

        public void FillCBMalus()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.CharacterMalus select c;
            var CharacterMali = query.ToList();
            context.Dispose();
            cbMali.ItemsSource = CharacterMali;
            cbMali.DisplayMemberPath = "MalusName";
            cbMali.SelectedValuePath = "Id";
        }


        private void btBonusAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoni.SelectedItem == null)
            {
                MessageBox.Show("Please select a Bonus");
            }
            else
            {
                lbBonuses.Items.Add(cbBoni.SelectedItem);
                lbBonuses.DisplayMemberPath = "BonusName";
                lbBonuses.SelectedValuePath = "Id";
            }
        }

        private void btBonusDel_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoni.SelectedItem == null)
            {
                MessageBox.Show("Please select a Bonus");
            }
            else
            {
                foreach (Utility.CharacterBonus CBonus in lbBonuses.Items)
                {
                    if (CBonus.BonusName == cbBoni.Text)
                    {
                        lbBonuses.Items.Remove(CBonus);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No Bonus with that Name could be found");
                    }
                }
            }
        }

        private void btMaliAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbMali.SelectedItem == null)
            {
                MessageBox.Show("Please select a Malus");
            }
            else
            {
                lbMali.Items.Add(cbMali.SelectedItem);
                lbMali.DisplayMemberPath = "MalusName";
                lbMali.SelectedValuePath = "Id";
            }
        }

        private void btMaliDel_Click(object sender, RoutedEventArgs e)
        {
            if (cbMali.SelectedItem == null)
            {
                MessageBox.Show("Please select a Malus");
            }
            else
            {
                foreach (Utility.CharacterMalus CMalus in lbMali.Items)
                {
                    if (CMalus.MalusName == cbMali.Text)
                    {
                        lbBonuses.Items.Remove(CMalus);
                    }
                    else
                    {
                        MessageBox.Show("No Malus with that Name could be found");
                    }
                }
            }
        }

        private void btAddDisadvantage_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var query = from c in findcontext.Disadvantages where c.Name == tbName.Text select c;
            var foundstat = query.FirstOrDefault();
            findcontext.Dispose();
            if (foundstat == null)
            {
                if (tbName.Text == "")
                {
                    MessageBox.Show("Please input a Name for the Disadvantage!");
                }
                else
                {
                    using (var newcontext = new Utility.Db1Entities())
                    {
                        try
                        {
                            Utility.Disadvantages newDisadvantage = new Utility.Disadvantages();
                            newDisadvantage.Name = tbName.Text;
                            newDisadvantage.Discription = tbDiscription.Text;
                            newDisadvantage.isEnabled = cbisEnabled.IsEnabled;
                            newDisadvantage.PointCost = Convert.ToInt32(tbPointCost.Text);
                            foreach (Utility.CharacterBonus charbonus in lbBonuses.Items)
                            {
                                Utility.UsedBonus newUsedBonus = new Utility.UsedBonus();
                                newUsedBonus.BonusId = charbonus.Id;
                                newDisadvantage.UsedBonus.Add(newUsedBonus);
                            }
                            foreach (Utility.CharacterMalus charmalus in lbMali.Items)
                            {
                                Utility.UsedMalus newUsedMalus = new Utility.UsedMalus();
                                newUsedMalus.MalusId = charmalus.Id;
                                newDisadvantage.UsedMalus.Add(newUsedMalus);
                            }
                            newcontext.Disadvantages.Add(newDisadvantage);
                            newcontext.SaveChanges();
                        }
                        catch (DbEntityValidationException t)
                        {
                            MessageBox.Show(t.ToString());
                        }
                        
                    }
                    MessageBox.Show("'" + tbName.Text + "' added to the Database");
                    tbName.Text = "";
                }
            }
            else
            {
                MessageBox.Show("That Disadvantage allready exists in the Database!");
            }
            LoadStackPanelContent();
        }

        public void LoadStackPanelContent()
        {
            sP1.Children.Clear();
            var context = new Utility.Db1Entities();
            var query = from c in context.Disadvantages select c;
            var advlist = query.ToList();

            foreach (Utility.Disadvantages adv in advlist)
            {
                Expander ex = new Expander();
                UserControls.DisadvantageControl AC = new UserControls.DisadvantageControl();
                AC.InitControl(adv);
                Viewbox vb1 = new Viewbox();
                sP1.Children.Add(ex);
                ex.Content = vb1;
                ex.Header = adv.Name;
                vb1.Child = AC;
                vb1.Height = AC.Height;
            }
        }

        public void DeleteDisadvantage(UserControls.DisadvantageControl advControl)
        {
            using (var context = new Utility.Db1Entities())
            {
                var itemToRemove = context.Disadvantages.SingleOrDefault(x => x.Id == advControl.DisadvantageId); //returns a single item.

                if (itemToRemove != null)
                {
                    context.Disadvantages.Remove(itemToRemove);
                    context.SaveChanges();
                }
            }
            MessageBox.Show("Deleted!");
            LoadStackPanelContent();
        }

    }
}
