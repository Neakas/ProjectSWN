using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xceed.Wpf.Toolkit;

namespace SWN
{
    /// <summary>
    /// Interaction logic for Create_New_Character.xaml
    /// </summary>
    public partial class CreateNewCharacter : Window
    {
        //ToDo: Finish Me: MewCharacter
        private int Points;
        private int Strength;
        private int Dexterity;
        private int Intelligence;
        private int Health;
        private int BaseHitpoints;
        private int ModHitpoints;
        private int BaseWill;
        private int ModWill;
        private int BasePerception;
        private int ModPerception;
        private int BaseFatiguePoints;
        private int ModFatiguePoints;
        private double BaseSpeed;
        private double ModSpeed;
        private int BaseMove;
        private int ModMove;
        private Dictionary<SWNServiceReference.Advantages, int> AdvantageLevels = new Dictionary<SWNServiceReference.Advantages, int>();

        private List<SWNServiceReference.Advantages> AdvList = new List<SWNServiceReference.Advantages>();
        private List<SWNServiceReference.Requirements> reqList = new List<SWNServiceReference.Requirements>();
        private SWNServiceReference.Character character;

        public int points
        {
            get { return Points; }
            set
            {
                Points = value;
                OnPointsChange();
            }
        }

        public CreateNewCharacter(SWNServiceReference.Character c)
        {
            character = c;
            InitializeComponent();
            this.DataContext = c;
            Points = (int)c.PointTotal;
            Strength = (int)c.Strenght;
            Dexterity = (int)c.Dexterity;
            Intelligence = (int)c.Intelligence;
            Health = (int)c.Health;
            BaseHitpoints = (int)c.HitPoints;
            UpdateBaseHitpoints(Strength);
            BaseWill = (int)c.WillPower;
            UpdateBaseWill(Intelligence);
            BasePerception = (int)c.Perception;
            UpdateBasePerception(Intelligence);
            BaseFatiguePoints = (int)c.FatiguePoints;
            UpdateBaseFatiguePoints(Health);
            BaseSpeed = (double)c.BasicSpeed;
            UpdateBaseSpeed(Health, Dexterity);
            BaseMove = (int)c.BasicMove;
            UpdateMovePoints(BaseSpeed);
            SetupToolTips();
            SetupTabControls();
        }

