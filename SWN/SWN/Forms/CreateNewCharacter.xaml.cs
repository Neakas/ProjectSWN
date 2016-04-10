using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SWN.Networking;
using SWN.SWNServiceReference;

namespace SWN.Forms
{
    public partial class CreateNewCharacter
    {
        private readonly Character _character;
        private List<Advantages> _advList = new List<Advantages>();
        private int _baseFatiguePoints;
        private int _baseHitpoints;
        private int _baseMove;
        private int _basePerception;
        private double _baseSpeed;
        private int _baseWill;
        private int _dexterity;
        private int _health;
        private int _intelligence;
        private int _modFatiguePoints;
        private int _modHitpoints;
        private int _modMove;
        private int _modPerception;
        private double _modSpeed;
        private int _modWill;
        //ToDo: Finish Me: MewCharacter
        private int _points;
        private List<Requirements> _reqList = new List<Requirements>();
        private int _strength;
        private readonly Dictionary<Advantages, int> _boughtAdvantageDict = new Dictionary<Advantages, int>();

        public CreateNewCharacter( Character c )
        {
            _character = c;
            InitializeComponent();
            DataContext = c;
            if (c.PointTotal != null)
            {
                _points = (int) c.PointTotal;
            }
            if (c.Strenght != null)
            {
                _strength = (int) c.Strenght;
            }
            if (c.Dexterity != null)
            {
                _dexterity = (int) c.Dexterity;
            }
            if (c.Intelligence != null)
            {
                _intelligence = (int) c.Intelligence;
            }
            if (c.Health != null)
            {
                _health = (int) c.Health;
            }
            if (c.HitPoints != null)
            {
                _baseHitpoints = (int) c.HitPoints;
            }
            UpdateBaseHitpoints(_strength);
            if (c.WillPower != null)
            {
                _baseWill = (int) c.WillPower;
            }
            UpdateBaseWill(_intelligence);
            if (c.Perception != null)
            {
                _basePerception = (int) c.Perception;
            }
            UpdateBasePerception(_intelligence);
            if (c.FatiguePoints != null)
            {
                _baseFatiguePoints = (int) c.FatiguePoints;
            }
            UpdateBaseFatiguePoints(_health);
            if (c.BasicSpeed != null)
            {
                _baseSpeed = (double) c.BasicSpeed;
            }
            UpdateBaseSpeed(_health, _dexterity);
            if (c.BasicMove != null)
            {
                _baseMove = (int) c.BasicMove;
            }
            UpdateMovePoints(_baseSpeed);
            SetupToolTips();
            SetupTabControls();
            ReloadDataGrid();
        }

