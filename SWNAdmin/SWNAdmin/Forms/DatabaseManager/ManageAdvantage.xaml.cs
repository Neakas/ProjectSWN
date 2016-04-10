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

        private readonly List<string> _operator = new List<string>
        {
            "+",
            "-",
            "="
        };

        private int _updateId;

        public ManageAdvantage()
        {
            AdvWindow = this;
            InitializeComponent();
            CbModOp.ItemsSource = _operator;
            LoadStackPanelContent();
            LoadModifierComboBox();
        }

        private void btAddAdvantage_Click( object sender, RoutedEventArgs e )
        {
            var findcontext = new Db1Entities();
            var foundstat = ( from c in findcontext.Advantages where c.Name == TbName.Text select c ).FirstOrDefault();
            findcontext.Dispose();
            if (foundstat == null)
            {
                if (TbName.Text == "")
                {
                    MessageBox.Show("Please input a Name for the Advantage!");
                }
                else
                {
                    using (var newcontext = new Db1Entities())
                    {
                        var adv = new Advantages
                        {
                            isEnabled = CbisEnabled.IsChecked != null && (bool) CbisEnabled.IsChecked,
                            isPhysical = CbisPhysical.IsChecked != null && (bool) CbisPhysical.IsChecked,
                            isSocial = CbisSocial.IsChecked != null && (bool) CbisSocial.IsChecked,
                            isSuperNatural = CbisSuperNatural.IsChecked != null && (bool) CbisSuperNatural.IsChecked,
                            isExotic = CbisExotic.IsChecked != null && (bool) CbisExotic.IsChecked,
                            isMundane = CbisMundane.IsChecked != null && (bool) CbisMundane.IsChecked,
                            isMental = CbisMental.IsChecked != null && (bool) CbisMental.IsChecked,
                            Limitation = TbLimitation.Text,
                            Discription = TbDiscription.Text,
                            PointCost = Convert.ToInt32(TbPointCost.Text),
                            hasLevels = CbhasLevels.IsChecked != null && (bool) CbhasLevels.IsChecked,
                            Name = TbName.Text,
                            isCreationLocked = CbisCreationLocked.IsChecked != null && (bool) CbisCreationLocked.IsChecked,
                            Reference = TbReference.Text
                        };
                        newcontext.Advantages.Add(adv);
                        newcontext.SaveChanges();
                        foreach (Modifier item in LbModifier.Items)
                        {
                            UsedModifier usedmod;
                            _dictMod.TryGetValue(item, out usedmod);
                            var firstOrDefault = ( from c in newcontext.Advantages where c.Name == adv.Name select c ).FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                if (usedmod != null)
                                {
                                    usedmod.ForeignId = firstOrDefault.Id;
                                }
                            }
                            newcontext.UsedModifier.Add(usedmod);
                            newcontext.SaveChanges();
                        }
                    }
                    MessageBox.Show("'" + TbName.Text + "' added to the Database");
                    TbName.Text = "";
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
            Sp1.Children.Clear();
            var context = new Db1Entities();
            var query = from c in context.Advantages select c;
            var advlist = query.ToList().OrderBy(advantages => advantages.Name);

            foreach (var adv in advlist)
            {
                var ex = new Expander();
                var ac = new AdvantageControl();
                ac.InitControl(adv);
                var vb1 = new Viewbox();
                Sp1.Children.Add(ex);
                ex.Content = vb1;
                ex.Header = adv.Name;
                vb1.Child = ac;
                vb1.Height = ac.Height;
            }
        }

        public void DeleteAdvantage( AdvantageControl advControl )
        {
            using (var context = new Db1Entities())
            {
                var itemToRemove = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId);
                //returns a single item.
                var moddelist = ( from c in context.UsedModifier where c.ForeignId == itemToRemove.Id select c ).ToList();

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

        public void UpdateAdvantage( AdvantageControl advControl )
        {
            //Reads out of the Database to Edit
            LbModifier.Items.Clear();
            BtAddAdvantage.IsEnabled = false;
            BtEditAdvantage.IsEnabled = true;
            using (var context = new Db1Entities())
            {
                var loadItem = context.Advantages.SingleOrDefault(x => x.Id == advControl.AdvantageId);
                //returns a single item.
                if (loadItem != null)
                {
                    _updateId = loadItem.Id;
                    TbName.Text = loadItem.Name;
                    TbDiscription.Text = loadItem.Discription;
                    CbisEnabled.IsChecked = loadItem.isEnabled;
                    TbPointCost.Text = loadItem.PointCost.ToString();
                    CbisPhysical.IsChecked = loadItem.isPhysical;
                    CbisMental.IsChecked = loadItem.isMental;
                    CbisSocial.IsChecked = loadItem.isSocial;
                    CbisExotic.IsChecked = loadItem.isExotic;
                    CbisSuperNatural.IsChecked = loadItem.isSuperNatural;
                    CbisMundane.IsChecked = loadItem.isMundane;
                    CbhasLevels.IsChecked = loadItem.hasLevels != null && (bool) loadItem.hasLevels;
                    CbisCreationLocked.IsChecked = loadItem.isCreationLocked != null && (bool) loadItem.isCreationLocked;
                    TbReference.Text = loadItem.Reference;
                    TbLimitation.Text = loadItem.Limitation;
                }
                var usedModifers = ( from c in context.UsedModifier where c.ForeignId == loadItem.Id select c ).ToList();
                foreach (var umod in usedModifers)
                {
                    var mod = ( from c in context.Modifier where c.Id == umod.ModifierId select c ).FirstOrDefault();
                    LbModifier.Items.Add(mod);
                    LbModifier.DisplayMemberPath = "Name";
                    if (mod != null)
                    {
                        _dictMod.Add(mod, umod);
                    }
                }
                CheckRequirement();
            }
        }

        private void btEditAdvantage_Click( object sender, RoutedEventArgs e )
        {
            //Writes the Update back to the Database
            var context = new Db1Entities();
            var editAdv = ( from c in context.Advantages where c.Name == TbName.Text select c ).FirstOrDefault();
            using (context)
            {
                if (editAdv != null)
                {
                    editAdv.Discription = TbDiscription.Text;
                    if (CbisEnabled.IsChecked != null)
                    {
                        editAdv.isEnabled = (bool) CbisEnabled.IsChecked;
                        editAdv.isExotic = CbisExotic.IsChecked != null && (bool) CbisExotic.IsChecked;
                        editAdv.isMental = CbisMental.IsChecked != null && (bool) CbisMental.IsChecked;
                        editAdv.isMundane = CbisMundane.IsChecked != null && (bool) CbisMundane.IsChecked;
                        editAdv.isPhysical = CbisPhysical.IsChecked != null && (bool) CbisPhysical.IsChecked;
                        editAdv.isSocial = CbisSocial.IsChecked != null && (bool) CbisSocial.IsChecked;
                        editAdv.isSuperNatural = CbisSuperNatural.IsChecked != null && (bool) CbisSuperNatural.IsChecked;
                        editAdv.Limitation = TbLimitation.Text;
                        editAdv.Name = TbName.Text;
                        editAdv.hasLevels = CbhasLevels.IsChecked != null && (bool) CbhasLevels.IsChecked;
                        editAdv.PointCost = int.Parse(TbPointCost.Text);
                        editAdv.isCreationLocked = CbisCreationLocked.IsChecked != null && (bool) CbisCreationLocked.IsChecked;
                    }
                    editAdv.Reference = TbReference.Text;
                    context.Entry(editAdv).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            BtAddAdvantage.IsEnabled = true;
            BtEditAdvantage.IsEnabled = false;
            LoadStackPanelContent();
            ClearControls();
        }

        private void btOpenModifers_Click( object sender, RoutedEventArgs e )
        {
            var mm = new ManageModifiers();
            mm.ShowDialog();
            LoadModifierComboBox();
        }

        private void cbModifier_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (CbModifier.SelectedItem != null)
            {
                var modifier = CbModifier.SelectedItem as Modifier;
                if (modifier != null)
                {
                    CbModifier.ToolTip = modifier.Description;
                }
                CbModOp.IsEnabled = true;
                TbModVal.IsEnabled = true;
            }
            else
            {
                CbModOp.IsEnabled = false;
                TbModVal.IsEnabled = false;
            }
        }

        private void CheckRequirement()
        {
            var context = new Db1Entities();
            var advantagequery = ( from c in context.Advantages where c.Name == TbName.Text select c ).FirstOrDefault();
            var reqquery = ( from c in context.Requirements where c.SourceItemID == advantagequery.Id select c ).FirstOrDefault();
            if (reqquery == null)
            {
                return;
            }
            RbRegSet.IsChecked = reqquery.SourceItemID == _updateId;
        }

        private void btModifierAdd_Click( object sender, RoutedEventArgs e )
        {
            if (CbModOp.SelectedItem == null)
            {
                MessageBox.Show("Bitte einen _operator auswählen!");
            }
            else
            {
                if (TbModVal.Text == "")
                {
                    MessageBox.Show("Bitte einen Value eingeben");
                }
                else
                {
                    var addItem = CbModifier.SelectedItem as Modifier;
                    if (addItem != null)
                    {
                        var usedMod = new UsedModifier
                        {
                            Operator = CbModOp.Text,
                            Value = int.Parse(TbModVal.Text),
                            ModifierId = addItem.Id
                        };
                        LbModifier.Items.Add(addItem);
                        LbModifier.DisplayMemberPath = "Name";
                        _dictMod.Add(addItem, usedMod);
                    }
                    CbModOp.SelectedValue = null;
                    TbModVal.Text = "";
                }
            }
        }

        private void btModifierDel_Click( object sender, RoutedEventArgs e )
        {
            if (LbModifier.SelectedItem == null)
            {
                return;
            }
            LbModifier.Items.Remove(LbModifier.SelectedItem);
            if (_dictMod.ContainsKey((Modifier) LbModifier.SelectedItem))
            {
                _dictMod.Remove((Modifier) LbModifier.SelectedItem);
            }
        }

        private void btModifierUpd_Click( object sender, RoutedEventArgs e )
        {
            if (LbModifier.SelectedItem != null)
            {
                using (var context = new Db1Entities())
                {
                    _dictMod[(Modifier) LbModifier.SelectedItem].Value = int.Parse(TbModVal.Text);
                    _dictMod[(Modifier) LbModifier.SelectedItem].Operator = CbModOp.Text;
                    UsedModifier foundmod;
                    _dictMod.TryGetValue((Modifier) LbModifier.SelectedItem, out foundmod);
                    var usedmod = ( from c in context.UsedModifier where c.Id == foundmod.Id select c ).FirstOrDefault();
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
                LbModifier.SelectedItem = null;
                CbModOp.Text = "";
                TbModVal.Text = "";
            }
        }

        private void LoadModifierComboBox()
        {
            var context = new Db1Entities();
            CbModifier.ItemsSource = ( from c in context.Modifier select c ).ToList().OrderBy(modifier => modifier.Name);
            CbModifier.DisplayMemberPath = "Name";
        }

        private void lbModifier_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if (LbModifier.SelectedItem != null)
            {
                BtModifierAdd.IsEnabled = false;
                BtModifierAdd.Visibility = Visibility.Hidden;
                BtModifierUpd.Visibility = Visibility.Visible;
                BtModifierUpd.IsEnabled = true;
                BtModifierDel.IsEnabled = true;
                foreach (Modifier item in CbModifier.Items)
                {
                    var modifier = LbModifier.SelectedItem as Modifier;
                    if (modifier != null && item.Name == modifier.Name)
                    {
                        CbModifier.SelectedItem = item;
                    }
                }
                UsedModifier usedmod;
                _dictMod.TryGetValue((Modifier) LbModifier.SelectedItem, out usedmod);
                CbModOp.IsEnabled = true;
                TbModVal.IsEnabled = true;
                if (usedmod == null)
                {
                    return;
                }
                CbModOp.Text = usedmod.Operator;
                TbModVal.Text = usedmod.Value.ToString();
            }
            else
            {
                CbModOp.IsEnabled = false;
                TbModVal.IsEnabled = false;
                BtModifierAdd.IsEnabled = true;
                BtModifierDel.IsEnabled = false;
                BtModifierAdd.Visibility = Visibility.Visible;
                BtModifierUpd.Visibility = Visibility.Hidden;
                BtModifierUpd.IsEnabled = false;
            }
        }

        private void ClearControls()
        {
            TbDiscription.Text = "";
            TbLimitation.Text = "";
            TbName.Text = "";
            TbPointCost.Text = "0";
            CbisEnabled.IsChecked = true;
            CbisExotic.IsChecked = false;
            CbisMental.IsChecked = false;
            CbisMundane.IsChecked = false;
            CbisPhysical.IsChecked = false;
            CbisSocial.IsChecked = false;
            CbisSuperNatural.IsChecked = false;
            CbModifier.SelectedItem = null;
            LbModifier.Items.Clear();
            CbModifier.SelectedItem = null;
            TbModVal.Text = "";
            RbRegSet.IsChecked = false;
            CbhasLevels.IsChecked = false;
            CbisCreationLocked.IsChecked = false;
            TbReference.Text = "";
            _dictMod.Clear();
        }
    }
}