        private void SetupToolTips()
        {
            iudStrenght.ToolTip = "Strength measures physical power" + Environment.NewLine +
                                "and bulk. It is crucial if you are a" + Environment.NewLine +
                                "warrior in a primitive world, as high" + Environment.NewLine +
                                "ST lets you dish out and absorb more" + Environment.NewLine +
                                "damage in hand-to-hand combat. Any" + Environment.NewLine +
                                "adventurer will find ST useful for" + Environment.NewLine +
                                "lifting and throwing things, moving" + Environment.NewLine +
                                "quickly with a load, etc. ST directly" + Environment.NewLine +
                                "determines Basic Lift, basic" + Environment.NewLine +
                                "damage, and Hit Points," + Environment.NewLine +
                                "and affects your character’s Build";

            iudDexterity.ToolTip = "Dexterity measures a combination" + Environment.NewLine +
                                "of agility, coordination, and fine" + Environment.NewLine +
                                "motor ability. It controls your basic" + Environment.NewLine +
                                "ability at most athletic, fighting, and" + Environment.NewLine +
                                "vehicle-operation skills, and at craft" + Environment.NewLine +
                                "skills that call for a delicate touch. DX" + Environment.NewLine +
                                "also helps determine Basic Speed (a" + Environment.NewLine +
                                "measure of reaction time) and" + Environment.NewLine +
                                "Basic Move.";

            iudIntelligence.ToolTip = "Intelligence broadly measures" + Environment.NewLine +
                                "brainpower, including creativity, intuition," + Environment.NewLine +
                                "memory, perception, reason," + Environment.NewLine +
                                "sanity, and willpower. It rules your" + Environment.NewLine +
                                "basic ability with all “mental” skills –" + Environment.NewLine +
                                "sciences, social interaction, magic," + Environment.NewLine +
                                "etc. Any wizard, scientist, or gadgeteer" + Environment.NewLine +
                                "needs a high IQ first of all. The secondary" + Environment.NewLine +
                                "characteristics of Will" + Environment.NewLine +
                                "and Perception are based on" + Environment.NewLine +
                                "IQ.";

            iudHealth.ToolTip = "Health measures energy and vitality." + Environment.NewLine +
                                "It represents stamina, resistance (to" + Environment.NewLine +
                                "poison, disease, radiation, etc.), and" + Environment.NewLine +
                                "basic “grit.” A high HT is good for anyone" + Environment.NewLine +
                                "– but it is vital for low-tech warriors." + Environment.NewLine +
                                "HT determines Fatigue Points," + Environment.NewLine +
                                " and helps determine Basic" + Environment.NewLine +
                                "Speed and Basic Move.";

            tbBasicLift.ToolTip = "Basic Lift is the maximum weight" + Environment.NewLine +
                                "you can lift over your head with one" + Environment.NewLine +
                                "hand in one second. It is equal to" + Environment.NewLine +
                                "(STxST)/5 lbs. If BL is 10 lbs. or more," + Environment.NewLine +
                                "round to the nearest whole number; e.g.," + Environment.NewLine +
                                "16.2 lbs. becomes 16 lbs. The average" + Environment.NewLine +
                                "human has ST 10 and a BL of 20 lbs." + Environment.NewLine +
                                "Doubling the time lets you lift" + Environment.NewLine +
                                "2¥BL overhead in one hand." + Environment.NewLine +
                                "Quadrupling the time, and using two" + Environment.NewLine +
                                "hands, you can lift 8xBL overhead.";

            iudHitPoints.ToolTip = "Hit Points represent your body’s" + Environment.NewLine +
                                "ability to sustain injury. By default," + Environment.NewLine +
                                "you have HP equal to your ST. For" + Environment.NewLine +
                                "instance, ST 10 gives 10 HP." + Environment.NewLine +
                                "You can increase HP at the cost of" + Environment.NewLine +
                                "2 points per HP, or reduce HP for -2" + Environment.NewLine +
                                "points per HP. In a realistic campaign," + Environment.NewLine +
                                "the GM should not allow HP to vary" + Environment.NewLine +
                                "by more than ±30% of ST; e.g., a ST 10" + Environment.NewLine +
                                "character could have between 7 and" + Environment.NewLine +
                                "13 HP.";

            iudWillPower.ToolTip = "Will measures your ability to withstand" + Environment.NewLine +
                                "psychological stress (brainwashing," + Environment.NewLine +
                                "fear, hypnotism, interrogation," + Environment.NewLine +
                                "seduction, torture, etc.) and your" + Environment.NewLine +
                                "resistance to supernatural attacks" + Environment.NewLine +
                                "(magic, psionics, etc.). By default, Will" + Environment.NewLine +
                                "is equal to IQ. You can increase it at" + Environment.NewLine +
                                "the cost of 5 points per +1, or reduce it" + Environment.NewLine +
                                "for -5 points per -1. You cannot raise" + Environment.NewLine +
                                "Will past 20, or lower it by more than" + Environment.NewLine +
                                "4, without GM permission." + Environment.NewLine +
                                "Note that Will does not represent" + Environment.NewLine +
                                "physical resistance – buy HT for that!";

            iudPerception.ToolTip = "Perception represents your general" + Environment.NewLine +
                                "alertness. The GM makes a “Sense" + Environment.NewLine +
                                "roll” against your Per to determine" + Environment.NewLine +
                                "whether you notice something." + Environment.NewLine +
                                "By default, Per" + Environment.NewLine +
                                "equals IQ, but you can increase it for 5" + Environment.NewLine +
                                "points per +1, or reduce it for -5 points" + Environment.NewLine +
                                "per -1. You cannot raise Per past 20, or" + Environment.NewLine +
                                "lower it by more than 4, without GM" + Environment.NewLine +
                                "permission.";

            iudFatiguePoints.ToolTip = "Fatigue Points represent your" + Environment.NewLine +
                                "body’s “energy supply.” By default, you" + Environment.NewLine +
                                "have FP equal to your HT. For" + Environment.NewLine +
                                "instance, HT 10 gives 10 FP." + Environment.NewLine +
                                "You can increase FP at the cost of 3" + Environment.NewLine +
                                "points per FP, or reduce FP for -3" + Environment.NewLine +
                                "points per FP. In a realistic campaign," + Environment.NewLine +
                                "the GM should not allow FP to vary by" + Environment.NewLine +
                                "more than ±30% of HT";

            iudBasicSpeed.ToolTip = "Your Basic Speed is a measure of" + Environment.NewLine +
                                "your reflexes and general physical" + Environment.NewLine +
                                "quickness. It helps determine your" + Environment.NewLine +
                                "running speed (see Basic Move," + Environment.NewLine +
                                "below), your chance of dodging an" + Environment.NewLine +
                                "attack, and the order in which you act" + Environment.NewLine +
                                "in combat (a high Basic Speed will let" + Environment.NewLine +
                                "you “out-react” your foes)." + Environment.NewLine +
                                "To calculate Basic Speed, add your" + Environment.NewLine +
                                "HT and DX together, and then divide" + Environment.NewLine +
                                "the total by 4. Do not round it off. A" + Environment.NewLine +
                                "5.25 is better than a 5!";

            iudBasicMove.ToolTip = "Your Basic Move is your ground" + Environment.NewLine +
                                "speed in yards per second. This is how" + Environment.NewLine +
                                "fast you can run – or roll, slither, etc. –" + Environment.NewLine +
                                "without encumbrance (although you" + Environment.NewLine +
                                "can go a little faster if you “sprint” in a" + Environment.NewLine +
                                "straight line;" + Environment.NewLine +
                                "Basic Move starts out equal to" + Environment.NewLine +
                                "Basic Speed, less any fractions; e.g.," + Environment.NewLine +
                                "Basic Speed 5.75 gives Basic Move 5." + Environment.NewLine +
                                "An average person has Basic Move 5;" + Environment.NewLine +
                                "therefore, he can run about 5 yards" + Environment.NewLine +
                                "per second if unencumbered." + Environment.NewLine +
                                "You can increase Basic Move for 5" + Environment.NewLine +
                                "points per yard/second or reduce it for" + Environment.NewLine +
                                "-5 points per yard/second.";
        }