        private int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                OnPointsChange();
            }
        }

        private void ReloadDataGrid()
        {
            DgBoughtAdvantages.ItemsSource = _boughtAdvantageDict.ToDictionary(t => t.Key.Name, t => t.Value);
            if (DgBoughtAdvantages.Items.Count <= 0)
            {
                return;
            }
            DgBoughtAdvantages.Columns[0].Header = "Bought Advantage";
            DgBoughtAdvantages.Columns[1].Header = "Bought Level";
            var clr = new Color
            {
                A = 255,
                R = 51,
                G = 51,
                B = 51
            };
            Brush bgbrush = new SolidColorBrush(clr);
            DgBoughtAdvantages.RowBackground = bgbrush;
            DgBoughtAdvantages.Foreground = Brushes.White;
            DgBoughtAdvantages.Columns[0].Width = 420;
        }

        private void SetupToolTips()
        {
            IudStrenght.ToolTip = "_strength measures physical power" + Environment.NewLine + "and bulk. It is crucial if you are a" + Environment.NewLine + "warrior in a primitive world, as high" + Environment.NewLine +
                                  "ST lets you dish out and absorb more" + Environment.NewLine + "damage in hand-to-hand combat. Any" + Environment.NewLine + "adventurer will find ST useful for" + Environment.NewLine +
                                  "lifting and throwing things, moving" + Environment.NewLine + "quickly with a load, etc. ST directly" + Environment.NewLine + "determines Basic Lift, basic" + Environment.NewLine +
                                  "damage, and Hit _points," + Environment.NewLine + "and affects your _character’s Build";

            IudDexterity.ToolTip = "_dexterity measures a combination" + Environment.NewLine + "of agility, coordination, and fine" + Environment.NewLine + "motor ability. It controls your basic" + Environment.NewLine +
                                   "ability at most athletic, fighting, and" + Environment.NewLine + "vehicle-operation skills, and at craft" + Environment.NewLine + "skills that call for a delicate touch. DX" + Environment.NewLine +
                                   "also helps determine Basic Speed (a" + Environment.NewLine + "measure of reaction time) and" + Environment.NewLine + "Basic Move.";

            IudIntelligence.ToolTip = "_intelligence broadly measures" + Environment.NewLine + "brainpower, including creativity, intuition," + Environment.NewLine + "memory, perception, reason," + Environment.NewLine +
                                      "sanity, and willpower. It rules your" + Environment.NewLine + "basic ability with all “mental” skills –" + Environment.NewLine + "sciences, social interaction, magic," + Environment.NewLine +
                                      "etc. Any wizard, scientist, or gadgeteer" + Environment.NewLine + "needs a high IQ first of all. The secondary" + Environment.NewLine + "characteristics of Will" + Environment.NewLine +
                                      "and Perception are based on" + Environment.NewLine + "IQ.";

            IudHealth.ToolTip = "_health measures energy and vitality." + Environment.NewLine + "It represents stamina, resistance (to" + Environment.NewLine + "poison, disease, radiation, etc.), and" + Environment.NewLine +
                                "basic “grit.” A high HT is good for anyone" + Environment.NewLine + "– but it is vital for low-tech warriors." + Environment.NewLine + "HT determines Fatigue _points," + Environment.NewLine +
                                " and helps determine Basic" + Environment.NewLine + "Speed and Basic Move.";

            TbBasicLift.ToolTip = "Basic Lift is the maximum weight" + Environment.NewLine + "you can lift over your head with one" + Environment.NewLine + "hand in one second. It is equal to" + Environment.NewLine +
                                  "(STxST)/5 lbs. If BL is 10 lbs. or more," + Environment.NewLine + "round to the nearest whole number; e.g.," + Environment.NewLine + "16.2 lbs. becomes 16 lbs. The average" + Environment.NewLine +
                                  "human has ST 10 and a BL of 20 lbs." + Environment.NewLine + "Doubling the time lets you lift" + Environment.NewLine + "2¥BL overhead in one hand." + Environment.NewLine +
                                  "Quadrupling the time, and using two" + Environment.NewLine + "hands, you can lift 8xBL overhead.";

            IudHitPoints.ToolTip = "Hit _points represent your body’s" + Environment.NewLine + "ability to sustain injury. By default," + Environment.NewLine + "you have HP equal to your ST. For" + Environment.NewLine +
                                   "instance, ST 10 gives 10 HP." + Environment.NewLine + "You can increase HP at the cost of" + Environment.NewLine + "2 Points per HP, or reduce HP for -2" + Environment.NewLine +
                                   "Points per HP. In a realistic campaign," + Environment.NewLine + "the GM should not allow HP to vary" + Environment.NewLine + "by more than ±30% of ST; e.g., a ST 10" + Environment.NewLine +
                                   "_character could have between 7 and" + Environment.NewLine + "13 HP.";

            IudWillPower.ToolTip = "Will measures your ability to withstand" + Environment.NewLine + "psychological stress (brainwashing," + Environment.NewLine + "fear, hypnotism, interrogation," + Environment.NewLine +
                                   "seduction, torture, etc.) and your" + Environment.NewLine + "resistance to supernatural attacks" + Environment.NewLine + "(magic, psionics, etc.). By default, Will" + Environment.NewLine +
                                   "is equal to IQ. You can increase it at" + Environment.NewLine + "the cost of 5 Points per +1, or reduce it" + Environment.NewLine + "for -5 Points per -1. You cannot raise" + Environment.NewLine +
                                   "Will past 20, or lower it by more than" + Environment.NewLine + "4, without GM permission." + Environment.NewLine + "Note that Will does not represent" + Environment.NewLine +
                                   "physical resistance – buy HT for that!";

            IudPerception.ToolTip = "Perception represents your general" + Environment.NewLine + "alertness. The GM makes a “Sense" + Environment.NewLine + "roll” against your Per to determine" + Environment.NewLine +
                                    "whether you notice something." + Environment.NewLine + "By default, Per" + Environment.NewLine + "equals IQ, but you can increase it for 5" + Environment.NewLine +
                                    "Points per +1, or reduce it for -5 Points" + Environment.NewLine + "per -1. You cannot raise Per past 20, or" + Environment.NewLine + "lower it by more than 4, without GM" + Environment.NewLine +
                                    "permission.";

            IudFatiguePoints.ToolTip = "Fatigue _points represent your" + Environment.NewLine + "body’s “energy supply.” By default, you" + Environment.NewLine + "have FP equal to your HT. For" + Environment.NewLine +
                                       "instance, HT 10 gives 10 FP." + Environment.NewLine + "You can increase FP at the cost of 3" + Environment.NewLine + "Points per FP, or reduce FP for -3" + Environment.NewLine +
                                       "Points per FP. In a realistic campaign," + Environment.NewLine + "the GM should not allow FP to vary by" + Environment.NewLine + "more than ±30% of HT";

            IudBasicSpeed.ToolTip = "Your Basic Speed is a measure of" + Environment.NewLine + "your reflexes and general physical" + Environment.NewLine + "quickness. It helps determine your" + Environment.NewLine +
                                    "running speed (see Basic Move," + Environment.NewLine + "below), your chance of dodging an" + Environment.NewLine + "attack, and the order in which you act" + Environment.NewLine +
                                    "in combat (a high Basic Speed will let" + Environment.NewLine + "you “out-react” your foes)." + Environment.NewLine + "To calculate Basic Speed, add your" + Environment.NewLine +
                                    "HT and DX together, and then divide" + Environment.NewLine + "the total by 4. Do not round it off. A" + Environment.NewLine + "5.25 is better than a 5!";

            IudBasicMove.ToolTip = "Your Basic Move is your ground" + Environment.NewLine + "speed in yards per second. This is how" + Environment.NewLine + "fast you can run – or roll, slither, etc. –" + Environment.NewLine +
                                   "without encumbrance (although you" + Environment.NewLine + "can go a little faster if you “sprint” in a" + Environment.NewLine + "straight line;" + Environment.NewLine + "Basic Move starts out equal to" +
                                   Environment.NewLine + "Basic Speed, less any fractions; e.g.," + Environment.NewLine + "Basic Speed 5.75 gives Basic Move 5." + Environment.NewLine + "An average person has Basic Move 5;" +
                                   Environment.NewLine + "therefore, he can run about 5 yards" + Environment.NewLine + "per second if unencumbered." + Environment.NewLine + "You can increase Basic Move for 5" + Environment.NewLine +
                                   "Points per yard/second or reduce it for" + Environment.NewLine + "-5 Points per yard/second.";
        }

        private void SetupTabControls()
        {
            _advList = ServerConnection.LocalServiceClient.RequestAdvantages(MainWindow.CurrentInstance.LocalCient).OrderBy(x => x.Name).ToList();
            _reqList = ServerConnection.LocalServiceClient.RequestRequirements(MainWindow.CurrentInstance.LocalCient);
            //LbAdvantages.ItemsSource = _advList;
            LbAdvantages.Items.SortDescriptions.Add(new SortDescription("Content", ListSortDirection.Ascending));
            LbAdvantages.Items.IsLiveSorting = true;
            foreach (var item in _advList)
            {
                LbAdvantages.Items.Add(item);
            }
            LbAdvantages.DisplayMemberPath = "Name";
        }

        private void OnPointsChange()
        {
            TbPoints.Text = _points.ToString();
            TbLinkedPoints.Text = TbPoints.Text;
        }

        //Strenght

        private void iudStrenght_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
                {
                    BuyStrenght();
                }
                else
                {
                    SellStrenght();
                }
                UpdateBasicLift((int) e.NewValue);
                ResetModHitpoints();
            }
        }

        private void BuyStrenght()
        {
            //+-10 _points/Level
            if (CanBuy(10))
            {
                Points = Points - 10;
                _strength += 1;
                _baseHitpoints += 1;
            }
            else
            {
                IudStrenght.ValueChanged -= iudStrenght_ValueChanged;
                IudStrenght.Value -= 1;
                IudStrenght.ValueChanged += iudStrenght_ValueChanged;
            }
        }

        private void SellStrenght()
        {
            //+-10 _points/Level
            Points = Points + 10;
            _strength -= 1;
            _baseHitpoints -= 1;
        }

        //_dexterity

        private void iudDexterity_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
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
            //+-20 _points/Level
            if (CanBuy(20))
            {
                Points = Points - 20;
                _dexterity += 1;
            }
            else
            {
                IudDexterity.ValueChanged -= iudDexterity_ValueChanged;
                IudDexterity.Value -= 1;
                IudDexterity.ValueChanged += iudDexterity_ValueChanged;
            }
        }

        private void SellDexterity()
        {
            //+-20 _points/Level
            Points = Points + 20;
            _dexterity -= 1;
        }

        //_intelligence

        private void iudIntelligence_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
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
            //+-20 _points/Level
            if (CanBuy(20))
            {
                Points = Points - 20;
                _intelligence += 1;
                _baseWill += 1;
                _basePerception += 1;
            }
            else
            {
                IudIntelligence.ValueChanged -= iudIntelligence_ValueChanged;
                IudIntelligence.Value -= 1;
                IudIntelligence.ValueChanged += iudIntelligence_ValueChanged;
            }
        }

        private void SellIntelligence()
        {
            //+-20 _points/Level
            Points = Points + 20;
            _intelligence -= 1;
            _baseWill -= 1;
            _basePerception -= 1;
        }

        //_health

        private void iudHealth_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
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
            //+-10 _points/Level
            if (CanBuy(10))
            {
                Points = Points - 10;
                _health += 1;
                _baseFatiguePoints += 1;
            }
            else
            {
                IudHealth.ValueChanged -= iudHealth_ValueChanged;
                IudHealth.Value -= 1;
                IudHealth.ValueChanged += iudHealth_ValueChanged;
            }
        }

        private void SellHealth()
        {
            //+-10 _points/Level
            Points = Points + 10;
            _health -= 1;
            _baseFatiguePoints -= 1;
        }

        //BasicLift

        private void UpdateBasicLift( int newStrenght )
        {
            TbBasicLift.Text = Math.Floor(newStrenght * newStrenght / 5.00 / 2.00).ToString(CultureInfo.InvariantCulture);
        }

        //Hitpoints

        private void UpdateBaseHitpoints( int newStrenght )
        {
            _baseHitpoints = newStrenght;
            IudHitPoints.ValueChanged -= iudHitPoints_ValueChanged;
            IudHitPoints.Value = _baseHitpoints + _modHitpoints;
            IudHitPoints.ValueChanged += iudHitPoints_ValueChanged;
        }

        private void ResetModHitpoints()
        {
            var h = _modHitpoints;
            if (_modHitpoints > 0)
            {
                for (var i = 0; i < h; i++)
                {
                    SellBonusHitpoints();
                }
            }
            if (_modHitpoints < 0)
            {
                for (var i = 0; i > h; i--)
                {
                    BuyBonusHitpoints();
                }
            }
            UpdateBaseHitpoints(_strength);
        }

        private void iudHitPoints_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            const int strenghtPercent = 30;
            if (IudStrenght.Value == null)
            {
                return;
            }
            var calcValueLowEnd = (int) ( (double) IudStrenght.Value / 100 * ( 100 - strenghtPercent ) );
            var calcValueHighEnd = (int) ( (double) IudStrenght.Value / 100 * ( 100 + strenghtPercent ) );
            if (e.OldValue == null)
            {
                return;
            }
            if ((int) e.OldValue < (int) e.NewValue)
            {
                if ((int) e.NewValue > calcValueHighEnd)
                {
                    MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + strenghtPercent + " Percent!");
                    UpdateBaseHitpoints(_strength);
                }
                else
                {
                    BuyBonusHitpoints();
                }
            }
            else
            {
                if ((int) e.NewValue < calcValueLowEnd)
                {
                    MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + strenghtPercent + " Percent!");
                    UpdateBaseHitpoints(_strength);
                }
                else
                {
                    SellBonusHitpoints();
                }
            }
        }

        private void BuyBonusHitpoints()
        {
            //+-2 _points per +- 1 Hp
            if (CanBuy(2))
            {
                Points = Points - 2;
                _modHitpoints += 1;
            }
            else
            {
                IudHitPoints.ValueChanged -= iudHitPoints_ValueChanged;
                IudHitPoints.Value -= 1;
                IudHitPoints.ValueChanged += iudHitPoints_ValueChanged;
            }
        }

        private void SellBonusHitpoints()
        {
            //+-2 _points per +- 1 Hp
            Points = Points + 2;
            _modHitpoints -= 1;
        }

        //Will

        private void UpdateBaseWill( int newIntelligence )
        {
            _baseWill = newIntelligence;
            IudWillPower.ValueChanged -= iudWillPower_ValueChanged;
            IudWillPower.Value = _baseWill + _modWill;
            IudWillPower.ValueChanged += iudWillPower_ValueChanged;
        }

        private void ResetModWill()
        {
            var h = _modWill;
            if (_modWill > 0)
            {
                for (var i = 0; i < h; i++)
                {
                    SellBonusWill();
                }
            }
            if (_modWill < 0)
            {
                for (var i = 0; i > h; i--)
                {
                    BuyBonusWill();
                }
            }
            UpdateBaseWill(_intelligence);
        }

        private void iudWillPower_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
                {
                    if ((int) e.NewValue > 20)
                    {
                        MessageBox.Show("Can not raise Will higher than 20 without GM Permission");
                        UpdateBaseWill(_intelligence);
                    }
                    else
                    {
                        BuyBonusWill();
                    }
                }
                else
                {
                    if (_baseWill - (int) e.NewValue > 4)
                    {
                        MessageBox.Show("Can not lower Willpower by more then 4 _points from the Main Stat");
                        UpdateBaseWill(_intelligence);
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
            //+-5 _points per +- 1 Will
            if (CanBuy(5))
            {
                Points = Points - 5;
                _modWill += 1;
            }
            else
            {
                IudWillPower.ValueChanged -= iudWillPower_ValueChanged;
                IudWillPower.Value -= 1;
                IudWillPower.ValueChanged += iudWillPower_ValueChanged;
            }
        }

        private void SellBonusWill()
        {
            //+-5 _points per +- 1 Will
            Points = Points + 5;
            _modWill -= 1;
        }

        //Perception

        private void UpdateBasePerception( int newIntelligence )
        {
            _basePerception = newIntelligence;
            IudPerception.ValueChanged -= iudPerception_ValueChanged;
            IudPerception.Value = _basePerception + _modPerception;
            IudPerception.ValueChanged += iudPerception_ValueChanged;
        }

        private void ResetModPerception()
        {
            var h = _modPerception;
            if (_modPerception > 0)
            {
                for (var i = 0; i < h; i++)
                {
                    SellBonusPerception();
                }
            }
            if (_modPerception < 0)
            {
                for (var i = 0; i > h; i--)
                {
                    BuyBonusPerception();
                }
            }
            UpdateBasePerception(_intelligence);
        }

        private void iudPerception_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
                {
                    if ((int) e.NewValue > 20)
                    {
                        MessageBox.Show("Can not raise Perception higher than 20 without GM Permission");
                        UpdateBasePerception(_intelligence);
                    }
                    else
                    {
                        BuyBonusPerception();
                    }
                }
                else
                {
                    if (_basePerception - (int) e.NewValue > 4)
                    {
                        MessageBox.Show("Can not lower Perception by more then 4 _points from the Main Stat");
                        UpdateBasePerception(_intelligence);
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
            //+-5 _points per +- 1 Will
            if (CanBuy(5))
            {
                Points = Points - 5;
                _modPerception += 1;
            }
            else
            {
                IudPerception.ValueChanged -= iudPerception_ValueChanged;
                IudPerception.Value -= 1;
                IudPerception.ValueChanged += iudPerception_ValueChanged;
            }
        }

        private void SellBonusPerception()
        {
            //+-5 _points per +- 1 Will
            Points = Points + 5;
            _modPerception -= 1;
        }

        //Fatigue _points

        private void UpdateBaseFatiguePoints( int newHealth )
        {
            _baseFatiguePoints = newHealth;
            IudFatiguePoints.ValueChanged -= iudFatiguePoints_ValueChanged;
            IudFatiguePoints.Value = _baseFatiguePoints + _modFatiguePoints;
            IudFatiguePoints.ValueChanged += iudFatiguePoints_ValueChanged;
        }

        private void ResetModFatiguePoints()
        {
            var h = _modFatiguePoints;
            if (_modFatiguePoints > 0)
            {
                for (var i = 0; i < h; i++)
                {
                    SellBonusFatiguePoints();
                }
            }
            if (_modFatiguePoints < 0)
            {
                for (var i = 0; i > h; i--)
                {
                    BuyBonusFatiguePoints();
                }
            }
            UpdateBaseFatiguePoints(_health);
        }

        private void iudFatiguePoints_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            const int healthPercent = 30;
            if (IudHealth.Value == null)
            {
                return;
            }
            var calcValueLowEnd = (int) ( (double) IudHealth.Value / 100 * ( 100 - healthPercent ) );
            var calcValueHighEnd = (int) ( (double) IudHealth.Value / 100 * ( 100 + healthPercent ) );
            if (e.OldValue == null)
            {
                return;
            }
            if ((int) e.OldValue < (int) e.NewValue)
            {
                if ((int) e.NewValue > calcValueHighEnd)
                {
                    MessageBox.Show("Can not deviate from the Main Stat: _health by more then " + healthPercent + " Percent!");
                    UpdateBaseFatiguePoints(_health);
                }
                else
                {
                    BuyBonusFatiguePoints();
                }
            }
            else
            {
                if ((int) e.NewValue < calcValueLowEnd)
                {
                    MessageBox.Show("Can not deviate from the Main Stat: _health by more then " + healthPercent + " Percent!");
                    UpdateBaseFatiguePoints(_health);
                }
                else
                {
                    SellBonusFatiguePoints();
                }
            }
        }

        private void BuyBonusFatiguePoints()
        {
            //+-3 _points per +- 1 Hp
            if (CanBuy(3))
            {
                Points = Points - 3;
                _modFatiguePoints += 1;
            }
            else
            {
                IudFatiguePoints.ValueChanged -= iudFatiguePoints_ValueChanged;
                IudFatiguePoints.Value -= 1;
                IudFatiguePoints.ValueChanged += iudFatiguePoints_ValueChanged;
            }
        }

        private void SellBonusFatiguePoints()
        {
            //+-3 _points per +- 1 Hp
            Points = Points + 3;
            _modFatiguePoints -= 1;
        }

        //Base Speed

        private void UpdateBaseSpeed( int newHealth, int newDex )
        {
            _baseSpeed = ( newHealth + newDex ) / 4.00;
            IudBasicSpeed.ValueChanged -= iudBasicSpeed_ValueChanged;
            IudBasicSpeed.Value = _baseSpeed + _modSpeed;
            IudBasicSpeed.ValueChanged += iudBasicSpeed_ValueChanged;
        }

        private void ResetModSpeed()
        {
            var h = _modSpeed;
            if (_modSpeed > 0)
            {
                for (double i = 0; i < h; i = i + 0.25)
                {
                    SellBonusSpeed();
                }
            }
            if (_modSpeed < 0)
            {
                for (double i = 0; i > h; i = i - 0.25)
                {
                    BuyBonusSpeed();
                }
            }
            UpdateBaseSpeed(_health, _dexterity);
        }

        private void iudBasicSpeed_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((double) e.OldValue < (double) e.NewValue)
                {
                    if ((double) e.NewValue - _baseSpeed > 2.00)
                    {
                        MessageBox.Show("Can not raise Speed by more then 2.00 _points from the Main Stat");
                        UpdateBaseSpeed(_health, _dexterity);
                    }
                    else
                    {
                        BuyBonusSpeed();
                    }
                }
                else
                {
                    if (_baseSpeed - (double) e.NewValue > 2.00)
                    {
                        MessageBox.Show("Can not lower Speed by more then 2.00 _points from the Main Stat");
                        UpdateBaseSpeed(_health, _dexterity);
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
            //+-5 _points per +- 0.25 Hp
            if (CanBuy(5))
            {
                Points = Points - 5;
                _modSpeed += 0.25;
            }
            else
            {
                IudBasicSpeed.ValueChanged -= iudBasicSpeed_ValueChanged;
                IudBasicSpeed.Value -= 1;
                IudBasicSpeed.ValueChanged += iudBasicSpeed_ValueChanged;
            }
        }

        private void SellBonusSpeed()
        {
            //+-5 _points per +- 0.25 Hp
            Points = Points + 5;
            _modSpeed -= 0.25;
        }

        //Basic Move

        private void UpdateMovePoints( double basicspeed )
        {
            _baseMove = Convert.ToInt32(Math.Floor(basicspeed + _modSpeed));
            IudBasicMove.ValueChanged -= iudBasicMove_ValueChanged;
            IudBasicMove.Value = _baseMove + _modMove;
            IudBasicMove.ValueChanged += iudBasicMove_ValueChanged;
        }

        private void ResetModMove()
        {
            double h = _modMove;
            if (_modMove > 0)
            {
                for (var i = 0; i < h; i++)
                {
                    SellBonusMove();
                }
            }
            if (_modMove < 0)
            {
                for (var i = 0; i > h; i++)
                {
                    BuyBonusMove();
                }
            }
            UpdateMovePoints(_baseSpeed);
        }

        private void iudBasicMove_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            if (e.OldValue != null)
            {
                if ((int) e.OldValue < (int) e.NewValue)
                {
                    if ((int) e.NewValue - _baseMove > 3)
                    {
                        MessageBox.Show("Can not raise Move by more then 3 _points from the Main Stat");
                        UpdateMovePoints(_baseMove);
                    }
                    else
                    {
                        BuyBonusMove();
                    }
                }
                else
                {
                    if (_baseMove - (int) e.NewValue > 3)
                    {
                        MessageBox.Show("Can not lower Move by more then 3 _points from the Main Stat");
                        UpdateMovePoints(_baseMove);
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
            //+-5 _points per +- 1 Yard
            if (CanBuy(5))
            {
                Points = Points - 5;
                _modMove += 1;
            }
            else
            {
                IudBasicMove.ValueChanged -= iudBasicMove_ValueChanged;
                IudBasicMove.Value -= 1;
                IudBasicMove.ValueChanged += iudBasicMove_ValueChanged;
            }
        }

        private void SellBonusMove()
        {
            //+-5 _points per +- 1 1 Yard
            Points = Points + 5;
            _modMove -= 1;
        }

        private void PopulateAdvantageSideBar( Advantages item )
        {
            TbAdvType.Text = GetAdvType(item);
            TbAdvReq.Text = GetRequirements(item);
            TbAdvName.Text = item.Name;
            TxtbAdvDiscription.Text = item.Discription;
            TbAdvPointCost.Text = item.PointCost.ToString();
            if (item.hasLevels != null && (bool) item.hasLevels)
            {
                LbladvLevel.Visibility = Visibility.Visible;
            }
            else
            {
                LbladvLevel.Visibility = Visibility.Hidden;
            }
        }

        private string GetRequirements( Advantages advitem )
        {
            foreach (var item in _reqList.Where(item => item.SourceType == "Advantage").Where(item => item.SourceItemID == advitem.Id))
            {
                return item.TargetName;
            }
            return "";
        }

        private void ClearAdvantageSideBar()
        {
            TbAdvName.Text = "";
            TbAdvReq.Text = "";
            TbAdvType.Text = "";
            TxtbAdvDiscription.Text = "";
            TbAdvPointCost.Text = "";
        }

        private static string GetAdvType( Advantages item )
        {
            var typelist = new List<string>();
            if (item.isPhysical != null && (bool) item.isPhysical)
            {
                typelist.Add("Physical");
            }
            if (item.isMental != null && (bool) item.isMental)
            {
                typelist.Add("Mental");
            }
            if (item.isMundane != null && (bool) item.isMundane)
            {
                typelist.Add("Mundane");
            }
            if (item.isExotic != null && (bool) item.isExotic)
            {
                typelist.Add("Exotic");
            }
            if (item.isSocial != null && (bool) item.isSocial)
            {
                typelist.Add("Social");
            }
            if (item.isSuperNatural != null && (bool) item.isSuperNatural)
            {
                typelist.Add("Super-Natural");
            }
            var type = string.Join(",", typelist);
            return type;
        }

        private bool CanBuy( int cost )
        {
            var canbuy = false;
            if (_points - cost >= 0)
            {
                canbuy = true;
            }
            else
            {
                MessageBox.Show("That is to expensive!");
            }
            return canbuy;
        }

        private void btSaveCharacter_Click( object sender, RoutedEventArgs e )
        {
            _character.Age = int.Parse(TbAge.Text);
            _character.BasicLift = int.Parse(TbBasicLift.Text);
            _character.BasicMove = IudBasicMove.Value;
            _character.BasicSpeed = IudBasicSpeed.Value;
            _character.Dexterity = IudDexterity.Value;
            _character.FatiguePoints = IudFatiguePoints.Value;
            _character.Health = IudHealth.Value;
            _character.Height = double.Parse(TbHeight.Text);
            _character.HitPoints = IudHitPoints.Value;
            _character.Intelligence = IudIntelligence.Value;
            _character.Name = TbName.Text;
            _character.Perception = IudPerception.Value;
            _character.PlayerName = MainWindow.CurrentInstance.LocalCient.UserName;
            _character.PointTotal = int.Parse(TbPoints.Text);
            _character.Strenght = IudStrenght.Value;
            _character.Weight = int.Parse(TbWeight.Text);
            _character.WillPower = IudWillPower.Value;
            ServerConnection.LocalServiceClient.SaveCharacter(MainWindow.CurrentInstance.LocalCient, _character);
            MessageBox.Show("Saved!");
        }

        private void btBuyAdv_Click( object sender, RoutedEventArgs e )
        {
            var pointCost = ( (Advantages) LbAdvantages.SelectedItem ).PointCost;
            if (pointCost == null || !CanBuy((int) pointCost))
            {
                return;
            }
            BuyAdvantage();
            ReloadDataGrid();
        }

        private void BuyAdvantage()
        {
            var adv = LbAdvantages.SelectedItem as Advantages;
            if (adv?.PointCost == null)
            {
                return;
            }
            if (!CanBuy((int) adv.PointCost))
            {
                return;
            }
            Points -= (int) adv.PointCost;
            if (_boughtAdvantageDict.ContainsKey(adv))
            {
                var value = _boughtAdvantageDict[adv];
                _boughtAdvantageDict[adv] = value + 1;
            }
            else
            {
                _boughtAdvantageDict.Add(adv, 1);
            }
            if (adv.hasLevels != null && (bool) !adv.hasLevels)
            {
                LbAdvantages.Items.Remove(LbAdvantages.SelectedItem);
            }
        }

        private void SellAdvantage()
        {
            var adv = ( from c in _boughtAdvantageDict where c.Key.Name == ( (KeyValuePair<string, int>) DgBoughtAdvantages.SelectedItem ).Key select c.Key ).FirstOrDefault();
            if (adv != null)
            {
                if (adv.PointCost != null)
                {
                    Points += (int) adv.PointCost;
                }

                if (_boughtAdvantageDict.ContainsKey(adv))
                {
                    if (_boughtAdvantageDict[adv] > 1)
                    {
                        _boughtAdvantageDict[adv] -= 1;
                        ReloadDataGrid();
                    }
                    else
                    {
                        _boughtAdvantageDict.Remove(adv);
                        ReloadDataGrid();
                        if (adv.hasLevels != null && (bool) !adv.hasLevels)
                        {
                            LbAdvantages.Items.Add(adv);
                            LbAdvantages.DisplayMemberPath = "Name";
                            LbAdvantages.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                        }
                    }
                }
            }
            ClearAdvantageSideBar();
        }

        private void lbAdvantages_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            DgBoughtAdvantages.SelectedItem = null;
            ClearAdvantageSideBar();
            if (LbAdvantages.SelectedItem != null)
            {
                PopulateAdvantageSideBar((Advantages) LbAdvantages.SelectedItem);
                if (TbAdvReq.Text == "")
                {
                    BtBuyAdv.IsEnabled = true;
                }
                else
                {
                    BtBuyAdv.IsEnabled = false;
                    foreach (var item in _boughtAdvantageDict.Keys.Where(item => item.Name == TbAdvReq.Text))
                    {
                        BtBuyAdv.IsEnabled = true;
                    }
                }
            }
            else
            {
                BtBuyAdv.IsEnabled = false;
            }
        }

        private void dgBoughtAdvantages_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if (DgBoughtAdvantages.SelectedItem != null)
            {
                SellAdvantage();
            }
        }

        private void dgBoughtAdvantages_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            LbAdvantages.SelectedItem = null;
            if (DgBoughtAdvantages.SelectedItem == null)
            {
                return;
            }
            ClearAdvantageSideBar();
            PopulateAdvantageSideBar(( from c in _boughtAdvantageDict where c.Key.Name == ( (KeyValuePair<string, int>) DgBoughtAdvantages.SelectedItem ).Key select c.Key ).FirstOrDefault());
        }
    }
}