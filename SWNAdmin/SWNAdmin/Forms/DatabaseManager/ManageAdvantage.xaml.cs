using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.UserControls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for AddAdvantage.xaml
    /// </summary>
    public partial class ManageAdvantage
    {
        public static ManageAdvantage AdvWindow;
        private readonly Dictionary<Modifier, UsedModifier> _dictMod = new Dictionary<Modifier, UsedModifier>();
        private readonly List<string> _operator = new List<string> {"+", "-", "="};
        private int _updateId;

        public ManageAdvantage()
        {
            AdvWindow = this;
            InitializeComponent();
            cbModOp.ItemsSource = _operator;
            LoadStackPanelContent();
            LoadModifierComboBox();
        }

        private void btAddAdvantage_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Db1Entities();
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
                    using (var newcontext = new Db1Entities())
                    {
                        var adv = new Advantages
                        {
                            isEnabled = cbisEnabled.IsChecked != null && (bool) cbisEnabled.IsChecked,
                            isPhysical = cbisPhysical.IsChecked != null && (bool) cbisPhysical.IsChecked,
                            isSocial = cbisSocial.IsChecked != null && (bool) cbisSocial.IsChecked,
                            isSuperNatural = cbisSuperNatural.IsChecked != null && (bool) cbisSuperNatural.IsChecked,
                            isExotic = cbisExotic.IsChecked != null && (bool) cbisExotic.IsChecked,
                            isMundane = cbisMundane.IsChecked != null && (bool) cbisMundane.IsChecked,
                            isMental = cbisMental.IsChecked != null && (bool) cbisMental.IsChecked,
                            Limitation = tbLimitation.Text,
                            Discription = tbDiscription.Text,
                            PointCost = Convert.ToInt32(tbPointCost.Text),
                            hasLevels = cbhasLevels.IsChecked != null && (bool) cbhasLevels.IsChecked,
                            Name = tbName.Text,
                            isCreationLocked =
                                cbisCreationLocked.IsChecked != null && (bool) cbisCreationLocked.IsChecked,
                            Reference = tbReference.Text
                        };
                        newcontext.Advantages.Add(adv);
                        newcontext.SaveChanges();
                        foreach (Modifier item in lbModifier.Items)
                        {
                            UsedModifier usedmod;
                            _dictMod.TryGetValue(item, out usedmod);
                            var firstOrDefault =
                                (from c in newcontext.Advantages where c.Name == adv.Name select c).FirstOrDefault();
                            if (firstOrDefault != null) if (usedmod != null) usedmod.ForeignId = firstOrDefault.Id;
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

        private void LoadStackPanelContent()
        {
            sP1.Children.Clear();
            var context = new Db1Entities();
            var query = from c in context.Advantages select c;
            var advlist = query.ToList().OrderBy(advantages => advantages.Name);

            foreach (var adv in advlist)
            {
                var ex = new Expander();
                var ac = new AdvantageControl();
                ac.InitControl(adv);
                var vb1 = new Viewbox();
                sP1.Children.Add(ex);
                ex.Content = vb1;
                ex.Header = adv.Name;
                vb1.Child = ac;
                vb1.Height = ac.Height;
            }
        }

        public void DeleteAdvantage(AdvantageControl advControl)
        {
            using (var context = new Db1Entities())
            {
                var itemToRemove = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId);
                //returns a single item.
                var moddelist = (from c in context.UsedModifier where c.ForeignId == itemToRemove.Id select c).ToList();

                if (itemToRemove != null)
                {
                    context.Entry(itemToRemove).State = EntityState.Deleted;
                    foreach (var item in moddelist)
                    {
                        context.Entry(item).State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                }
            }
            MessageBox.Show("Deleted!");
            LoadStackPanelContent();
            ClearControls();
        }

        public void UpdateAdvantage(AdvantageControl advControl)
        {
            //Reads out of the Database to Edit
            lbModifier.Items.Clear();
            btAddAdvantage.IsEnabled = false;
            btEditAdvantage.IsEnabled = true;
            using (var context = new Db1Entities())
            {
                var loadItem = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId);
                //returns a single item.
                if (loadItem != null)
                {
                    _updateId = loadItem.Id;
                    tbName.Text = loadItem.Name;
                    tbDiscription.Text = loadItem.Discription;
                    cbisEnabled.IsChecked = loadItem.isEnabled;
                    tbPointCost.Text = loadItem.PointCost.ToString();
                    cbisPhysical.IsChecked = loadItem.isPhysical;
                    cbisMental.IsChecked = loadItem.isMental;
                    cbisSocial.IsChecked = loadItem.isSocial;
                    cbisExotic.IsChecked = loadItem.isExotic;
                    cbisSuperNatural.IsChecked = loadItem.isSuperNatural;
                    cbisMundane.IsChecked = loadItem.isMundane;
                    cbhasLevels.IsChecked = loadItem.hasLevels != null && (bool) loadItem.hasLevels;
                    cbisCreationLocked.IsChecked = loadItem.isCreationLocked != null && (bool) loadItem.isCreationLocked;
                    tbReference.Text = loadItem.Reference;
                    tbLimitation.Text = loadItem.Limitation;
                }
                var usedModifers = (from c in context.UsedModifier where c.ForeignId == loadItem.Id select c).ToList();
                foreach (var umod in usedModifers)
                {
                    var mod = (from c in context.Modifier where c.Id == umod.ModifierId select c).FirstOrDefault();
                    lbModifier.Items.Add(mod);
                    lbModifier.DisplayMemberPath = "Name";
                    if (mod != null) _dictMod.Add(mod, umod);
                }
                CheckRequirement();
            }
        }

        private void btEditAdvantage_Click(object sender, RoutedEventArgs e)
        {
            //Writes the Update back to the Database
            var context = new Db1Entities();
            var editAdv = (from c in context.Advantages where c.Name == tbName.Text select c).FirstOrDefault();
            using (context)
            {
                if (editAdv != null)
                {
                    editAdv.Discription = tbDiscription.Text;
                    if (cbisEnabled.IsChecked != null)
                    {
                        editAdv.isEnabled = (bool) cbisEnabled.IsChecked;
                        editAdv.isExotic = cbisExotic.IsChecked != null && (bool) cbisExotic.IsChecked;
                        editAdv.isMental = cbisMental.IsChecked != null && (bool) cbisMental.IsChecked;
                        editAdv.isMundane = cbisMundane.IsChecked != null && (bool) cbisMundane.IsChecked;
                        editAdv.isPhysical = cbisPhysical.IsChecked != null && (bool) cbisPhysical.IsChecked;
                        editAdv.isSocial = cbisSocial.IsChecked != null && (bool) cbisSocial.IsChecked;
                        editAdv.isSuperNatural = cbisSuperNatural.IsChecked != null && (bool) cbisSuperNatural.IsChecked;
                        editAdv.Limitation = tbLimitation.Text;
                        editAdv.Name = tbName.Text;
                        editAdv.hasLevels = cbhasLevels.IsChecked != null && (bool) cbhasLevels.IsChecked;
                        editAdv.PointCost = int.Parse(tbPointCost.Text);
                        editAdv.isCreationLocked = cbisCreationLocked.IsChecked != null && (bool) cbisCreationLocked.IsChecked;
                    }
                    editAdv.Reference = tbReference.Text;
                    context.Entry(editAdv).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            btAddAdvantage.IsEnabled = true;
            btEditAdvantage.IsEnabled = false;
            LoadStackPanelContent();
            ClearControls();
        }

        private void btOpenModifers_Click(object sender, RoutedEventArgs e)
        {
            var mm = new ManageModifiers();
            mm.ShowDialog();
            LoadModifierComboBox();
        }

        private void cbModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbModifier.SelectedItem != null)
            {
                var modifier = cbModifier.SelectedItem as Modifier;
                if (modifier != null)
                    cbModifier.ToolTip = modifier.Description;
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
            var advantagequery = (from c in context.Advantages where c.Name == tbName.Text select c).FirstOrDefault();
            var reqquery =
                (from c in context.Requirements where c.SourceItemID == advantagequery.Id select c).FirstOrDefault();
            if (reqquery == null) return;
            rbRegSet.IsChecked = reqquery.SourceItemID == _updateId;
        }

        private void btModifierAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbModOp.SelectedItem == null)
            {
                MessageBox.Show("Bitte einen _operator auswählen!");
            }
            else
            {
                if (tbModVal.Text == "")
                {
                    MessageBox.Show("Bitte einen Value eingeben");
                }
                else
                {
                    var addItem = cbModifier.SelectedItem as Modifier;
                    if (addItem != null)
                    {
                        var usedMod = new UsedModifier
                        {
                            Operator = cbModOp.Text,
                            Value = int.Parse(tbModVal.Text),
                            ModifierId = addItem.Id
                        };
                        lbModifier.Items.Add(addItem);
                        lbModifier.DisplayMemberPath = "Name";
                        _dictMod.Add(addItem, usedMod);
                    }
                    cbModOp.SelectedValue = null;
                    tbModVal.Text = "";
                }
            }
        }

        private void btModifierDel_Click(object sender, RoutedEventArgs e)
        {
            if (lbModifier.SelectedItem == null) return;
            lbModifier.Items.Remove(lbModifier.SelectedItem);
            if (_dictMod.ContainsKey((Modifier) lbModifier.SelectedItem))
                _dictMod.Remove((Modifier) lbModifier.SelectedItem);
        }

        private void btModifierUpd_Click(object sender, RoutedEventArgs e)
        {
            if (lbModifier.SelectedItem != null)
            {
                using (var context = new Db1Entities())
                {
                    _dictMod[(Modifier) lbModifier.SelectedItem].Value = int.Parse(tbModVal.Text);
                    _dictMod[(Modifier) lbModifier.SelectedItem].Operator = cbModOp.Text;
                    UsedModifier foundmod;
                    _dictMod.TryGetValue((Modifier) lbModifier.SelectedItem, out foundmod);
                    var usedmod = (from c in context.UsedModifier where c.Id == foundmod.Id select c).FirstOrDefault();
                    if (usedmod != null)
                    {
                        if (foundmod != null)
                        {
                            usedmod.Operator = foundmod.Operator;
                            usedmod.Value = foundmod.Value;
                        }
                        context.Entry(usedmod).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
                lbModifier.SelectedItem = null;
                cbModOp.Text = "";
                tbModVal.Text = "";
            }
        }

        private void LoadModifierComboBox()
        {
            var context = new Db1Entities();
            cbModifier.ItemsSource = (from c in context.Modifier select c).ToList().OrderBy(modifier => modifier.Name);
            cbModifier.DisplayMemberPath = "Name";
        }

        private void lbModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbModifier.SelectedItem != null)
            {
                btModifierAdd.IsEnabled = false;
                btModifierAdd.Visibility = Visibility.Hidden;
                btModifierUpd.Visibility = Visibility.Visible;
                btModifierUpd.IsEnabled = true;
                btModifierDel.IsEnabled = true;
                foreach (Modifier item in cbModifier.Items)
                {
                    var modifier = lbModifier.SelectedItem as Modifier;
                    if (modifier != null && item.Name == modifier.Name)
                    {
                        cbModifier.SelectedItem = item;
                    }
                }
                UsedModifier usedmod;
                _dictMod.TryGetValue((Modifier) lbModifier.SelectedItem, out usedmod);
                cbModOp.IsEnabled = true;
                tbModVal.IsEnabled = true;
                if (usedmod == null) return;
                cbModOp.Text = usedmod.Operator;
                tbModVal.Text = usedmod.Value.ToString();
            }
            else
            {
                cbModOp.IsEnabled = false;
                tbModVal.IsEnabled = false;
                btModifierAdd.IsEnabled = true;
                btModifierDel.IsEnabled = false;
                btModifierAdd.Visibility = Visibility.Visible;
                btModifierUpd.Visibility = Visibility.Hidden;
                btModifierUpd.IsEnabled = false;
            }
        }

        private void ClearControls()
        {
            tbDiscription.Text = "";
            tbLimitation.Text = "";
            tbName.Text = "";
            tbPointCost.Text = "0";
            cbisEnabled.IsChecked = true;
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
            cbhasLevels.IsChecked = false;
            cbisCreationLocked.IsChecked = false;
            tbReference.Text = "";
            _dictMod.Clear();
        }
    }
}