        private void SetupTabControls()
        {
            AdvList = ServerConnection.LocalServiceClient.RequestAdvantages(MainWindow.CurrentInstance.LocalCient).OrderBy(x => x.Name).ToList();
            reqList = ServerConnection.LocalServiceClient.RequestRequirements((MainWindow.CurrentInstance.LocalCient));
            //lbAdvantages.ItemsSource = AdvList;
            foreach (SWNServiceReference.Advantages item in AdvList)
            {
                lbAdvantages.Items.Add(item);
            }
            lbAdvantages.DisplayMemberPath = "Name";
        }
        private void OnPointsChange()
        {
            tbPoints.Text = Points.ToString();
        }

        //Strenght

        private void iudStrenght_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyStrenght();
                }
                else
                {
                    SellStrenght();
                }
                UpdateBasicLift((int)e.NewValue);
                ResetModHitpoints();
            }
        }

        private void BuyStrenght()
        {
            //+-10 Points/Level
            if (CanBuy(10))
            {
                points = points - 10;
                Strength += 1;
                BaseHitpoints += 1;
            }
            else
            {
                iudStrenght.ValueChanged -= iudStrenght_ValueChanged;
                iudStrenght.Value -= 1;
                iudStrenght.ValueChanged += iudStrenght_ValueChanged;
            }
        }

        private void SellStrenght()
        {
            //+-10 Points/Level
            points = points + 10;
            Strength -= 1;
            BaseHitpoints -= 1;
        }

        //Dexterity

        private void iudDexterity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyDexterity();
                }
                else
                {
                    SellDexterity();
                }
            }
            ResetModSpeed();
            ResetModMove();
        }
        private void BuyDexterity()
        {
            //+-20 Points/Level
            if (CanBuy(20))
            {
                points = points - 20;
                Dexterity += 1;
            }
            else
            {
                iudDexterity.ValueChanged -= iudDexterity_ValueChanged;
                iudDexterity.Value -= 1;
                iudDexterity.ValueChanged += iudDexterity_ValueChanged;
            }
        }

        private void SellDexterity()
        {
            //+-20 Points/Level
            points = points + 20;
            Dexterity -= 1;
        }

        //Intelligence

        private void iudIntelligence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyIntelligence();
                }
                else
                {
                    SellIntelligence();
                }
                ResetModWill();
                ResetModPerception();
            }
        }
        private void BuyIntelligence()
        {
            //+-20 Points/Level
            if (CanBuy(20))
            {
                points = points - 20;
                Intelligence += 1;
                BaseWill += 1;
                BasePerception += 1;
            }
            else
            {
                iudIntelligence.ValueChanged -= iudIntelligence_ValueChanged;
                iudIntelligence.Value -= 1;
                iudIntelligence.ValueChanged += iudIntelligence_ValueChanged;
            }
        }

        private void SellIntelligence()
        {
            //+-20 Points/Level
            points = points + 20;
            Intelligence -= 1;
            BaseWill -= 1;
            BasePerception -= 1;
        }

        //Health

        private void iudHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyHealth();
                }
                else
                {
                    SellHealth();
                }
                ResetModFatiguePoints();
                ResetModSpeed();
                ResetModMove();
            }
        }

        private void BuyHealth()
        {
            //+-10 Points/Level
            if (CanBuy(10))
            {
                points = points - 10;
                Health += 1;
                BaseFatiguePoints += 1;
            }
            else
            {
                iudHealth.ValueChanged -= iudHealth_ValueChanged;
                iudHealth.Value -= 1;
                iudHealth.ValueChanged += iudHealth_ValueChanged;
            }
        }

        private void SellHealth()
        {
            //+-10 Points/Level
            points = points + 10;
            Health -= 1;
            BaseFatiguePoints -= 1;
        }

        //BasicLift

        private void UpdateBasicLift(int newStrenght)
        {
            tbBasicLift.Text = Math.Floor((Double)(((newStrenght * newStrenght) / 5) / 2)).ToString();
        }


        //Hitpoints

        private void UpdateBaseHitpoints(int newStrenght)
        {
            BaseHitpoints = newStrenght;
            iudHitPoints.ValueChanged -= iudHitPoints_ValueChanged;
            iudHitPoints.Value = BaseHitpoints + ModHitpoints;
            iudHitPoints.ValueChanged += iudHitPoints_ValueChanged;
        }

        private void ResetModHitpoints()
        {
            int h = ModHitpoints;
            if (ModHitpoints > 0)
            {
                for (int i = 0; i < h; i++)
                {
                    SellBonusHitpoints();
                }
            }
            if (ModHitpoints < 0)
            {
                for (int i = 0; i > h; i--)
                {
                    BuyBonusHitpoints();
                }
            }
            UpdateBaseHitpoints(Strength);
        }

        private void iudHitPoints_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int StrenghtPercent = 30;
            int CalcValueLowEnd = (int)(((Double)iudStrenght.Value / 100) * (100 - StrenghtPercent));
            int CalcValueHighEnd = (int)(((Double)iudStrenght.Value / 100) * (100 + StrenghtPercent));
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if ((int)e.NewValue > CalcValueHighEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + StrenghtPercent + " Percent!");
                        UpdateBaseHitpoints(Strength);
                    }
                    else
                    {
                        BuyBonusHitpoints();
                    }
                }
                else
                {
                    if ((int)e.NewValue < CalcValueLowEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + StrenghtPercent + " Percent!");
                        UpdateBaseHitpoints(Strength);
                    }
                    else
                    {
                        SellBonusHitpoints();
                    }
                }
            }
        }

        private void BuyBonusHitpoints()
        {
            //+-2 Points per +- 1 Hp
            if (CanBuy(2))
            {
                points = points - 2;
                ModHitpoints += 1;
            }
            else
            {
                iudHitPoints.ValueChanged -= iudHitPoints_ValueChanged;
                iudHitPoints.Value -= 1;
                iudHitPoints.ValueChanged += iudHitPoints_ValueChanged;
            }
        }

        private void SellBonusHitpoints()
        {
            //+-2 Points per +- 1 Hp
            points = points + 2;
            ModHitpoints -= 1;
        }

        //Will

        private void UpdateBaseWill(int newIntelligence)
        {
            BaseWill = newIntelligence;
            iudWillPower.ValueChanged -= iudWillPower_ValueChanged;
            iudWillPower.Value = BaseWill + ModWill;
            iudWillPower.ValueChanged += iudWillPower_ValueChanged;
        }

        private void ResetModWill()
        {
            int h = ModWill;
            if (ModWill > 0)
            {
                for (int i = 0; i < h; i++)
                {
                    SellBonusWill();
                }
            }
            if (ModWill < 0)
            {
                for (int i = 0; i > h; i--)
                {
                    BuyBonusWill();
                }
            }
            UpdateBaseWill(Intelligence);
        }

        private void iudWillPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if ((int)e.NewValue > 20)
                    {
                        System.Windows.MessageBox.Show("Can not raise Will higher than 20 without GM Permission");
                        UpdateBaseWill(Intelligence);
                    }
                    else
                    {
                        BuyBonusWill();
                    }
                }
                else
                {
                    if ((BaseWill - (int)e.NewValue) > 4)
                    {
                        System.Windows.MessageBox.Show("Can not lower Willpower by more then 4 Points from the Main Stat");
                        UpdateBaseWill(Intelligence);
                    }
                    else
                    {
                        SellBonusWill();
                    }
                }
            }
        }
        private void BuyBonusWill()
        {
            //+-5 Points per +- 1 Will
            if (CanBuy(5))
            {
                points = points - 5;
                ModWill += 1;
            }
            else
            {
                iudWillPower.ValueChanged -= iudWillPower_ValueChanged;
                iudWillPower.Value -= 1;
                iudWillPower.ValueChanged += iudWillPower_ValueChanged;
            }
        }

        private void SellBonusWill()
        {
            //+-5 Points per +- 1 Will
            points = points + 5;
            ModWill -= 1;
        }

        //Perception

        private void UpdateBasePerception(int newIntelligence)
        {
            BasePerception = newIntelligence;
            iudPerception.ValueChanged -= iudPerception_ValueChanged;
            iudPerception.Value = BasePerception + ModPerception;
            iudPerception.ValueChanged += iudPerception_ValueChanged;
        }

        private void ResetModPerception()
        {
            int h = ModPerception;
            if (ModPerception > 0)
            {
                for (int i = 0; i < h; i++)
                {
                    SellBonusPerception();
                }
            }
            if (ModPerception < 0)
            {
                for (int i = 0; i > h; i--)
                {
                    BuyBonusPerception();
                }
            }
            UpdateBasePerception(Intelligence);
        }

        private void iudPerception_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if ((int)e.NewValue > 20)
                    {
                        System.Windows.MessageBox.Show("Can not raise Perception higher than 20 without GM Permission");
                        UpdateBasePerception(Intelligence);
                    }
                    else
                    {
                        BuyBonusPerception();
                    }
                }
                else
                {
                    if ((BasePerception - (int)e.NewValue) > 4)
                    {
                        System.Windows.MessageBox.Show("Can not lower Perception by more then 4 Points from the Main Stat");
                        UpdateBasePerception(Intelligence);
                    }
                    else
                    {
                        SellBonusPerception();
                    }
                }
            }
        }
        private void BuyBonusPerception()
        {
            //+-5 Points per +- 1 Will
            if (CanBuy(5))
            {
                points = points - 5;
                ModPerception += 1;
            }
            else
            {
                iudPerception.ValueChanged -= iudPerception_ValueChanged;
                iudPerception.Value -= 1;
                iudPerception.ValueChanged += iudPerception_ValueChanged;
            }
        }

        private void SellBonusPerception()
        {
            //+-5 Points per +- 1 Will
            points = points + 5;
            ModPerception -= 1;
        }

        //Fatigue Points

        private void UpdateBaseFatiguePoints(int newHealth)
        {
            BaseFatiguePoints = newHealth;
            iudFatiguePoints.ValueChanged -= iudFatiguePoints_ValueChanged;
            iudFatiguePoints.Value = BaseFatiguePoints + ModFatiguePoints;
            iudFatiguePoints.ValueChanged += iudFatiguePoints_ValueChanged;
        }

        private void ResetModFatiguePoints()
        {
            int h = ModFatiguePoints;
            if (ModFatiguePoints > 0)
            {
                for (int i = 0; i < h; i++)
                {
                    SellBonusFatiguePoints();
                }
            }
            if (ModFatiguePoints < 0)
            {
                for (int i = 0; i > h; i--)
                {
                    BuyBonusFatiguePoints();
                }
            }
            UpdateBaseFatiguePoints(Health);
        }

        private void iudFatiguePoints_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int HealthPercent = 30;
            int CalcValueLowEnd = (int)(((Double)iudHealth.Value / 100) * (100 - HealthPercent));
            int CalcValueHighEnd = (int)(((Double)iudHealth.Value / 100) * (100 + HealthPercent));
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if ((int)e.NewValue > CalcValueHighEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Health by more then " + HealthPercent + " Percent!");
                        UpdateBaseFatiguePoints(Health);
                    }
                    else
                    {
                        BuyBonusFatiguePoints();
                    }
                }
                else
                {
                    if ((int)e.NewValue < CalcValueLowEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Health by more then " + HealthPercent + " Percent!");
                        UpdateBaseFatiguePoints(Health);
                    }
                    else
                    {
                        SellBonusFatiguePoints();
                    }
                }
            }
        }

        private void BuyBonusFatiguePoints()
        {
            //+-3 Points per +- 1 Hp
            if (CanBuy(3))
            {
                points = points - 3;
                ModFatiguePoints += 1;
            }
            else
            {
                iudFatiguePoints.ValueChanged -= iudFatiguePoints_ValueChanged;
                iudFatiguePoints.Value -= 1;
                iudFatiguePoints.ValueChanged += iudFatiguePoints_ValueChanged;
            }
        }

        private void SellBonusFatiguePoints()
        {
            //+-3 Points per +- 1 Hp
            points = points + 3;
            ModFatiguePoints -= 1;
        }

        //Base Speed

        private void UpdateBaseSpeed(int newHealth, int newDex)
        {
            BaseSpeed = ((newHealth + newDex) / 4.00);
            iudBasicSpeed.ValueChanged -= iudBasicSpeed_ValueChanged;
            iudBasicSpeed.Value = BaseSpeed + ModSpeed;
            iudBasicSpeed.ValueChanged += iudBasicSpeed_ValueChanged;
        }

        private void ResetModSpeed()
        {

            double h = ModSpeed;
            if (ModSpeed > 0)
            {
                for (double i = 0; i < h; i = i + 0.25)
                {
                    SellBonusSpeed();
                }
            }
            if (ModSpeed < 0)
            {
                for (double i = 0; i > h; i = i - 0.25)
                {
                    BuyBonusSpeed();
                }
            }
            UpdateBaseSpeed(Health, Dexterity);
        }

        private void iudBasicSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((double)e.OldValue < (double)e.NewValue)
                {
                    if (((double)e.NewValue) - BaseSpeed > 2.00)
                    {
                        System.Windows.MessageBox.Show("Can not raise Speed by more then 2.00 Points from the Main Stat");
                        UpdateBaseSpeed(Health, Dexterity);
                    }
                    else
                    {
                        BuyBonusSpeed();
                    }
                }
                else
                {
                    if ((BaseSpeed - (double)e.NewValue) > 2.00)
                    {
                        System.Windows.MessageBox.Show("Can not lower Speed by more then 2.00 Points from the Main Stat");
                        UpdateBaseSpeed(Health, Dexterity);
                    }
                    else
                    {
                        SellBonusSpeed();
                    }
                }
            }
            ResetModMove();
        }

        private void BuyBonusSpeed()
        {
            //+-5 Points per +- 0.25 Hp
            if (CanBuy(5))
            {
                points = points - 5;
                ModSpeed += 0.25;
            }
            else
            {
                iudBasicSpeed.ValueChanged -= iudBasicSpeed_ValueChanged;
                iudBasicSpeed.Value -= 1;
                iudBasicSpeed.ValueChanged += iudBasicSpeed_ValueChanged;
            }
        }

        private void SellBonusSpeed()
        {
            //+-5 Points per +- 0.25 Hp
            points = points + 5;
            ModSpeed -= 0.25;
        }

        //Basic Move

        private void UpdateMovePoints(double Basicspeed)
        {
            BaseMove = Convert.ToInt32(Math.Floor(Basicspeed + ModSpeed));
            iudBasicMove.ValueChanged -= iudBasicMove_ValueChanged;
            iudBasicMove.Value = BaseMove + ModMove;
            iudBasicMove.ValueChanged += iudBasicMove_ValueChanged;
        }

        private void ResetModMove()
        {
            double h = ModMove;
            if (ModMove > 0)
            {
                for (int i = 0; i < h; i++)
                {
                    SellBonusMove();
                }
            }
            if (ModMove < 0)
            {
                for (int i = 0; i > h; i++)
                {
                    BuyBonusMove();
                }
            }
            UpdateMovePoints(BaseSpeed);
        }

        private void iudBasicMove_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if (((int)e.NewValue) - BaseMove > 3)
                    {
                        System.Windows.MessageBox.Show("Can not raise Move by more then 3 Points from the Main Stat");
                        UpdateMovePoints(BaseMove);
                    }
                    else
                    {
                        BuyBonusMove();
                    }
                }
                else
                {
                    if ((BaseMove - (int)e.NewValue) > 3)
                    {
                        System.Windows.MessageBox.Show("Can not lower Move by more then 3 Points from the Main Stat");
                        UpdateMovePoints(BaseMove);
                    }
                    else
                    {
                        SellBonusMove();
                    }
                }
            }
        }

        private void BuyBonusMove()
        {
            //+-5 Points per +- 1 Yard
            if (CanBuy(5))
            {
                points = points - 5;
                ModMove += 1;
            }
            else
            {
                iudBasicMove.ValueChanged -= iudBasicMove_ValueChanged;
                iudBasicMove.Value -= 1;
                iudBasicMove.ValueChanged += iudBasicMove_ValueChanged;
            }
        }

        private void SellBonusMove()
        {
            //+-5 Points per +- 1 1 Yard
            points = points + 5;
            ModMove -= 1;
        }

        private void lbAdvantages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdvantages.SelectedItem != null)
            {
                PopulateAdvantageSideBar((SWNServiceReference.Advantages)lbAdvantages.SelectedItem);
                if (tbAdvReq.Text == "")
                {
                    btBuyAdv.IsEnabled = true;
                }
                else
                {
                    btBuyAdv.IsEnabled = false;
                    foreach (SWNServiceReference.Advantages item in lbBoughtAdvantages.Items)
                    {
                        if (item.Name == tbAdvReq.Text)
                        {
                            btBuyAdv.IsEnabled = true;
                        }
                    }
                }
            }
            else
            {
                btBuyAdv.IsEnabled = false;
            }
        }

        private void PopulateAdvantageSideBar(SWNServiceReference.Advantages item)
        {
            tbAdvType.Text = GetAdvType(item);
            tbAdvReq.Text = GetRequirements(item);
            tbAdvName.Text = item.Name;
            txtbAdvDiscription.Text = item.Discription;
            tbAdvPointCost.Text = item.PointCost.ToString();
            if ((bool)item.hasLevels)
            {
                lbladvLevel.Visibility = Visibility.Visible;
            }
            else
            {
                lbladvLevel.Visibility = Visibility.Hidden;
            }
        }

        private string GetRequirements(SWNServiceReference.Advantages advitem)
        {
            foreach (SWNServiceReference.Requirements item in reqList)
            {
                if (item.SourceType == "Advantage")
                {
                    if (item.SourceItemID == advitem.Id)
                    {
                        return item.TargetName;
                    }
                }
            }
            return "";
            //Requirements reqquery = (from c in context.Requirements where c.SourceItemID == Advantagequery.Id select c).FirstOrDefault();
        }

        private void ClearAdvantageSideBar()
        {
            tbAdvName.Text = "";
            tbAdvReq.Text = "";
            tbAdvType.Text = "";
            txtbAdvDiscription.Text = "";
            tbAdvPointCost.Text = "";
        }

        //private void PopulateDisadvantageSideBar(SWNServiceReference.Disadvantages item)
        //{
        //    tbAdvType.Text = GetDisAdvType(item);
        //    txtbAdvDiscription.Text = item.Discription;
        //    tbAdvPointCost.Text = item.PointCost.ToString();
        //}

        private string GetAdvType(SWNServiceReference.Advantages item)
        {
            List<string> Typelist = new List<string>();
            string Type = "";
            if (item.isPhysical == true)
            {
                Typelist.Add("Physical");
            }
            if (item.isMental == true)
            {
                Typelist.Add("Mental");
            }
            if (item.isMundane == true)
            {
                Typelist.Add("Mundane");
            }
            if (item.isExotic == true)
            {
                Typelist.Add("Exotic");
            }
            if (item.isSocial == true)
            {
                Typelist.Add("Social");
            }
            if (item.isSuperNatural == true)
            {
                Typelist.Add("Super-Natural");
            }
            Type = string.Join(",", Typelist);
            return Type;
        }
        //private string GetDisAdvType(SWNServiceReference.Disadvantages item)
        //{
        //    List<string> Typelist = new List<string>();
        //    string Type = "";
        //    if (item.isPhysical == true)
        //    {
        //        Typelist.Add("Physical");
        //    }
        //    if (item.isMental == true)
        //    {
        //        Typelist.Add("Mental");
        //    }
        //    if (item.isMundane == true)
        //    {
        //        Typelist.Add("Mundane");
        //    }
        //    if (item.isExotic == true)
        //    {
        //        Typelist.Add("Exotic");
        //    }
        //    if (item.isSocial == true)
        //    {
        //        Typelist.Add("Social");
        //    }
        //    if (item.isSuperNatural == true)
        //    {
        //        Typelist.Add("Super-Natural");
        //    }
        //    Type = string.Join(",", Typelist);
        //    return Type;
        //}

        private void lbDisadvantages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btBuyAdv_Click(object sender, RoutedEventArgs e)
        {
            BuyAdvantage();
            ClearAdvantageSideBar();
        }

        private void btSellAdv_Click(object sender, RoutedEventArgs e)
        {
            SellAdvantage();

            List<SWNServiceReference.Advantages> sortlist = new List<SWNServiceReference.Advantages>();
            foreach (SWNServiceReference.Advantages item in lbAdvantages.Items)
            {
                sortlist.Add(item);
            }
            var newList = sortlist.OrderBy(x => x.Name).ToList();
            lbAdvantages.Items.Clear();
            foreach (SWNServiceReference.Advantages item in newList)
            {
                lbAdvantages.Items.Add(item);
            }
            ClearAdvantageSideBar();
            tbboughtAdvLevels.Text = "";
        }

        private void BuyAdvantage()
        {
            SWNServiceReference.Advantages adv = lbAdvantages.SelectedItem as SWNServiceReference.Advantages;
            if (CanBuy((int)adv.PointCost))
            {
                points -= (int)adv.PointCost;
                if ((bool)adv.hasLevels)
                {
                    if (AdvantageLevels.ContainsKey(adv))
                    {
                        int value = AdvantageLevels[adv];
                        AdvantageLevels[adv] = value + 1;
                    }
                    else
                    {
                        AdvantageLevels.Add(adv, 1);
                        lbBoughtAdvantages.Items.Add(adv);
                        lbBoughtAdvantages.DisplayMemberPath = "Name";
                    }
                }
                else
                {
                    lbBoughtAdvantages.Items.Add(adv);
                    lbBoughtAdvantages.DisplayMemberPath = "Name";
                    lbAdvantages.Items.Remove(lbAdvantages.SelectedItem);
                }
                lbBoughtAdvantages.SelectedItem = null;
            }
        }

        private void SellAdvantage()
        {
            SWNServiceReference.Advantages adv = lbBoughtAdvantages.SelectedItem as SWNServiceReference.Advantages;
            points += (int)adv.PointCost;
            if ((bool)adv.hasLevels)
            {
                if (AdvantageLevels.ContainsKey(adv))
                {
                    if (AdvantageLevels[adv] > 1)
                    {
                        AdvantageLevels[adv] -= 1;
                    }
                    else
                    {
                        AdvantageLevels.Remove(adv);
                        lbBoughtAdvantages.Items.Remove(adv);
                    }
                }
            }
            else
            {
                lbAdvantages.Items.Add(adv);
                lbAdvantages.Items.IsLiveSorting = true;
                lbAdvantages.DisplayMemberPath = "Name";
                lbBoughtAdvantages.Items.Remove(lbBoughtAdvantages.SelectedItem);
            }
            lbBoughtAdvantages.SelectedItem = null;
            tbboughtAdvLevels.Text = "";
        }

        private bool CanBuy(int Cost)
        {
            bool canbuy = false;
            if ((Points - Cost) >= 0)
            {
                canbuy = true;
            }
            else
            {
                System.Windows.MessageBox.Show("That is to expensive!");
            }
            return canbuy;
        }

        private void lbBoughtAdvantages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBoughtAdvantages.SelectedItem != null)
            {
                PopulateAdvantageSideBar((SWNServiceReference.Advantages)lbBoughtAdvantages.SelectedItem);
                btSellAdv.IsEnabled = true;
                if ((bool)((SWNServiceReference.Advantages)lbBoughtAdvantages.SelectedItem).hasLevels)
                {
                    tbboughtAdvLevels.Text = AdvantageLevels[((SWNServiceReference.Advantages)lbBoughtAdvantages.SelectedItem)].ToString();
                }
                else
                {
                    tbboughtAdvLevels.Text = "";
                }
            }
            else
            {
                btSellAdv.IsEnabled = false;
            }
        }

        private void lbBoughtAdvantages_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lbBoughtAdvantages.SelectedItem != null)
            {
                lbBoughtAdvantages.SelectedItem = lbBoughtAdvantages.SelectedItem;
            }
        }

        private void btSaveCharacter_Click(object sender, RoutedEventArgs e)
        {
            character.Age = Int32.Parse(tbAge.Text);
            character.BasicLift = Int32.Parse(tbBasicLift.Text);
            character.BasicMove = iudBasicMove.Value;
            character.BasicSpeed = iudBasicSpeed.Value;
            character.Dexterity = iudDexterity.Value;
            character.FatiguePoints = iudFatiguePoints.Value;
            character.Health = iudHealth.Value;
            character.Height = Double.Parse(tbHeight.Text);
            character.HitPoints = iudHitPoints.Value;
            character.Intelligence = iudIntelligence.Value;
            character.Name = tbName.Text;
            character.Perception = iudPerception.Value;
            character.PlayerName = MainWindow.CurrentInstance.LocalCient.UserName;
            character.PointTotal = Int32.Parse(tbPoints.Text);
            character.Strenght = iudStrenght.Value;
            character.Weight = Int32.Parse(tbWeight.Text);
            character.WillPower = iudWillPower.Value;
            ServerConnection.LocalServiceClient.SaveCharacter(MainWindow.CurrentInstance.LocalCient, character);
            System.Windows.MessageBox.Show("Saved!");
        }
    }
}
