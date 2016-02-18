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
using SWNAdmin.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for AddAdvantage.xaml
    /// </summary>
    public partial class ManageAdvantage : Window
    {
        public static ManageAdvantage AdvWindow;
        public List<string> Operator = new List<string> { "+", "-", "=" };
        public Dictionary<Modifier, UsedModifier> DictMod = new Dictionary<Modifier, UsedModifier>();
        public int UpdateID;
        public ManageAdvantage()
        {
            AdvWindow = this;
            InitializeComponent();
            cbModOp.ItemsSource = Operator;
            LoadStackPanelContent();
            LoadModifierComboBox();
        }

        private void btAddAdvantage_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var foundstat = (from c in findcontext.Advantages where c.Name == tbName.Text select c).FirstOrDefault();
            findcontext.Dispose();
            if (foundstat == null)
            {
                if (tbName.Text == "")
                {
                    MessageBox.Show("Please input a Name for the Advantage!");
                }
                else
                {
                    using (var newcontext = new Utility.Db1Entities())
                    {
                        Advantages adv = new Advantages();
                        adv.isEnabled = (bool)cbisEnabled.IsChecked;
                        adv.isPhysical = (bool)cbisPhysical.IsChecked;
                        adv.isSocial = (bool)cbisSocial.IsChecked;
                        adv.isSuperNatural = (bool)cbisSuperNatural.IsChecked;
                        adv.isExotic = (bool)cbisExotic.IsChecked;
                        adv.isMundane = (bool)cbisMundane.IsChecked;
                        adv.isMental = (bool)cbisMental.IsChecked;
                        adv.Limitation = tbLimitation.Text;
                        adv.Discription = tbDiscription.Text;
                        adv.PointCost = Int32.Parse(tbPointCost.Text);
                        adv.Name = tbName.Text;
                        newcontext.Advantages.Add(adv);
                        newcontext.SaveChanges();
                        foreach (Modifier item in lbModifier.Items)
                        {
                            UsedModifier usedmod;
                            DictMod.TryGetValue(item, out usedmod);
                            usedmod.ForeignId = (from c in newcontext.Advantages where c.Name == adv.Name select c).FirstOrDefault().Id;                            
                            newcontext.UsedModifier.Add(usedmod);
                            newcontext.SaveChanges();
                        }
                    }
                    MessageBox.Show("'" + tbName.Text + "' added to the Database");
                    tbName.Text = "";
                }
            }
            else
            {
                MessageBox.Show("That Advantage allready exists in the Database!");
            }
            LoadStackPanelContent();
            ClearControls();
        }

        public void LoadStackPanelContent()
        {
            sP1.Children.Clear();
            var context = new Utility.Db1Entities();
            var query = from c in context.Advantages select c;
            var advlist = query.ToList().OrderBy(Advantages => Advantages.Name);

            foreach (Utility.Advantages adv in advlist)
            {
                Expander ex = new Expander();
                UserControls.AdvantageControl AC = new UserControls.AdvantageControl();
                AC.InitControl(adv);
                Viewbox vb1 = new Viewbox();
                sP1.Children.Add(ex);
                ex.Content = vb1;
                ex.Header = adv.Name;
                vb1.Child = AC;
                vb1.Height = AC.Height;
            }
        }

        public void DeleteAdvantage(UserControls.AdvantageControl advControl)
        {
            
            using (var context = new Utility.Db1Entities())
            {
                var itemToRemove = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId); //returns a single item.
                List<UsedModifier> moddelist = (from c in context.UsedModifier where c.ForeignId == itemToRemove.Id select c).ToList();

                if (itemToRemove != null)
                {
                    context.Entry(itemToRemove).State = System.Data.Entity.EntityState.Deleted;
                    foreach (UsedModifier item in moddelist)
                    {
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    context.SaveChanges();
                }
            }
            MessageBox.Show("Deleted!");
            LoadStackPanelContent();
            ClearControls();
        }

        public void UpdateAdvantage(UserControls.AdvantageControl advControl)
        {
            //Reads out of the Database to Edit
            lbModifier.Items.Clear();
            btAddAdvantage.IsEnabled = false;
            btEditAdvantage.IsEnabled = true;
            using (var context = new Utility.Db1Entities())
            {
                var LoadItem = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId); //returns a single item.
                UpdateID = LoadItem.Id;
                if (LoadItem != null)
                {
                    tbName.Text = LoadItem.Name;
                    tbDiscription.Text = LoadItem.Discription;
                    cbisEnabled.IsChecked = LoadItem.isEnabled;
                    tbPointCost.Text = LoadItem.PointCost.ToString();
                    cbisPhysical.IsChecked = (Boolean)LoadItem.isPhysical;
                    cbisMental.IsChecked = (Boolean)LoadItem.isMental;
                    cbisSocial.IsChecked = (Boolean)LoadItem.isSocial;
                    cbisExotic.IsChecked = (Boolean)LoadItem.isExotic;
                    cbisSuperNatural.IsChecked = (Boolean)LoadItem.isSuperNatural;
                    cbisMundane.IsChecked = (Boolean)LoadItem.isMundane;
                    tbLimitation.Text = LoadItem.Limitation;
                }
                List<UsedModifier> UsedModifers = (from c in context.UsedModifier where c.ForeignId == LoadItem.Id select c).ToList();
                foreach (UsedModifier umod in UsedModifers)
                {
                    Modifier mod = (from c in context.Modifier where c.Id == umod.ModifierId select c).FirstOrDefault();
                    lbModifier.Items.Add(mod);
                    lbModifier.DisplayMemberPath = "Name";
                    DictMod.Add(mod,umod);
                }
                CheckRequirement();
            }
        }

        private void btEditAdvantage_Click(object sender, RoutedEventArgs e)
        {
            //Writes the Update back to the Database
            var Context = new Db1Entities();
            Advantages editAdv = (from c in Context.Advantages where c.Name == tbName.Text select c).FirstOrDefault();
            using (Context)
            {
                editAdv.Discription = tbDiscription.Text;
                editAdv.isEnabled = (bool)cbisEnabled.IsChecked;
                editAdv.isExotic = (bool)cbisExotic.IsChecked;
                editAdv.isMental = (bool)cbisMental.IsChecked;
                editAdv.isMundane = (bool)cbisMundane.IsChecked;
                editAdv.isPhysical = (bool)cbisPhysical.IsChecked;
                editAdv.isSocial = (bool)cbisSocial.IsChecked;
                editAdv.isSuperNatural = (bool)cbisSuperNatural.IsChecked;
                editAdv.Limitation = tbLimitation.Text;
                editAdv.Name = tbName.Text;
                editAdv.PointCost = Int32.Parse(tbPointCost.Text);
                Context.Entry(editAdv).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            btAddAdvantage.IsEnabled = true;
            btEditAdvantage.IsEnabled = false;
            LoadStackPanelContent();
            ClearControls();
        }

        private void btOpenModifers_Click(object sender, RoutedEventArgs e)
        {
            ManageModifiers mm = new ManageModifiers();
            mm.ShowDialog();
            LoadModifierComboBox();
        }

        private void cbModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbModifier.SelectedItem != null)
            {
                cbModifier.ToolTip = (cbModifier.SelectedItem as Modifier).Description;
                cbModOp.IsEnabled = true;
                tbModVal.IsEnabled = true;
            }
            else
            {
                cbModOp.IsEnabled = false;
                tbModVal.IsEnabled = false;
            }
        }

        private void CheckRequirement()
        {
            var context = new Db1Entities();
            Advantages Advantagequery = (from c in context.Advantages where c.Name == tbName.Text select c).FirstOrDefault();
            Requirements reqquery = (from c in context.Requirements where c.SourceItemID == Advantagequery.Id select c).FirstOrDefault();
            if (reqquery != null)
            {
                if (reqquery.SourceItemID == UpdateID)
                {
                    rbRegSet.IsChecked = true;
                }
                else
                {
                    rbRegSet.IsChecked = false;
                }
            }
        }

        private void btModifierAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbModOp.SelectedItem == null)
            {
                MessageBox.Show("Bitte einen Operator auswählen!");
            }
            else
            {
                if (tbModVal.Text == "")
                {
                    MessageBox.Show("Bitte einen Value eingeben");
                }
                else
                {
                    Modifier AddItem = (cbModifier.SelectedItem as Modifier);
                    UsedModifier UsedMod = new UsedModifier();
                    UsedMod.Operator = cbModOp.Text;
                    UsedMod.Value = Int32.Parse(tbModVal.Text);
                    UsedMod.ModifierId = AddItem.Id;
                    lbModifier.Items.Add(AddItem);
                    lbModifier.DisplayMemberPath = "Name";
                    DictMod.Add(AddItem, UsedMod);
                    cbModOp.SelectedValue = null;
                    tbModVal.Text = "";
                }
            }   
        }

        private void btModifierDel_Click(object sender, RoutedEventArgs e)
        {
            if (lbModifier.SelectedItem != null)
            {
                lbModifier.Items.Remove(lbModifier.SelectedItem);
                DictMod.Remove((lbModifier.SelectedItem) as Modifier);
            }
        }

        private void LoadModifierComboBox()
        {
            var context = new Db1Entities();
            cbModifier.ItemsSource = (from c in context.Modifier select c).ToList().OrderBy(Modifier => Modifier.Name);
            cbModifier.DisplayMemberPath = "Name";
        }

        private void lbModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbModifier.SelectedItem != null)
            {
                btModifierAdd.IsEnabled = false;
                btModifierDel.IsEnabled = true;
                cbModifier.SelectedItem = null;
                UsedModifier usedmod;
                DictMod.TryGetValue((lbModifier.SelectedItem as Modifier), out usedmod);
                cbModOp.IsEnabled = true;
                tbModVal.IsEnabled = true;
                cbModOp.Text = usedmod.Operator;
                tbModVal.Text = usedmod.Value.ToString();
            }
            else
            {
                cbModOp.IsEnabled = false;
                tbModVal.IsEnabled = false;
                btModifierAdd.IsEnabled = true;
                btModifierDel.IsEnabled = false;
            }   
        }

        private void ClearControls()
        {
            tbDiscription.Text = "";
            tbLimitation.Text = "";
            tbName.Text = "";
            tbPointCost.Text = "";
            cbisEnabled.IsChecked = false;
            cbisExotic.IsChecked = false;
            cbisMental.IsChecked = false;
            cbisMundane.IsChecked = false;
            cbisPhysical.IsChecked = false;
            cbisSocial.IsChecked = false;
            cbisSuperNatural.IsChecked = false;
            cbModifier.SelectedItem = null;
            lbModifier.Items.Clear();
            cbModifier.SelectedItem = null;
            tbModVal.Text = "";
            rbRegSet.IsChecked = false;
        }
    }
}
